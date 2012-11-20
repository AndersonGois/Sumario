
using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
   public sealed class  AdmissaoHospitalMap : ClassMap<AdmissaoHospital>
    {
        public AdmissaoHospitalMap()
        {
            Id(x => x.Id);
            Map(x => x.DataAdmissao);
            Map(x => x.DataSaida);
            Map(x => x.Registro);
            HasManyToMany(x => x.Admissaos).Cascade.All();
            References(x => x.Hospital);
        }
    }
}
