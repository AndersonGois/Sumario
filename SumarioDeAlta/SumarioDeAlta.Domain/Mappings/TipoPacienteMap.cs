
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
   public sealed class TipoPacienteMap:ClassMap<TipoPaciente>
    {
       public TipoPacienteMap()
       {
           Id(x => x.Id);
           Map(x => x.Nome);

       }
    }
}
