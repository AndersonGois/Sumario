
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class TipoProcedimentoMap : ClassMap<TipoProcedimento>
    {
        public TipoProcedimentoMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
