using NUnit.Framework;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Testes.Entities
{
    [TestFixture]
    public class PacienteTest
    {
        [Test]
        public void get_equals_deve_retornar_true_com_os_mesmos_valores_e_instancias_diferentes_test()
        {
            var pacienteA = new Paciente { CPF = "123", Nome = "Paciente", Id = 1 };
            var pacienteB = new Paciente { CPF = "123", Nome = "Paciente", Id = 1 };

            Assert.IsTrue(pacienteA.Equals(pacienteB));
        }

        [Test]
        public void get_equals_deve_retonar_false_caso_o_parametro_de_comparacao_seja_nulo_test()
        {
            var pacienteA = new Paciente { CPF = "123", Nome = "Paciente", Id = 1 };
            Paciente pacienteB = null;

            Assert.IsFalse(pacienteA.Equals(pacienteB));
        }

        [Test]
        public void get_hash_code_de_um_paciente_com_id_igual_a_1_deve_retornar_1_test()
        {
            var paciente = new Paciente { Id = 1 };

            Assert.AreEqual(1, paciente.GetHashCode());
        }
    }
}
