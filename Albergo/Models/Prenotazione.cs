using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Albergo.Models
{
    public class Prenotazione
    {
        [ScaffoldColumn(false)]
        public int idPrenotazione { get; set; }

        [Required]
        [Display(Name = "Selezionare un Cliente")]
        public int idCliente { get; set; }

        [Required]
        [Display(Name = "Selezionare una Camera")]
        public int NumCamera { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataPrenotazione { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Inizio Soggiorno")]

        public DateTime DataInizio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Fine Soggiorno")]
        public DateTime DataFine { get; set;}

        [ScaffoldColumn(false)]
        public int Anno { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il Prezzo deve contenere solo numeri.")]
        [Display(Name = "Caparra")]
        [Range(1,500 , ErrorMessage = "L'a caparra minima è di 1 €")]
        public decimal Caparra { get; set; }


        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il Prezzo deve contenere solo numeri.")]
        [Display(Name = "Costo")]
        public decimal Costo { get; set; }

        [Required]
        [Display(Name = "Selezionare un tipo di soggiorno")]
        public string TipoSoggiorno { get; set; }

        public Prenotazione() { }

        // Costruttore con ID
        public Prenotazione(int idPrenotazione, int idCliente, int numCamera, DateTime dataPrenotazione ,DateTime dataInizio, DateTime dataFine, int anno, decimal caparra ,decimal costo, string tipoSoggiorno)
        {
            this.idPrenotazione = idPrenotazione;
            this.idCliente = idCliente;
            NumCamera = numCamera;
            DataPrenotazione = dataPrenotazione;
            DataInizio = dataInizio;
            DataFine = dataFine;
            Anno = anno;
            Caparra = caparra;
            Costo = costo;
            TipoSoggiorno = tipoSoggiorno;
        }

        // Metodo per inserire una nuova prenotazione nella tabella Prenotazione
        public static void InserisciNuovaPrenotazione(Prenotazione nuovaPrenotazione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Prenotazione (idCliente, NumCamera, DataPrenotazione,DataInizio, DataFine, Anno, Caparra, Costo, TipoSoggiorno) VALUES (@idCliente, @NumCamera, @DataPrenotazione, @DataInizio, @DataFine, @Anno, @Caparra, @Costo, @TipoSoggiorno)", conn);
                cmd.Parameters.AddWithValue("@idCliente", nuovaPrenotazione.idCliente);
                cmd.Parameters.AddWithValue("@NumCamera", nuovaPrenotazione.NumCamera);
                cmd.Parameters.AddWithValue("@DataPrenotazione", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("@DataInizio", nuovaPrenotazione.DataInizio.ToShortDateString());
                cmd.Parameters.AddWithValue("@DataFine", nuovaPrenotazione.DataFine.ToShortDateString());
                cmd.Parameters.AddWithValue("@Anno", (int)DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@Caparra", nuovaPrenotazione.Caparra);
                cmd.Parameters.AddWithValue("@Costo", nuovaPrenotazione.Costo);
                cmd.Parameters.AddWithValue("@TipoSoggiorno", nuovaPrenotazione.TipoSoggiorno);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Inserimento avvenuto con successo");
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

        // Metodo per ottenere una lista di prenotazioni dalla tabella Prenotazione
        public static List<Prenotazione> GetListPrenotazioni()
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Prenotazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Prenotazione prenotazione = new Prenotazione
                    {
                        idPrenotazione = Convert.ToInt32(reader["idPrenotazione"]),
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        NumCamera = Convert.ToInt32(reader["NumCamera"]),
                        DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                        DataInizio = Convert.ToDateTime(reader["DataInizio"]),
                        DataFine = Convert.ToDateTime(reader["DataFine"]),
                        Anno = Convert.ToInt32(reader["Anno"]),
                        Caparra = Convert.ToDecimal(reader["Caparra"]),
                        Costo = Convert.ToDecimal(reader["Costo"]),
                        TipoSoggiorno = reader["TipoSoggiorno"].ToString()
                    };
                    prenotazioni.Add(prenotazione);
                }
            }

            return prenotazioni;
        }

        //Metodo per la cancellazione di una prenotazione attraverso l'ID
        public static void CancellaPrenotazione(int idPrenotazione)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Prenotazione WHERE idPrenotazione = @idPrenotazione", conn);
                cmd.Parameters.AddWithValue("@idPrenotazione", idPrenotazione);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Cancellazione della prenotazione avvenuta con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessuna prenotazione trovata con l'ID specificato");
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

        //Metodo per l'aggiornamento di una prenotazione
        public static void AggiornaPrenotazione(Prenotazione prenotazioneDaAggiornare)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Prenotazione SET idCliente = @IdCliente, NumCamera = @NumCamera, DataInizio = @DataInizio, DataFine = @DataFine, Anno = @Anno, Caparra = @Caparra, Costo = @Costo, TipoSoggiorno = @TipoSoggiorno WHERE idPrenotazione = @IdPrenotazione", conn);
                cmd.Parameters.AddWithValue("@IdCliente", prenotazioneDaAggiornare.idCliente);
                cmd.Parameters.AddWithValue("@NumCamera", prenotazioneDaAggiornare.NumCamera);
                cmd.Parameters.AddWithValue("@DataInizio", prenotazioneDaAggiornare.DataInizio);
                cmd.Parameters.AddWithValue("@DataFine", prenotazioneDaAggiornare.DataFine);
                cmd.Parameters.AddWithValue("@Anno", (int)DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@Caparra", prenotazioneDaAggiornare.Caparra);
                cmd.Parameters.AddWithValue("@Costo", prenotazioneDaAggiornare.Costo);
                cmd.Parameters.AddWithValue("@TipoSoggiorno", prenotazioneDaAggiornare.TipoSoggiorno);
                cmd.Parameters.AddWithValue("@IdPrenotazione", prenotazioneDaAggiornare.idPrenotazione);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Aggiornamento avvenuto con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessuna riga aggiornata");
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

        //Metodo per il recupero di una prenotazione tramite ID
        public static Prenotazione GetPrenotazioneById(int id)
        {
            Prenotazione prenotazione = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prenotazione WHERE idPrenotazione = @IdPrenotazione", conn);
                cmd.Parameters.AddWithValue("@IdPrenotazione", id);

                SqlDataReader reader = cmd.ExecuteReader();
                                                  
                if (reader.Read())
                {
                    prenotazione = new Prenotazione
                    {
                        idPrenotazione = (int)reader["idPrenotazione"],
                        idCliente = (int)reader["idCliente"],
                        NumCamera = (int)reader["NumCamera"],
                        DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                        DataInizio = (DateTime)reader["DataInizio"],
                        DataFine = (DateTime)reader["DataFine"],
                        Anno = Convert.ToInt32(reader["Anno"]),
                        Caparra = (decimal)reader["Caparra"],
                        Costo = (decimal)reader["Costo"],
                        TipoSoggiorno = reader["TipoSoggiorno"].ToString()
                    };
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

            return prenotazione;
        }


    }
}