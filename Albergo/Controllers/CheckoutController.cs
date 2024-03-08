using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CheckoutController : Controller
    {
        //Action che recupera un ElementoListaPrenotazioni e le lista di servizi aggiuntivi per calcolare il totale e mostrare tutto a video
        public ActionResult Index(int idPrenotazione)
        {


            ElementoListaPrenotazioni newElemento = ElementoListaPrenotazioni.GetElementoListaPrenotazioneById(idPrenotazione);
            ViewBag.Prenotazione = newElemento;
            List<ServizioAggiuntivo> listServiziAggiunti = ServizioAggiuntivo.GetListaServiziAggiuntivi(idPrenotazione);
            ViewBag.listServiziAggiunti = listServiziAggiunti;


            return View();
        }


        //Action che esegue il Checkout 
        //1)Recupera la prenotazione
        //2)Recupera l'id della Camera
        //3)Porta la camera a "Disponibile"
        //4)Cancella la prenotazione
        //5)Cancella i servizi aggiuntivi relativi alla prenotazione
        //6) Da un feed dell'avvenuto checkout
        [HttpGet]
        public ActionResult CheckoutAction(int idPrenotazione)
        {

            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }
        }



    }
}