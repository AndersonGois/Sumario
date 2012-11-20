using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Text;
using SumarioDeAlta.Controller;
using SumarioDeAlta.Domain.Entities;

namespace SumarioDeAlta.Mvc.Controllers
{
    public class PacienteController : System.Web.Mvc.Controller
    {

        private PacientesController _controller;
        private Paciente Paciente;

        public PacienteController()
        {
            _controller = new PacientesController();
        }

        #region Pesquisa Pacientes

        public ActionResult Index()
        {
            return View();
        }

        #region AutoComplete

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AutoCompletePaciente(string nome)
        {
            var list = (from a in ListaDeRegistroPaciente()
                        where a.Paciente.ToLower().Contains(nome.ToLower())
                        select new { name = a.Paciente, id = a.Id }).Distinct().ToList();


            return new JsonResult { Data = list };
        }

        #endregion

        #region Grid de Paciente

        public IEnumerable<RegistroPaciente> OrderByJqGrid(string sidx, string sord, List<RegistroPaciente> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(RegistroPaciente).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult CarregarPacientes(string sidx, string sord, int page, int rows, string nome, string prontuario, string cpf)
        {

            List<RegistroPaciente> list;
            list = string.IsNullOrEmpty(nome) ? OrderByJqGrid(sidx, sord, ListaDeRegistroPaciente()).ToList() : OrderByJqGrid(sidx, sord, ListaDeRegistroPaciente()).Where(p => p.Paciente.Contains(nome)).ToList();

            //list = string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(prontuario)
            //           ? OrderByJqGrid(sidx, sord, ListaDeRegistroPaciente()).ToList()
            //           : OrderByJqGrid(sidx, sord, ListaDeRegistroPaciente())
            //                 .Where(p => nome != null && (p.Registro.Contains(prontuario) && p.Paciente.Contains(nome))).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
                             {
                                 total = totalPages,
                                 page = page,
                                 records = iRowCount,
                                 rows = new RowElement[iCurrencyRows]
                             };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[8] };
                second.rows[currentRow].cell[1] = list[iCount].Id;
                second.rows[currentRow].cell[2] = list[iCount].Registro;
                second.rows[currentRow].cell[3] = list[iCount].Unidade;
                second.rows[currentRow].cell[4] = list[iCount].Paciente;
                second.rows[currentRow].cell[5] = list[iCount].Admissao;
                second.rows[currentRow].cell[6] = list[iCount].TipoPaciente;
                second.rows[currentRow].cell[7] = list[iCount].Saida;

                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }
        public class RegistroPaciente
        {
            public string Id { get; set; }
            public string Registro { get; set; }
            public string Unidade { get; set; }
            public string Paciente { get; set; }
            public string Admissao { get; set; }
            public string TipoPaciente { get; set; }
            public string Saida { get; set; }

        }
        public List<RegistroPaciente> ListaDeRegistroPaciente()
        {
            var list = new List<RegistroPaciente>();
            var paciente = _controller.TodosPacientes();
            foreach (var item in paciente)
            {

                var firstOrDefault = item.AdmissaoHospital.Select(x => x.Hospital).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    foreach (var admissaoHospital in item.AdmissaoHospital)
                    {

                        var _paciente = new RegistroPaciente
                                            {
                                                Id = item.Id.ToString(),
                                                Registro = admissaoHospital.Registro,
                                                Unidade = admissaoHospital.Hospital.Nome,
                                                Paciente = item.Nome,
                                                Admissao = admissaoHospital.DataAdmissao.ToShortDateString(),
                                                Saida = admissaoHospital.DataSaida.ToShortDateString(),
                                                TipoPaciente = item.TipoPaciente.Nome

                                            };
                        list.Add(_paciente);



                    }
                }
            }

            return list;
        }

        #endregion

        #endregion

        #region Dados Gerais

