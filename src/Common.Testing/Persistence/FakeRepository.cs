using System;
using System.Collections.Generic;
using Common.Domain.Model;
using Common.Persistence;

namespace Common.Testing.Persistence
{
    public class FakeRepository : IRepository
    {
        private readonly Dictionary<string, List<IDomainEvent>> _eventStreams;

        public FakeRepository()
        {
            _eventStreams = new Dictionary<string, List<IDomainEvent>>();
        }

        public void Save<TAggregate>(TAggregate aggregate, Guid commitId)
            where TAggregate : AggregateRoot
        {
            if (_eventStreams.ContainsKey(aggregate.Id.ToString()))
                _eventStreams[aggregate.Id.ToString()].AddRange(aggregate.UnpublishedEvents);
            else
                _eventStreams.Add(aggregate.Id.ToString(), new List<IDomainEvent>(aggregate.UnpublishedEvents));

            aggregate.ClearUnpublishedEvents();
        }

        public TAggregate GetById<TAggregate>(string id)
            where TAggregate : AggregateRoot
        {
            if (!_eventStreams.ContainsKey(id)) return null;

            TAggregate result =
                (TAggregate)
                    Activator.CreateInstance(typeof(TAggregate), true);

            foreach (var @event in _eventStreams[id])
                (result as dynamic).Apply(@event as dynamic);

            return result;
        }
    }
}