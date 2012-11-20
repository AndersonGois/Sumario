using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class AlergiaMap:ClassMap<Alergia>
    {
        public AlergiaMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
