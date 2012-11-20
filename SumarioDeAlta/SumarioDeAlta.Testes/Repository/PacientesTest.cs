using System;
using NUnit.Framework;
using SumarioDeAlta.Domain.Entities;
using SumarioDeAlta.Domain.Entities.Common;
using SumarioDeAlta.Domain.Repository;
using SumarioDeAlta.Testes.BaseTest;

namespace SumarioDeAlta.Testes.Repository
{
    [TestFixture]
    public class PacientesTest : PersistenceTestBase
    {
        private Paciente paciente;
        private Pacientes pacientes;

        [SetUp]
        public void inicializar()
        {
            paciente = new Paciente { Nome = "Isaac" ,CPF = "01234567890",Nascimento = DateTime.Now,Sexo = new TipoSexo{Nome ="Masculino"} };

            pacientes = new Pacientes(Session);

            pacientes.Adicionar(paciente);
        }

        [Test]
        public void construtor_vazio_nao_deve_retornar_excecao_test()
        {
            var pacientesComConstrutorVazio = new Pacientes();

            Assert.IsNotNull(pacientesComConstrutorVazio);
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
