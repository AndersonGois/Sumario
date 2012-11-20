using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SumarioDeAlta.Controller;
using SumarioDeAlta.Domain.Entities;


namespace SumarioDeAlta.Mvc.Controllers
{
    public class DadosGeraisController : System.Web.Mvc.Controller
    {
        private PacientesController _controller = new PacientesController();

        public ActionResult DadosGerais(int? id)
        {
            int teste = id.HasValue ? id.Value : 0;
            var paciente = _controller.ObterPaciente(teste);
            //IEnumerable<SelectListItem> items =
            //    _controller.TodosPacientes().Select(x => new SelectListItem {Text = x.Nome, Value = x.Id.ToString()});
            //ViewBag.test = items;

            return View(paciente);
        }

       

        public ActionResult Enviar(int id)
        {
            return RedirectToAction("DadosGerais");
        }


        [HttpPost]
        public ActionResult DadosGerais(Paciente paciente)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("DadosGeraisrrr");
            }
            catch
            {
                return Redirect("DadosGerais");
            }
        }

    }
}
