using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class DadosGeraisMap : ClassMap<DadosGerais>
    {
        public DadosGeraisMap()
        {
            Id(x => x.Id);
            Map(x => x.DescricaoAlergia);
            Map(x => x.IsAlergico);
            HasManyToMany(x => x.Alergias);
            HasManyToMany(x => x.Medicamentos);
          
        }
    }
}
