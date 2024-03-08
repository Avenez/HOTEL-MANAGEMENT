using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Albergo.Models
{
    public class ServizioAggiuntivo
    {
        [Required]
        [Display(Name = "Scegliere un servizio")]
        public string Descrizione { get; set; }


        [ScaffoldColumn(false)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Aggiungere una quantità")]
        [Range(1, 100)]
        public int Qta { get; set; }

        [Required]
        [Display(Name = "Inserire un costo")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo deve contenere solo numeri.")]
        public decimal Prezzo { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo deve contenere solo numeri.")]
        public int idPrenotazione { get; set; }

        public ServizioAggiuntivo() { }


        public ServizioAggiuntivo(string descrizione, DateTime Data, int Qta, decimal Prezzo, int idPrenotazione)
        {
            Descrizione = descrizione;
            this.Data = Data;
            this.Qta = Qta;
            this.Prezzo = Prezzo;
            this.idPrenotazione = idPrenotazione;
        }


        
            public static void InsertServizioAggiuntivo(ServizioAggiuntivo nuovoServizioAggiuntivo)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ServiziAggiunti (Descrizione, Data, Qta, Prezzo, idPrenotazione) VALUES (@Descrizione, @Data, @Qta, @Prezzo, @idPrenotazione)", conn);
                    cmd.Parameters.AddWithValue("@Descrizione", nuovoServizioAggiuntivo.Descrizione);
                    cmd.Parameters.AddWithValue("@Data", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Qta", nuovoServizioAggiuntivo.Qta);
                    cmd.Parameters.AddWithValue("@Prezzo", nuovoServizioAggiuntivo.Prezzo);
                    cmd.Parameters.AddWithValue("@idPrenotazione", nuovoServizioAggiuntivo.idPrenotazione);

                    cmd.ExecuteNonQuery();

                    System.Diagnostics.Debug.WriteLine("Inserimento del nuovo Servizio Aggiuntivo avvenuto con successo");
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

            public static List<ServizioAggiuntivo> GetListaServiziAggiuntivi(int idPrenotazione)
            {
                List<ServizioAggiuntivo> serviziAggiuntivi = new List<ServizioAggiuntivo>();

                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ServiziAggiunti WHERE idPrenotazione = @idPrenotazione", conn);
                    cmd.Parameters.AddWithValue("@idPrenotazione", idPrenotazione);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        ServizioAggiuntivo servizio = new ServizioAggiuntivo
                        {
                            Descrizione = Convert.ToString(reader["Descrizione"]),
                            Data = Convert.ToDateTime(reader["Data"]),
                            Qta = Convert.ToInt32(reader["Qta"]),
                            Prezzo = Convert.ToDecimal(reader["Prezzo"]),
                            idPrenotazione = Convert.ToInt32(reader["idPrenotazione"])
                        };
                        serviziAggiuntivi.Add(servizio);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                finally
                {
                    conn.Close();
                }

                return serviziAggiuntivi;
            }

        public static void CancellaServiziAggiuntivi(int idPrenotazione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ServiziAggiunti WHERE idPrenotazione = @idPrenotazione", conn);
                cmd.Parameters.AddWithValue("@idPrenotazione", idPrenotazione);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Cancellazione dei servizi aggiuntivi per la prenotazione con ID {idPrenotazione} avvenuta con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessun servizio aggiuntivo trovato per la prenotazione con l'ID specificato");
                }
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


        public static void CancellaServiziAggiuntiviIdDateDe(int idPrenotazione, DateTime data, string descrizione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ServiziAggiunti WHERE idPrenotazione = @idPrenotazione AND Data = @data AND Descrizione = @descrizione", conn);
                cmd.Parameters.AddWithValue("@idPrenotazione", idPrenotazione);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@descrizione", descrizione);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Cancellazione dei servizi aggiuntivi per la prenotazione con ID {idPrenotazione}, data {data} e descrizione {descrizione} avvenuta con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessun servizio aggiuntivo trovato per la prenotazione, data e descrizione specificate");
                }
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






    }
}