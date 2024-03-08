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

        //Action Asincrona per il recupero di tutte le prenotazioni "Pensione Completa"
        [HttpGet]
        public JsonResult Ricerca() 
        {
            System.Diagnostics.Debug.WriteLine("RICERCA");
            List<ElementoListaPrenotazioni> ListPensioniComplete = ElementoListaPrenotazioni.GetElementiListaPrenotazioniPensione();

            return Json(ListPensioniComplete, JsonRequestBehavior.AllowGet);
        }

        //Action Asincrona per il recupero di tutte le prenotazioni CF
        //Non implementata nella view per mancanza di tempo
        [HttpGet]
        public JsonResult Ricerca2(string CF) 
        {
            System.Diagnostics.Debug.WriteLine("CF RICERCA 2" + CF);
            System.Diagnostics.Debug.WriteLine("RICERCA 2");
            List<ElementoListaPrenotazioni> ListaPrenotazioniCF = ElementoListaPrenotazioni.GetElementiListaPrenotazioniCF(CF);

            

            return Json(ListaPrenotazioniCF, JsonRequestBehavior.AllowGet);
        }
    }
}