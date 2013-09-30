using System;

namespace Common.Domain.Model
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; set; } 
    }
}