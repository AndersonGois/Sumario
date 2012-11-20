
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
   public sealed class TipoDiagnosticoMap: ClassMap<TipoDiagnostico>
    {
        public TipoDiagnosticoMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
           
        }
    }
}