        public ActionResult DadosGerais(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            var admissaoHospital = paciente.AdmissaoHospital.FirstOrDefault();


            if (admissaoHospital != null)
            {
                ViewBag.registro = "Registro: " + admissaoHospital.Registro;
                ViewBag.Internacao = "Internação: " + admissaoHospital.DataAdmissao.ToShortDateString();
                ViewBag.AltaHospitalar ="Alta Hospitalar: "+ admissaoHospital.DataSaida.ToShortDateString();
            }

            ViewBag.Paciente = " Paciente: " + paciente.Nome;
            

            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        [HttpPost]
        public ActionResult DadosGerais(Paciente paciente)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Procedimento", new { id = paciente.Id });
            }
            catch
            {
                return Redirect("Index");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void SalvarDadosGerais(string alergiaId, string pacienteId, string descricaoAlergia, string isAlergia, string admissaoId)
        {
            _controller.SalvarDadosGerais(alergiaId, pacienteId, descricaoAlergia, isAlergia, admissaoId);
        }

        #region gridDiagnostico

        public List<Diagnostico> ListaDiagnostico()
        {
            var list = _controller.TodosDiagnosticos().Select(item => new Diagnostico
            {
                Cid = item.Cid,
                CodigoCid = item.CodigoCid,
                TipoDiagnostico = item.TipoDiagnostico.Nome
            }).ToList();
            return list;
        }
        public class Diagnostico
        {
            public int Id { get; set; }
            public string TipoDiagnostico { get; set; }
            public string CodigoCid { get; set; }
            public string Cid { get; set; }
        }
        public IEnumerable<Diagnostico> OrderByJqGrid(string sidx, string sord, List<Diagnostico> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(Diagnostico).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult CarregarDiagnostico(string sidx, string sord, int page, int rows)
        {
            List<Diagnostico> list;
            list = OrderByJqGrid(sidx, sord, ListaDiagnostico()).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
            {
                total = totalPages,
                page = page,
                records = iRowCount,
                rows = new RowElement[iCurrencyRows]
            };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[3] };
                second.rows[currentRow].cell[0] = list[iCount].TipoDiagnostico;
                second.rows[currentRow].cell[1] = list[iCount].CodigoCid;
                second.rows[currentRow].cell[2] = list[iCount].Cid;

                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Procedimentos

        public ActionResult Procedimento(int? id)
        {

            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string ListadeProcedimento()
        {
            var list = new StringBuilder();
            var todosprocedimentos = _controller.TodosTipoProcedimentos();
            foreach (var todosprocedimento in todosprocedimentos)
            {
                if (list.ToString() != "")
                    list.Append(";");
                list.Append(todosprocedimento.Id + ":" + todosprocedimento.Nome);
            }
            string teste = list.ToString();
            return list.ToString();

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void SalvarProcedimento(string procedimento, string data, string pacienteId)
        {
            _controller.IncluirProcedimentoAPaciente(procedimento, data, pacienteId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void ExcluirProcedimento(string procedimentoId, string pacienteId)
        {
            _controller.ExcluirProcedimento(procedimentoId, pacienteId);
        }

        #region gridProcedimento

        public List<Procedimentos> ListProcedimento()
        {

            var pacientId = (int)Session["pacienteId"];
            var list = _controller.TodosProcedimentosDoPaciente(pacientId).Select(item => new Procedimentos
            {
                DataProcedimento = item.DataProcedimento.ToShortDateString(),
                Nome = item.TipoProcedimento.Nome,
                Id = item.Id
            }).ToList();
            return list;
        }
        public class Procedimentos
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string DataProcedimento { get; set; }

        }
        public IEnumerable<Procedimentos> OrderByJqGrid(string sidx, string sord, List<Procedimentos> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(Procedimentos).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult IncluirProcedimentoAPaciente(string Id, string Nome, string DataProcedimento)
        {
            var second = new SecondMethod();
            var pacienteId = Session["pacienteId"].ToString();
            _controller.IncluirProcedimentoAPaciente(Nome, DataProcedimento, pacienteId);

            return Json(second, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CarregarProcedimentos(string sidx, string sord, int page, int rows)
        {
            List<Procedimentos> list;
            list = OrderByJqGrid(sidx, sord, ListProcedimento()).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
            {
                total = totalPages,
                page = page,
                records = iRowCount,
                rows = new RowElement[iCurrencyRows]
            };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[3] };
                second.rows[currentRow].cell[0] = list[iCount].Id.ToString();
                second.rows[currentRow].cell[1] = list[iCount].Nome;
                second.rows[currentRow].cell[2] = list[iCount].DataProcedimento;

                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Antimicrobianos

        public ActionResult Antimicrobianos(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            return View(paciente);
        }

        #region gridAntimicrobiano

        public List<Antimicrobiano> ListAntimicrobiano()
        {
            var list = new List<Antimicrobiano>();
            var antimicrobiano = new Antimicrobiano { Id = 1, Nome = "NOVALEX 50%", Periodo = "1", TipoPeriodo = "Dias" };
            var antimicrobiano2 = new Antimicrobiano { Id = 2, Nome = "BACTRIM", Periodo = "2", TipoPeriodo = "Meses" };
            list.Add(antimicrobiano);
            list.Add(antimicrobiano2);
            return list;
        }


        public class Antimicrobiano
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Periodo { get; set; }
            public string TipoPeriodo { get; set; }

        }

        public IEnumerable<Antimicrobiano> OrderByJqGrid(string sidx, string sord, List<Antimicrobiano> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(Antimicrobiano).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult IncluirAntimicrobianoAPaciente(string Id, string Nome, string Periodo)
        {
            var second = new SecondMethod();
            return Json(second, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarAntimicrobianos(string sidx, string sord, int page, int rows)
        {
            List<Antimicrobiano> list;
            list = OrderByJqGrid(sidx, sord, ListAntimicrobiano()).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
            {
                total = totalPages,
                page = page,
                records = iRowCount,
                rows = new RowElement[iCurrencyRows]
            };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[4] };
                second.rows[currentRow].cell[0] = list[iCount].Id.ToString();
                second.rows[currentRow].cell[1] = list[iCount].Nome;
                second.rows[currentRow].cell[2] = list[iCount].Periodo;
                second.rows[currentRow].cell[3] = list[iCount].TipoPeriodo;
                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Medicamentos

        public ActionResult Medicamentos(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        #region gridMedicamento

        public List<Medicamento> ListMedicamento()
        {
            var list = new List<Medicamento>();
            var Medicamento = new Medicamento { Id = 1, Nome = "NOVALGINA", Periodo = "1", TipoPeriodo = "Dias" };
            var Medicamento2 = new Medicamento { Id = 2, Nome = "ASPIRINA", Periodo = "2", TipoPeriodo = "Meses" };
            list.Add(Medicamento);
            list.Add(Medicamento2);
            return list;
        }


        public class Medicamento
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Periodo { get; set; }
            public string TipoPeriodo { get; set; }

        }

        public IEnumerable<Medicamento> OrderByJqGrid(string sidx, string sord, List<Medicamento> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(Medicamento).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult IncluirMedicamentoAPaciente(string Id, string Nome, string Periodo)
        {
            var second = new SecondMethod();
            return Json(second, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarMedicamentos(string sidx, string sord, int page, int rows)
        {
            List<Medicamento> list;
            list = OrderByJqGrid(sidx, sord, ListMedicamento()).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
            {
                total = totalPages,
                page = page,
                records = iRowCount,
                rows = new RowElement[iCurrencyRows]
            };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[4] };
                second.rows[currentRow].cell[0] = list[iCount].Id.ToString();
                second.rows[currentRow].cell[1] = list[iCount].Nome;
                second.rows[currentRow].cell[2] = list[iCount].Periodo;
                second.rows[currentRow].cell[3] = list[iCount].TipoPeriodo;

                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Exames

        public ActionResult Exames(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        #region gridExame

        public List<Exame> ListExame()
        {
            var list = new List<Exame>();
            var Exame = new Exame { Id = 1, TipoExame = "Laboratorio", Resultado = "Resultado", DataExame = "20/10/2012" };
            var Exame2 = new Exame { Id = 1, TipoExame = "Laboratorio", Resultado = "Resultado", DataExame = "20/10/2012" };
            list.Add(Exame);
            list.Add(Exame2);
            return list;
        }


        public class Exame
        {
            public int Id { get; set; }
            public string TipoExame { get; set; }
            public string Resultado { get; set; }
            public string DataExame { get; set; }

        }

        public IEnumerable<Exame> OrderByJqGrid(string sidx, string sord, List<Exame> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(Exame).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult IncluirExameAPaciente(string Id, string Nome, string Periodo)
        {
            var second = new SecondMethod();
            return Json(second, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarExames(string sidx, string sord, int page, int rows)
        {
            List<Exame> list;
            list = OrderByJqGrid(sidx, sord, ListExame()).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
            {
                total = totalPages,
                page = page,
                records = iRowCount,
                rows = new RowElement[iCurrencyRows]
            };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[4] };
                second.rows[currentRow].cell[0] = list[iCount].Id.ToString();
                second.rows[currentRow].cell[1] = list[iCount].TipoExame;
                second.rows[currentRow].cell[2] = list[iCount].Resultado;
                second.rows[currentRow].cell[3] = list[iCount].DataExame;

                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Hemotransfusão

        public ActionResult Hemotrasnfusao(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        #endregion

        #region Colonização por MDR

        public ActionResult ColonizacaoMdr(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        #endregion

        #region Receituario
        public ActionResult Receituarios(int? id)
        {
            int idPaciente = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(idPaciente);
            Session["pacienteId"] = idPaciente;
            return View(paciente);
        }

        #region gridReceituario

        public List<Receituario> ListReceituario()
        {
            var list = new List<Receituario>();
            var Receituario = new Receituario { Id = 1, Nome = "Novalgina", Dose = "1", TipoDose = "ml", Apresentacao = "1", TipoApresentacao = "Gota", Via = "Articular", Frequencia = "Em caso de Diarréia", Duracao = "1", TipoDuaracao = "Meses" };
            var Receituario2 = new Receituario { Id = 1, Nome = "Novalgina", Dose = "1", TipoDose = "ml", Apresentacao = "1", TipoApresentacao = "Gota", Via = "Articular", Frequencia = "Em caso de Diarréia", Duracao = "1", TipoDuaracao = "Meses" };
            list.Add(Receituario);
            list.Add(Receituario2);
            return list;
        }


        public class Receituario
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Dose { get; set; }
            public string TipoDose { get; set; }
            public string Apresentacao { get; set; }
            public string TipoApresentacao { get; set; }
            public string Via { get; set; }
            public string Frequencia { get; set; }
            public string Duracao { get; set; }
            public string TipoDuaracao { get; set; }

        }

        public IEnumerable<Receituario> OrderByJqGrid(string sidx, string sord, List<Receituario> pessoaList)
        {
            PropertyInfo propertyInfo = typeof(Receituario).GetProperty(sidx);
            if (propertyInfo != null)
            {
                return String.Compare(sord, "desc", StringComparison.Ordinal) == 0
                           ? (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null) descending
                              select x)
                           : (from x in pessoaList
                              orderby propertyInfo.GetValue(x, null)
                              select x);
            }

            return pessoaList;
        }
        public ActionResult IncluirReceituarioAPaciente(string Id, string Nome, string Periodo)
        {
            var second = new SecondMethod();
            return Json(second, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CarregarReceituarios(string sidx, string sord, int page, int rows)
        {
            List<Receituario> list;
            list = OrderByJqGrid(sidx, sord, ListReceituario()).ToList();

            var iRowCount = list.Count;
            var iRest = iRowCount % rows;
            int iCurrencyRows;

            var totalPages = rows > iRowCount
                                 ? 1
                                 : rows <= iRowCount ? (iRowCount / rows) + (iRest == 0 ? 0 : 1) : iRowCount;

            if ((iRowCount / rows) == 0)
            {
                iCurrencyRows = iRowCount;
            }
            else if ((totalPages).Equals(page))
            {
                iCurrencyRows = iRest == 0 ? rows : iRest;
            }
            else
            {
                iCurrencyRows = rows;
            }

            if (iRowCount < rows)
            {
                rows = iRowCount;
            }

            var second = new SecondMethod
            {
                total = totalPages,
                page = page,
                records = iRowCount,
                rows = new RowElement[iCurrencyRows]
            };

            int iCount = ((rows * page) - rows);

            for (int currentRow = 0; currentRow < iCurrencyRows; currentRow++)
            {
                second.rows[currentRow] = new RowElement { id = iCount + 1, cell = new string[10] };
                second.rows[currentRow].cell[0] = list[iCount].Id.ToString();
                second.rows[currentRow].cell[1] = list[iCount].Nome;
                second.rows[currentRow].cell[2] = list[iCount].Dose;
                second.rows[currentRow].cell[3] = list[iCount].TipoDose;
                second.rows[currentRow].cell[4] = list[iCount].Apresentacao;
                second.rows[currentRow].cell[5] = list[iCount].TipoApresentacao;
                second.rows[currentRow].cell[6] = list[iCount].Via;
                second.rows[currentRow].cell[7] = list[iCount].Frequencia;
                second.rows[currentRow].cell[8] = list[iCount].Duracao;
                second.rows[currentRow].cell[9] = list[iCount].TipoDuaracao;

                iCount++;
            }

            return Json(second, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #endregion

        #region Paginação do grid

        public class RowElement
        {
            public string[] cell;
            public int id;
        }

        public class SecondMethod
        {
            public int page;
            public int records;
            public RowElement[] rows;
            public int total;
        }

        #endregion
    }
}
