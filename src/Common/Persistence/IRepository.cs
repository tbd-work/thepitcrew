using System;
using Common.Domain.Model;

namespace Common.Persistence
{
    public interface IRepository
    {
        TAggregate GetById<TAggregate>(string id)
            where TAggregate : AggregateRoot;

        void Save<TAggregate>(TAggregate aggregate, Guid commitId)
            where TAggregate : AggregateRoot;
    }
}