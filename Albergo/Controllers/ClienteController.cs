using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCliente()
        {

            return View();
        }

        //Action che crea un nuovo cliente attraverso il metodo statico nel model e da un feed alla creazione
        [HttpPost]
        public ActionResult CreateCliente(Cliente C)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cliente.InserisciNuovoCliente(C);
                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Inserimento Cliente avvenuto con Successo";
                    return RedirectToAction("Backoffice", "Home");
                }
                else
                {
                    return View(C);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }

        }

        //Action recupera una lista di clienti attraverso il metodo statico nel model
        [HttpGet]
        public ActionResult ListClienti() 
        {
            List<Cliente> listCLienti = Cliente.GetListaClienti();
            ViewBag.listClienti = listCLienti;

            return View();
        }

        //Action recupera un clienti attraverso il metodo statico nel model
        [HttpGet]
        public ActionResult EditCliente(int idCliente)
        {

            Cliente Cliente = Cliente.GetClienteById(idCliente);

            return View(Cliente) ;
        }

        //Action edita un clienti attraverso il metodo statico nel model
        [HttpPost]
        public ActionResult EditCliente(Cliente C)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Cliente.AggiornaCliente(C);

                    return RedirectToAction("ListClienti", "Cliente");
                }
                else
                {
                    return View(C);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }


            
        }

        //Action che elimina un clienti attraverso il metodo statico nel model
        [HttpGet]
        public ActionResult DelCliente (int idCliente) 
        {
            Cliente.EliminaCliente(idCliente);

            return RedirectToAction("ListClienti", "Cliente");
        }

    }
}