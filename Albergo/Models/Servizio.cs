using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Albergo.Models
{
    public class Servizio
    {
        [ScaffoldColumn(false)]
        public int idServizio { get; set; }

        [Required]
        [Display(Name = "Inserire la descrizione del servizio")]
        [StringLength(5, MinimumLength = 200, ErrorMessage = "Il campo deve contenere almeno 5 caratteri")]
        public string Descrizione { get; set; }


        public Servizio() { }


        public Servizio(string descrizione)
        {
            Descrizione = descrizione;
        }

        // Metodo per inserire un nuovo servizio nella tabella Servizio (NON USATO)
        public static void InserisciNuovoServizio(Servizio nuovoServizio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ListaServizi (Descrizione) VALUES (@Descrizione)", conn);
                cmd.Parameters.AddWithValue("@Descrizione", nuovoServizio.Descrizione);

                cmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine("Inserimento avvenuto con successo");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

        // Metodo per ottenere una lista di servizi dalla tabella Servizio (POPOLAMENTO DROPDOWN)
        public static List<Servizio> GetServizi()
        {
            List<Servizio> servizi = new List<Servizio>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ListaServizi";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Servizio servizio = new Servizio
                    {
                        idServizio = Convert.ToInt32(reader["idServizio"]),
                        Descrizione = reader["Descrizione"].ToString()
                    };
                    servizi.Add(servizio);
                }
            }

            return servizi;
        }

        //Metodo Per l'eliminazione di un servizio (NON USATO)
        public static void EliminaServizio(int idServizio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ListaServizi WHERE idServizio = @idServizio", conn);
                cmd.Parameters.AddWithValue("@idServizio", idServizio);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Eliminazione del servizio con ID {idServizio} avvenuta con successo");
                }
                else
                {
                    Console.WriteLine("Nessun servizio trovato con l'ID specificato");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}