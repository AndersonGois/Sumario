
using System.Collections.Generic;
using System.ComponentModel;
using SumarioDeAlta.Domain.Entities.Interfaces;

namespace SumarioDeAlta.Domain.Entities
{
    public class DadosGerais : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        [DisplayName("Descrição da alergia")]
        public virtual string DescricaoAlergia { get; set; }
        public virtual bool IsAlergico { get; set; }
        public virtual IList<Alergia> Alergias { get; set; }
        public virtual IList<Medicamento> Medicamentos { get; set; }
       

        public virtual void Adicionar(Alergia alergia)
        {
            if (Alergias == null)
                Alergias = new List<Alergia>();

            Alergias.Add(alergia);
        }
    }
}
