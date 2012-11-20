using FluentNHibernate.Mapping;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Mappings
{
    public sealed class PacienteMap : ClassMap<Paciente>
    {
        public PacienteMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
            Map(x => x.CPF);
            Map(x => x.Nascimento);
            Map(x => x.DataUltimaAtualizacao);
            References(x => x.Sexo);
            References(x => x.TipoDiagnostico);
            References(x => x.DadosGerais).Cascade.All();
            References(x => x.TipoPaciente);
            HasManyToMany(x => x.Procedimentos).Cascade.All(); 
            HasMany(x => x.AdmissaoHospital).Cascade.AllDeleteOrphan();
            
        }
    }
}
