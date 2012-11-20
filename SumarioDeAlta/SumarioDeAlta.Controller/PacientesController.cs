using System;
using System.Collections.Generic;
using SumarioDeAlta.Domain.Repository;
using SumarioDeAlta.Domain.Entities;
using System.Linq;

namespace SumarioDeAlta.Controller
{
    public class PacientesController
    {
        private Pacientes _pacientes;
        private Admissaos _Admissaos;
        private Alergias _alergias;
        private Procedimentos _procedimentos;
        private TipoProcedimentos _tipoProcedimentos;
        
        public PacientesController()
        {
            _pacientes = new Pacientes();
            _alergias = new Alergias();
            _procedimentos = new Procedimentos();
            _Admissaos = new Admissaos();
            _tipoProcedimentos = new TipoProcedimentos();
        }

        #region Pesquisa Paciente

        public Paciente ObterPaciente(int id)
        {
            return _pacientes.Obter<Paciente>(id);
        }

        public IList<Paciente> TodosPacientes()
        {
            return _pacientes.Todos<Paciente>();
        }

        #endregion

        #region Dados Gerais

        public IList<Admissao> TodasAdmissoes()
        {
            return _Admissaos.Todos<Admissao>();
        }

        public IList<Alergia> TodasAlergias()
        {
            return _alergias.Todos<Alergia>();
        }

        public IList<Diagnostico> TodosDiagnosticos()
        {
            var diagnosticos = new Diagnosticos();
            return diagnosticos.Todos<Diagnostico>();
        }

        public void SalvarMotivoAdmissao(string admissaoId, string pacienteId)
        {
            if (string.IsNullOrEmpty(admissaoId) || string.IsNullOrEmpty(pacienteId)) return;
            var paciente = _pacientes.Obter<Paciente>(int.Parse(pacienteId));
            var admissaoHospital = paciente.AdmissaoHospital.OrderByDescending(x => x.DataAdmissao).FirstOrDefault();
            if (admissaoHospital != null)
            {
                var list = admissaoId.Split(',');
                foreach (var id in list)
                {
                    var admissao = _Admissaos.Obter<Admissao>(int.Parse(id));
                    admissaoHospital.Adicionar(admissao);
                }
            }
            _pacientes.Salvar(paciente);
        }

        public void SalvarDadosGerais(string alergiaId, string pacienteId, string descricaoAlergia, string isAlergia, string admissaoId)
        {

            if (string.IsNullOrEmpty(alergiaId) || string.IsNullOrEmpty(pacienteId) || string.IsNullOrEmpty(isAlergia)) return;
            var paciente = _pacientes.Obter<Paciente>(int.Parse(pacienteId));

            paciente.DadosGerais.IsAlergico = bool.Parse(isAlergia);
            paciente.DadosGerais.DescricaoAlergia = descricaoAlergia;
            var listAlergia = alergiaId.Split(',');
            foreach (var id in listAlergia)
            {
                var alergia = _alergias.Obter<Alergia>(int.Parse(id));
                if (paciente.DadosGerais.Alergias.Count(x => x.Id == alergia.Id) < 1)
                    paciente.DadosGerais.Adicionar(alergia);
            }

            var admissaoHospital = paciente.AdmissaoHospital.OrderByDescending(x => x.DataAdmissao).FirstOrDefault();
            if (admissaoHospital != null)
            {
                var listAdmissao = admissaoId.Split(',');
                foreach (var id in listAdmissao)
                {
                    var admissao = _Admissaos.Obter<Admissao>(int.Parse(id));
                    if (admissaoHospital.Admissaos.Count(x => x.Id == admissao.Id) < 1)
                        admissaoHospital.Adicionar(admissao);
                }
            }

            _pacientes.Salvar(paciente);
        }

        #endregion

        #region Procedimentos

        public IList<TipoProcedimento> TodosTipoProcedimentos()
        {
            return _procedimentos.Todos<TipoProcedimento>();
        }
        public IList<Procedimento> TodosProcedimentos()
        {

            return _procedimentos.Todos<Procedimento>();
        }
        public IList<Procedimento> TodosProcedimentosDoPaciente(int id)
        {
            if (id == 0)
                return null;

            var list = _pacientes.Obter<Paciente>(id);
            if (list == null) return null;


            return list.Procedimentos;
        }
        public void IncluirProcedimentoAPaciente(string procedimento, string data, string pacienteId)
        {
            if (string.IsNullOrEmpty(pacienteId) || string.IsNullOrEmpty(procedimento) || string.IsNullOrEmpty(data)) return;

            var paciente = _pacientes.Obter<Paciente>(int.Parse(pacienteId));
            var tipoProcedimento = _tipoProcedimentos.Obter<TipoProcedimento>(int.Parse(procedimento));
            var procediment = new Procedimento { DataProcedimento = DateTime.Parse(data), TipoProcedimento = tipoProcedimento };
            paciente.Adicionar(procediment);

            _pacientes.Adicionar(paciente);
        }
        public void ExcluirProcedimento(string procedimentoId, string pacienteId)
        {
            if (string.IsNullOrEmpty(pacienteId) || string.IsNullOrEmpty(procedimentoId)) return;

            var paciente = _pacientes.Obter<Paciente>(int.Parse(pacienteId));

            string[] list = procedimentoId.Split(',');
            foreach (var id in list)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var procedimento = paciente.Procedimentos.FirstOrDefault(x => x.Id == int.Parse(id));
                    paciente.Excluir(procedimento);
                }
            }

            _pacientes.Salvar(paciente);
        }

        #endregion
    }
}
