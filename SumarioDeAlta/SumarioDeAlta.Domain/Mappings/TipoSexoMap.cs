using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities.Common;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class TipoSexoMap : ClassMap<TipoSexo>
    {
        public TipoSexoMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
