using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class AdmissaoMap : ClassMap<Admissao>
    {
        public AdmissaoMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
