
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
   public sealed class HospitalMap:ClassMap<Hospital>
    {
       public HospitalMap()
       {
           Id(x => x.Id);
           Map(x => x.Nome);
       }
    }
}
