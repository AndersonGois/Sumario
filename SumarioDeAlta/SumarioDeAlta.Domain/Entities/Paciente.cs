using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SumarioDeAlta.Domain.Entities.Common;
using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
    public class Paciente : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string CPF { get; set; }

        public virtual DateTime? Nascimento { get; set; }

        public virtual TipoPaciente TipoPaciente { get; set; }

        public virtual TipoSexo Sexo { get; set; }

        public virtual IList<AdmissaoHospital> AdmissaoHospital { get; set; }

        public virtual IList<Procedimento> Procedimentos { get; set; }

        [DisplayName("Data da última atualização")]
        public virtual DateTime? DataUltimaAtualizacao { get; set; }

        public virtual DadosGerais DadosGerais { get; set; }

        public virtual TipoDiagnostico TipoDiagnostico { get; set; }

        //public override bool Equals(object obj)
        //{
        //    var outroPaciente = (Paciente)obj;

        //    if (outroPaciente == null) return false;

        //    return outroPaciente.Nome == Nome
        //           && outroPaciente.Id == Id
        //           && outroPaciente.CPF == CPF;
        //}

        public override int GetHashCode()
        {
            return Id;
        }

        public virtual void Adicionar(Procedimento procedimento)
        {
            if (Procedimentos == null)
                Procedimentos = new List<Procedimento>();
            Procedimentos.Add(procedimento);
        }
        public virtual void Excluir(Procedimento procedimento)
        {
            if (Procedimentos == null)
                Procedimentos = new List<Procedimento>();
            Procedimentos.Remove(procedimento);
        }

        public virtual void Adicionar(AdmissaoHospital admissaoHospital)
        {
            if (AdmissaoHospital == null)
                AdmissaoHospital = new List<AdmissaoHospital>();
            AdmissaoHospital.Add(admissaoHospital);
        }
        public Paciente()
        {
            DadosGerais = new DadosGerais();
        }
    }
}
