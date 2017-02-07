using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using SumarioDeAlta.Domain.Entities;
using SumarioDeAlta.Domain.Entities.Common;
using SumarioDeAlta.Domain.Repository;

namespace SumarioDeAlta.Testes.Infrasctructure
{
    [TestFixture]
   // [Ignore]
    public class CriarBancoDeDadosTest
    {
        [Test]
        [Ignore]
        public void a__Criar_Banco_De_Dados_Por_Modelo()
        {
            Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
           .FromAppSetting("Conexao")
            )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<Paciente>()).Mappings(m => m.MergeMappings())
            .ExposeConfiguration(BuildSchema).BuildSessionFactory();
        }

        [Test]
        public void a_Criar_TipoSexo()
        {
            var tiposDeSexo = new TiposDeSexo();
            Tipo masculino = new TipoSexo { Nome = "Masculino" };
            Tipo feminino = new TipoSexo { Nome = "Feminino" };

            tiposDeSexo.Salvar(masculino);
            tiposDeSexo.Salvar(feminino);
        }

        [Test]
        public void a_Criar_hospital()
        {
            var hospitais = new Hospitais();
            var quintador = new Hospital { Nome = "Hospital Quinta DOR" };
            var barraDor = new Hospital { Nome = "Hospital Barra DOR" };
            hospitais.Salvar(quintador);
            hospitais.Salvar(barraDor);
        }

        [Test]
        public void a_Criar_Admissao()
        {
            var admissaos = new Admissaos();
            var cirurgica = new Admissao { Nome = "Cirúrgica" };
            var clinica = new Admissao { Nome = "Clínica" };
            var eletiva = new Admissao { Nome = "Eletiva" };
            var emergencia = new Admissao { Nome = "Emergência" };
            admissaos.Salvar(clinica);
            admissaos.Salvar(cirurgica);
            admissaos.Salvar(eletiva);
            admissaos.Salvar(emergencia);

        }
        [Test]
        public void a_criar_tipoPaciente()
        {
            var tipoPacientes = new TipoPacientes();
            var tipopaciente = new TipoPaciente();
            tipopaciente.Nome = "Clínica Médica";
            tipoPacientes.Salvar(tipopaciente);
        }

        [Test]
        public void a_Criar_alergia()
        {
            var Alergias = new Alergias();
            var angiodema = new Alergia { Nome = "Angiodema" };
            var urticaria = new Alergia { Nome = "Urticária" };
            var anafilatico = new Alergia { Nome = "Choque Anafilático" };
            var broncoespasmo = new Alergia { Nome = "Broncoespasmo" };
            var laringoespasmo = new Alergia { Nome = "Laringoespasmo" };
            var outros = new Alergia { Nome = "Outros" };

            Alergias.Salvar(angiodema);
            Alergias.Salvar(urticaria);
            Alergias.Salvar(anafilatico);
            Alergias.Salvar(broncoespasmo);
            Alergias.Salvar(laringoespasmo);
            Alergias.Salvar(outros);

        }

        [Test]
        public void a_Criar_TipoDiagnostico()
        {
            var tipoDiagnosticos = new TipoDiagnosticos();
            var principal = new TipoDiagnostico { Nome = "Diagnóstico Principal" };
            var secundario = new TipoDiagnostico { Nome = "Diagnóstico Secundário" };
            tipoDiagnosticos.Salvar(principal);
            tipoDiagnosticos.Salvar(secundario);

        }
        [Test]
        public void a_adicionar_Tipoprocedimento()
        {
            var tipoProcedimentos = new TipoProcedimentos();
            var Adenoidectomia = new TipoProcedimento
            {
                Nome = "Adenoidectomia por videoendoscopia Outros",

            };
            var arteriografia = new TipoProcedimento
            {
                Nome = "Arteriografia Vertebral",

            };
            tipoProcedimentos.Salvar(Adenoidectomia);
            tipoProcedimentos.Salvar(arteriografia);
        }

        [Test]
        public void b_Criar_Diagnostico()
        {
            var diagnosticos = new Diagnosticos();
            var anemia = new Diagnostico { Cid = "Anemia em neoplastias(C00-D48)", CodigoCid = "D63.0*", TipoDiagnostico = new TipoDiagnostico { Id = 1 } };
            var cardiomegalia = new Diagnostico { Cid = "Cardiomegalia", CodigoCid = "151.7", TipoDiagnostico = new TipoDiagnostico { Id = 2 } };
            var complicacoes = new Diagnostico { Cid = "Algumas Complicações atuais subsequente ao infarto agudo do miocardio ", CodigoCid = "D63.0*", TipoDiagnostico = new TipoDiagnostico { Id = 1 } };

            diagnosticos.Salvar(anemia);
            diagnosticos.Salvar(cardiomegalia);
            diagnosticos.Salvar(complicacoes);
        }

        [Test]
        public void c1_Criar_pacientes()
        {
            var pacientes = new Pacientes();
            var matheus = PacienteMatheus();
            var maria = PacienteMaria();
            var joao = Pacientejoao();
            var tiago = PacienteTiago();
            var wilson = PacienteWilson();
            var pedro = PacientePedro();

            pacientes.Adicionar(pedro);
            pacientes.Adicionar(wilson);
            pacientes.Adicionar(tiago);
            pacientes.Adicionar(matheus);
            pacientes.Adicionar(maria);
            pacientes.Adicionar(joao);

        }


        [Test]
        public void c2_obterAdimissoes()
        {


            var diag = new Diagnosticos();
            var diagd = diag.Todos<Diagnostico>();

        }

        private static Paciente PacienteMatheus()
        {
            var admissaos = new Admissaos();
            var admissao = admissaos.Obter<Admissao>(1);
            var admissao2 = admissaos.Obter<Admissao>(2);
            var alergias = new Alergias();
            var alergia = alergias.Obter<Alergia>(1);
            var tipoProcedimentos = new TipoProcedimentos();
            var tipoProcedimento = tipoProcedimentos.Obter<TipoProcedimento>(1);
            var tipoProcedimento2 = tipoProcedimentos.Obter<TipoProcedimento>(2);

            var admissaoHospital = new AdmissaoHospital
                                              {
                                                  DataAdmissao = new DateTime(2012, 5, 20),
                                                  DataSaida = new DateTime(2012, 6, 20),
                                                  Registro = "21545",
                                                  Hospital = new Hospital { Id = 1 },
                                                  //Admissao = new Admissao { Id = 2 }
                                              };
            admissaoHospital.Adicionar(admissao);
            admissaoHospital.Adicionar(admissao);
            var procedimento = new Procedimento { DataProcedimento = new DateTime(2012, 5, 20), TipoProcedimento = tipoProcedimento };

            var procedimento2 = new Procedimento { DataProcedimento = new DateTime(2012, 5, 25), TipoProcedimento = tipoProcedimento2 };
            var paciente = new Paciente
                              {
                                  CPF = "02145253625",
                                  Nome = "Matheus",
                                  Nascimento = DateTime.Parse("20/09/1998"),
                                  DataUltimaAtualizacao = DateTime.UtcNow,
                                  Sexo = new TipoSexo { Id = 1 },
                                  TipoDiagnostico = new TipoDiagnostico { Id = 1 },
                                  TipoPaciente = new TipoPaciente { Id = 1 }
                              };
            paciente.Adicionar(procedimento);
            paciente.Adicionar(procedimento2);
            paciente.Adicionar(admissaoHospital);
            paciente.DadosGerais.Adicionar(alergia);

            return paciente;
        }

        private static Paciente PacienteMaria()
        {
            var alergias = new Alergias();
            var admissaos = new Admissaos();

            var alergia = alergias.Obter<Alergia>(1);
            var admissao = admissaos.Obter<Admissao>(1);

            var admissaoHospital = new AdmissaoHospital
            {
                DataAdmissao = new DateTime(2012, 4, 10),
                DataSaida = new DateTime(2012, 5, 20),
                Registro = "5487",
                Hospital = new Hospital { Id = 1 }
                //Admissao = new Admissao { Id = 3 }
            };

            var paciente = new Paciente
            {
                CPF = "12547859856",
                Nome = "Maria",
                Nascimento = DateTime.Parse("20/09/1999"),
                DataUltimaAtualizacao = DateTime.UtcNow,
                Sexo = new TipoSexo { Id = 2 },
                TipoDiagnostico = new TipoDiagnostico { Id = 2 },
                TipoPaciente = new TipoPaciente { Id = 1 }
            };

            paciente.Adicionar(admissaoHospital);
            paciente.DadosGerais.Adicionar(alergia);
            //paciente.DadosGerais.Adicionar(admissao);
            return paciente;
        }

        private static Paciente Pacientejoao()
        {
            var alergias = new Alergias();
            var admissaos = new Admissaos();

            var alergia = alergias.Obter<Alergia>(1);
            var admissao = admissaos.Obter<Admissao>(1);
            var admissao2 = admissaos.Obter<Admissao>(2);

            var admissaoHospital = new AdmissaoHospital
            {
                DataAdmissao = new DateTime(2012, 9, 10),
                DataSaida = new DateTime(2012, 10, 10),
                Registro = "8758",
                Hospital = new Hospital { Id = 2 }
                //Admissao = new Admissao { Id = 1 }
            };

            var admissaoHospital2 = new AdmissaoHospital
            {
                DataAdmissao = new DateTime(2012, 11, 4),
                DataSaida = new DateTime(2012, 11, 5),
                Registro = "8789",
                Hospital = new Hospital { Id = 1 }
                //Admissao = new Admissao { Id = 4 }
            };

            var paciente = new Paciente
            {
                CPF = "254125387254",
                Nome = "João",
                Nascimento = DateTime.Parse("20/09/1969"),
                DataUltimaAtualizacao = DateTime.UtcNow,
                Sexo = new TipoSexo { Id = 1 },
                TipoDiagnostico = new TipoDiagnostico { Id = 2 },
                TipoPaciente = new TipoPaciente { Id = 1 }
            };

            paciente.Adicionar(admissaoHospital);
            paciente.Adicionar(admissaoHospital2);
            paciente.DadosGerais.Adicionar(alergia);
            //paciente.DadosGerais.Adicionar(admissao);
            //paciente.DadosGerais.Adicionar(admissao2);
            return paciente;
        }

        private static Paciente PacienteWilson()
        {
            var alergias = new Alergias();
            var admissaos = new Admissaos();

            var alergia = alergias.Obter<Alergia>(1);
            var admissao = admissaos.Obter<Admissao>(1);

            var admissaoHospital = new AdmissaoHospital
            {
                DataAdmissao = new DateTime(2012, 5, 20),
                DataSaida = new DateTime(2012, 6, 20),
                Registro = "7788",
                Hospital = new Hospital { Id = 2 }
                //Admissao = new Admissao { Id = 2 }
            };

            var paciente = new Paciente
            {
                CPF = "21445253625",
                Nome = "Wilson",
                Nascimento = DateTime.Parse("20/09/1978"),
                DataUltimaAtualizacao = DateTime.UtcNow,
                Sexo = new TipoSexo { Id = 1 },
                TipoDiagnostico = new TipoDiagnostico { Id = 1 },
                TipoPaciente = new TipoPaciente { Id = 1 }
            };

            paciente.Adicionar(admissaoHospital);
            paciente.DadosGerais.Adicionar(alergia);
            //paciente.DadosGerais.Adicionar(admissao);
            return paciente;
        }

        private static Paciente PacienteTiago()
        {
            var alergias = new Alergias();
            var admissaos = new Admissaos();

            var alergia = alergias.Obter<Alergia>(2);
            var admissao = admissaos.Obter<Admissao>(2);

            var admissaoHospital = new AdmissaoHospital
            {
                DataAdmissao = new DateTime(2012, 4, 10),
                DataSaida = new DateTime(2012, 5, 20),
                Registro = "6658",
                Hospital = new Hospital { Id = 1 }
                //Admissao = new Admissao { Id = 2 }
            };

            var paciente = new Paciente
            {
                CPF = "12547888856",
                Nome = "Tiago",
                Nascimento = DateTime.Parse("20/09/1992"),
                DataUltimaAtualizacao = DateTime.UtcNow,
                Sexo = new TipoSexo { Id = 1 },
                TipoDiagnostico = new TipoDiagnostico { Id = 2 },
                TipoPaciente = new TipoPaciente { Id = 1 }
            };

            paciente.Adicionar(admissaoHospital);
            paciente.DadosGerais.Adicionar(alergia);
            //paciente.DadosGerais.Adicionar(admissao);
            return paciente;
        }

        private static Paciente PacientePedro()
        {
            var alergias = new Alergias();
            var admissaos = new Admissaos();

            var alergia = alergias.Obter<Alergia>(1);
            var admissao = admissaos.Obter<Admissao>(1);

            var admissaoHospital = new AdmissaoHospital
            {
                DataAdmissao = new DateTime(2012, 9, 1),
                DataSaida = new DateTime(2012, 11, 10),
                Registro = "4141",
                Hospital = new Hospital { Id = 1 }
                //Admissao = new Admissao { Id = 2 }
            };

            var paciente = new Paciente
            {
                CPF = "254125387246",
                Nome = "Pedro",
                Nascimento = DateTime.Parse("20/09/1979"),
                DataUltimaAtualizacao = DateTime.UtcNow,
                Sexo = new TipoSexo { Id = 1 },
                TipoDiagnostico = new TipoDiagnostico { Id = 1 },
                TipoPaciente = new TipoPaciente { Id = 1 }
            };

            paciente.Adicionar(admissaoHospital);
            paciente.DadosGerais.Adicionar(alergia);
            //paciente.DadosGerais.Adicionar(admissao);
            return paciente;
        }
        [Test]
        [Ignore]
        public void z_removerProcediemto()
        {
            var pacientes = new Pacientes();
            var paci = pacientes.Obter<Paciente>(4);
            var poroc = paci.Procedimentos.FirstOrDefault(x => x.Id == 1);
            paci.Excluir(poroc);
            pacientes.Salvar(paci);
        }

        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Drop(true, true);

            new SchemaExport(config)
                .Create(true, true);
        }
    }
}
