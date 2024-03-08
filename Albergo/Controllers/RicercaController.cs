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
        public JsonResult Ricerca() 
        {
            System.Diagnostics.Debug.WriteLine("RICERCA");
            List<ElementoListaPrenotazioni> ListPensioniComplete = ElementoListaPrenotazioni.GetElementiListaPrenotazioniPensione();

            return Json(ListPensioniComplete, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult Ricerca2(string CF) 
        {
            List<ElementoListaPrenotazioni> ListaPrenotazioniCF = ElementoListaPrenotazioni.GetElementiListaPrenotazioniCF(CF);

            var jsonData2 = ListaPrenotazioniCF;

            return Json(jsonData2, JsonRequestBehavior.AllowGet);
        }
    }
}