using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index(int idPrenotazione)
        {


            ElementoListaPrenotazioni newElemento = ElementoListaPrenotazioni.GetElementoListaPrenotazioneById(idPrenotazione);
            ViewBag.Prenotazione = newElemento;
            List<ServizioAggiuntivo> listServiziAggiunti = ServizioAggiuntivo.GetListaServiziAggiuntivi(idPrenotazione);
            ViewBag.listServiziAggiunti = listServiziAggiunti;


            return View();
        }



        [HttpGet]
        public ActionResult CheckoutAction(int idPrenotazione)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Prenotazione Prenotazione = Prenotazione.GetPrenotazioneById(idPrenotazione);
                    int idCamera = Camera.GetIdCameraFromNumCamera(Prenotazione.NumCamera);
                    Camera.UpdateDispCamera(idCamera, true);
                    Prenotazione.CancellaPrenotazione(idPrenotazione);
                    ServizioAggiuntivo.CancellaServiziAggiuntivi(idPrenotazione);
                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Checkout avvenuto con Successo";
                    return RedirectToAction("Backoffice", "Home");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }
        }



    }
}