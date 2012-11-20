
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
   public sealed class  ProcedimentoMap:ClassMap<Procedimento>
    {
        public ProcedimentoMap()
        {
            Id(x => x.Id);
            Map(x => x.DataProcedimento);
            References(x => x.TipoProcedimento);
        }
    }
}
