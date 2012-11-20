namespace SumarioDeAlta.Domain.Entities.Interfaces
{
    public interface IAggregateRoot<T>
    {
        T Id { get; }
    }
}
