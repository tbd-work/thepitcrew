using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain.Model;
using Common.Publishers;
using Raven.Client;

namespace Common.Persistence
{
    public class RavenRepository : IRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IPublisher _publisher;

        public RavenRepository(IDocumentStore documentStore, IPublisher publisher)
        {
            _documentStore = documentStore;
            _publisher = publisher;
        }

        public void Save<TAggregate>(TAggregate aggregate, Guid commitId)
            where TAggregate : AggregateRoot
        {
            StreamHead streamHead = GetStreamHead(aggregate.Id);

            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                documentSession.Store(new StreamData
                                      {
                                          Id = string.Format("{0}/{1}", aggregate.Id, streamHead.Commit),
                                          CommitId = commitId,
                                          ClrTypeName = typeof (TAggregate).FullName,
                                          EventData = aggregate.UnpublishedEvents,
                                          HeaderId = streamHead.Commit
                                      });

                documentSession.SaveChanges();
            }

            UpdateStreamHead(streamHead);

            foreach (IDomainEvent @event in aggregate.UnpublishedEvents)
                _publisher.Publish(@event);

            aggregate.ClearUnpublishedEvents();
        }   

        public TAggregate GetById<TAggregate>(string id) where TAggregate : AggregateRoot
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                IEnumerable<StreamData> streams = documentSession.Advanced.LoadStartingWith<StreamData>(id);

                TAggregate aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);

                foreach (var streamData in streams.OrderBy(s => s.HeaderId))
                    foreach (IDomainEvent domainEvent in streamData.EventData)
                        (aggregate as dynamic).Apply(domainEvent as dynamic);

                return aggregate;
            }
        }

        private StreamHead GetStreamHead(Guid aggregateId)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                StreamHead currentStreamHead = documentSession.Load<StreamHead>(aggregateId) ??
                                               new StreamHead { Id = aggregateId.ToString(), Commit = 0 };

                currentStreamHead.UpdateCommitTo(currentStreamHead.Commit + 1);

                return currentStreamHead;
            }
        }

        private void UpdateStreamHead(StreamHead streamHead)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                documentSession.Store(streamHead);
                documentSession.SaveChanges();
            }
        }
    }
}