
using System;
using System.Collections.Generic;
using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
    public class AdmissaoHospital : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataAdmissao { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual string Registro { get; set; }
        public virtual DateTime DataSaida { get; set; }
        public virtual IList<Admissao> Admissaos { get; set; }

        public virtual void Adicionar(Admissao admissao)
        {
            if (Admissaos == null)
                Admissaos = new List<Admissao>();
            Admissaos.Add(admissao);
        }
    }
}
