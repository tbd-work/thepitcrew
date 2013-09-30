using System;
using System.Collections.Generic;
using Common.Domain.Model;

namespace Common.Persistence
{
    class StreamData
    {
        public string Id { get; set; }
        public Guid CommitId { get; set; }
        public int HeaderId { get; set; }
        public string ClrTypeName { get; set; }
        public IEnumerable<IDomainEvent> EventData { get; set; }
    }
}