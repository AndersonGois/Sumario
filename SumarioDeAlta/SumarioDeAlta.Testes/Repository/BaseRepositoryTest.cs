using NUnit.Framework;
using SumarioDeAlta.Domain.Entities;
using SumarioDeAlta.Domain.Repository;

namespace SumarioDeAlta.Testes.Repository
{
    [TestFixture]
    public class BaseRepositoryTest
    {
        private Pacientes pacientes;
        private Paciente paciente;

        [SetUp]
        public void a_Criar_Banco_De_Dados_Por_Modelo()
        {
            pacientes = new Pacientes();

            paciente = new Paciente {CPF = "6576576", Nome = "Isaac"};

            pacientes.Adicionar(paciente);
        }

        [Test]
        public void adicionar_um_paciente_com_sucesso_test()
        {
            Assert.AreEqual(paciente.Id, 1);
        }

        [Test]
        public void obter_o_paciente_por_id_test()
        {
            var pacienteObtido = pacientes.Obter<Paciente>(paciente.Id);

            Assert.AreEqual(paciente, pacienteObtido);
        }

        [Test]
        public void obter_todos_os_pacientes_deve_retornar_somente_um_test()
        {
            var pacientesObtidos = pacientes.Todos<Paciente>();

            Assert.AreEqual(1, pacientesObtidos.Count);
        }

        [TearDown]
        public void remover_um_paciente_com_sucesso_test()
        {
            pacientes.Deletar(paciente);
        }
    }
}
