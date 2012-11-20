using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class MedicamentoMap : ClassMap<Medicamento>
    {
        public MedicamentoMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
