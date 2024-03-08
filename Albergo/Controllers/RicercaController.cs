using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Albergo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RicercaController : Controller
    {
        // GET: Ricerca
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Ricerca() 
        {
            List<ElementoListaPrenotazioni> ListPensioniComplete = ElementoListaPrenotazioni.GetElementiListaPrenotazioniPensione();
            ViewBag.ListPensioniComplete = ListPensioniComplete;

            return View();
        }

        [HttpGet]
        public JsonResult Ricerca2(string CF) 
        {
            List<ElementoListaPrenotazioni> ListaPrenotazioniCF = ElementoListaPrenotazioni.GetElementiListaPrenotazioniCF(CF);

            var jsonData = ListaPrenotazioniCF;

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}