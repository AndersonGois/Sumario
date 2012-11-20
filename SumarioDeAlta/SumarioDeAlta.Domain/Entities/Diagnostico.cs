using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
    public class Diagnostico : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string CodigoCid { get; set; }
        public virtual string Cid { get; set; }
        public virtual TipoDiagnostico TipoDiagnostico { get; set; }

    }
}
