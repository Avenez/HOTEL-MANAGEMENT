using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Albergo.Models;

namespace Albergo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }





        [HttpGet]
        public ActionResult Login() { return View(); }


        //Metodo di login che controlla se lo username e la password siano presenti sul db in modo da fare l'autenticazione tramite FormsAuthentication.SetAuthCookie(U.Username, false);
        [HttpPost]
        public ActionResult Login([Bind(Exclude = "Ruolo, IdUser")] Utente U)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM Utenti WHERE Username = @Username AND Password = @Password ";
            

            try
            { 

            conn.Open();
            SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@Username", U.Username );
                comm.Parameters.AddWithValue("@Password", U.Password );
                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(U.Username, false);
                    return RedirectToAction("Index" , "Home");
                }
                else
                {
                   
                }

            }
            catch(Exception ex)
            { 
                System.Diagnostics.Debug.WriteLine("Errore Login" + ex.Message);
            }
            finally 
            { conn.Close(); }
            return View();
        
        }




        //LogOut con FormsAuthentication.SignOut();
        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
            
        }
    }
}