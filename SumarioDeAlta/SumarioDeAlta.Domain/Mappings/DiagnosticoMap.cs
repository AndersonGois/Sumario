
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
   public sealed class DiagnosticoMap:ClassMap<Diagnostico>
    {
       public DiagnosticoMap()
       {
           Id(x => x.Id);
           Map(x => x.Cid);
           Map(x => x.CodigoCid);
           References(x => x.TipoDiagnostico);
       }
    }
}
