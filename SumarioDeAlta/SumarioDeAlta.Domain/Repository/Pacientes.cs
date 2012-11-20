using NHibernate;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Domain.Repository
{
    public class Pacientes : BaseRepository
    {
        public Pacientes()
        {
        }

        public Pacientes(ISession session)
            : base(session)
        {

        }

        public virtual void Adicionar(Paciente paciente)
        {
            base.Salvar(paciente);
        }
    }
}
