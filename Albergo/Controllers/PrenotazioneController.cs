using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PrenotazioneController : Controller
    {
        // GET: Prenotazione

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreatePrenotazione()
        {
            try
            {
                List<Cliente> listClienti = Cliente.GetListaClienti();
                SelectList dropListClienti = new SelectList(listClienti, "IdCliente", "FullCliente");
                ViewBag.dropListClienti = dropListClienti;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            try
            {
                List<Camera> listCamere = Camera.GetListaCamereDisp();
                SelectList dropListCamere = new SelectList(listCamere, "IdCamera", "NumCamera");
                ViewBag.dropListCamere = dropListCamere;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrenotazione(Prenotazione P)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Prenotazione.InserisciNuovaPrenotazione(P);
                    int idCamera = Camera.GetIdCameraFromNumCamera(P.NumCamera);
                    Camera.UpdateDispCamera(idCamera, false);
                    
                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Inserimento Prenotazione avvenuto con Successo";
                    return RedirectToAction("Backoffice", "Home");
                }
                else
                {
                    return View(P);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }

        }


        [HttpGet]
        public ActionResult ListPrenotazioni()
        {
            List<ElementoListaPrenotazioni> listPrenotazioni = ElementoListaPrenotazioni.GetElementiListaPrenotazioni();
            ViewBag.listPrenotazioni = listPrenotazioni;

            return View();
        }


        [HttpGet]
        public ActionResult EditPrenotazione(int idPrenotazione)
        {

            Prenotazione Prenotazione = Prenotazione.GetPrenotazioneById(idPrenotazione);
            int idCamera = Camera.GetIdCameraFromNumCamera(Prenotazione.NumCamera);
            Camera.UpdateDispCamera(idCamera, true);
            try
            {
                List<Camera> listCamere = Camera.GetListaCamereDisp();
                SelectList dropListCamere = new SelectList(listCamere, "IdCamera", "NumCamera");
                ViewBag.dropListCamere = dropListCamere;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return View(Prenotazione);
        }

        [HttpPost]
        public ActionResult EditPrenotazione(Prenotazione P)
        {
            System.Diagnostics.Debug.WriteLine(P.NumCamera);
            try
            {
                if (ModelState.IsValid)
                {
                    int idCamera = Camera.GetIdCameraFromNumCamera(P.NumCamera);
                    Camera.UpdateDispCamera(idCamera, false);
                    Prenotazione.AggiornaPrenotazione(P);

                    return RedirectToAction("ListPrenotazioni", "Prenotazione");
                }
                else
                {
                    return View(P);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }



        }


        [HttpGet]
        public ActionResult DelPrenotazione(int idPrenotazione)
        {
            ServizioAggiuntivo.CancellaServiziAggiuntivi(idPrenotazione);
            int idCamera = Camera.GetIdCameraFromNumCamera(idPrenotazione);
            Prenotazione.CancellaPrenotazione(idPrenotazione);

            Camera.UpdateDispCamera(idCamera, true);

            return RedirectToAction("ListPrenotazioni", "Prenotazione");
        }


    }
}