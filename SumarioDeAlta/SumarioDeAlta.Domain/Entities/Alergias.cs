using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
    public class Alergia : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
    }
}
