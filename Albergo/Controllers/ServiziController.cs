using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiziController : Controller
    {
        // GET: Servizi
        public ActionResult Index()
        {
            return View();
        }


        //Action che recupera una lista di servizi per popolare la dropdown
        //Recupera anche tutti i servizi aggiuntivi di una data prenotazione e la prenotazione stessa mediante il metodo nel model
        [HttpGet]
        public ActionResult ListServizi(int idPrenotazione) 
        {
            List<Servizio> listServizi = Servizio.GetServizi();
           
            SelectList dropListServizi = new SelectList(listServizi, "Descrizione", "Descrizione");
            ViewBag.dropListServizi = dropListServizi;


            ElementoListaPrenotazioni newElemento = ElementoListaPrenotazioni.GetElementoListaPrenotazioneById(idPrenotazione);
            ViewBag.Prenotazione = newElemento;
            List<ServizioAggiuntivo> listServiziAggiunti = ServizioAggiuntivo.GetListaServiziAggiuntivi(idPrenotazione);
            ViewBag.listServiziAggiunti = listServiziAggiunti;


            return View();
        }

        //Action che aggiunge un servizio aggiuntivo alla prenotazione mediante il metodo nel model
        [HttpPost]
        public ActionResult ListServizi(ServizioAggiuntivo Se)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServizioAggiuntivo.InsertServizioAggiuntivo(Se);


                    return RedirectToAction("ListServizi", "Servizi", new { idPrenotazione = Se.idPrenotazione });
                }
                else
                {
                    return View(Se);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }

        }

        //Action che elimina il servizio aggiuntivo mediante il metodo nel model
        [HttpGet]
        public ActionResult DelServizio(int idPrenotazione, int idSagg)
        {
            System.Diagnostics.Debug.WriteLine("idP " + idPrenotazione);
            System.Diagnostics.Debug.WriteLine("idSagg " + idSagg);
   
            ServizioAggiuntivo.CancellaServiziAggiuntiviIdDateDe(idSagg);


            return RedirectToAction("ListServizi", "Servizi", new { idPrenotazione = idPrenotazione });
        }
    }
}