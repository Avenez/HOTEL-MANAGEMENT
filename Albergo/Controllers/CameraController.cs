using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    public class CameraController : Controller
    {
        // GET: Camera
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCamera()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateCamera(Camera Ca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Camera.InserisciNuovaCamera(Ca);
                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Inserimento Camera avvenuto con Successo";
                    return RedirectToAction("Backoffice", "Home");
                }
                else
                {
                    return View(Ca);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }

        }

        [HttpGet]
        public ActionResult ListCamere()
        {
            List<Camera> listCamere = Camera.GetListaCamere();
            ViewBag.listCamere = listCamere;

            return View();
        }


        [HttpGet]
        public ActionResult EditCamera(int idCamera)
        {

            Camera Camera = Camera.GetCameraById(idCamera);

            return View(Camera);
        }

        [HttpPost]
        public ActionResult EditCamera(Camera Ca)
        {

            try
            {
                ModelState.Remove("NumCamera");
                if (ModelState.IsValid)
                {
                    Camera.AggiornaCamera(Ca);

                    return RedirectToAction("ListCamere", "Camera");
                }
                else
                {
                    return View(Ca);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }



        }


        [HttpGet]
        public ActionResult DelCamera(int idCamera)
        {
            Camera.EliminaCamera(idCamera);

            return RedirectToAction("ListCamere", "Camera");
        }

    }
}