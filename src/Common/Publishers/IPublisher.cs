using Common.Domain.Model;

namespace Common.Publishers
{
    public interface IPublisher
    {
        void Publish(IDomainEvent domainEvent);
    }    
}