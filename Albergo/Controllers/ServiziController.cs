using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    public class ServiziController : Controller
    {
        // GET: Servizi
        public ActionResult Index()
        {
            return View();
        }

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


        [HttpGet]
        public ActionResult DelServizio(int idPrenotazione, DateTime Data, string Descrizione)
        {
            ServizioAggiuntivo.CancellaServiziAggiuntiviIdDateDe(idPrenotazione, Data, Descrizione);


            return RedirectToAction("ListServizi", "Servizi", new { idPrenotazione = idPrenotazione });
        }
    }
}