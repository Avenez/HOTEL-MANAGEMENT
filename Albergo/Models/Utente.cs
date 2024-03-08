using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Albergo.Models
{
    public class Utente
    {
        [ScaffoldColumn(false)]
        public int IdUser { get; set; }

        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public string Ruolo { get; set; }

        public Utente() { }


        public Utente(string username, string password, string ruolo)
        {
            this.Username = username;
            this.Password = password;
            this.Ruolo = ruolo;
        }
    }
}