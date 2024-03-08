using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Albergo.Models
{
    public class ElementoListaPrenotazioni
    {
        public int idPrenotazione { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public string Email { get; set; }


        public int NumCamera { get; set; }


        public DateTime DataPrenotazione { get; set; }

        public DateTime DataInizio { get; set; }


        public DateTime DataFine { get; set; }

        public decimal Caparra { get; set; }


        public decimal Costo { get; set; }

        public string TipoSoggiorno { get; set; }

        public ElementoListaPrenotazioni() { }


        public ElementoListaPrenotazioni(int idPrenotazione, string nome, string cognome, string email, int numCamera, DateTime dataPrenotazione, DateTime dataInizio, DateTime dataFine, decimal caparra, decimal costo, string tipoSoggiorno)
        {
            this.idPrenotazione = idPrenotazione;
            Nome = nome;
            Cognome = cognome;
            Email = email;
            NumCamera = numCamera;
            DataPrenotazione = dataPrenotazione;
            DataInizio = dataInizio;
            DataFine = dataFine;
            Caparra = caparra;
            Costo = costo;
            TipoSoggiorno = tipoSoggiorno;
        }

        //Metodo per la creazione di una Lista di ElementoListaPrenotazioni
        public static List<ElementoListaPrenotazioni> GetElementiListaPrenotazioni()
        {
            List<ElementoListaPrenotazioni> elementiListaPrenotazioni = new List<ElementoListaPrenotazioni>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Prenotazione.idPrenotazione, Cliente.Nome, Cliente.Cognome, Cliente.Email, Prenotazione.NumCamera, Prenotazione.DataPrenotazione, Prenotazione.DataInizio, Prenotazione.DataFine, Prenotazione.Caparra, Prenotazione.Costo, Prenotazione.TipoSoggiorno FROM Prenotazione LEFT JOIN Cliente ON Prenotazione.idCliente = Cliente.idCliente", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    elementiListaPrenotazioni.Add(new ElementoListaPrenotazioni(
                        (int)reader["idPrenotazione"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["Email"].ToString(),
                        (int)reader["NumCamera"],
                        (DateTime)reader["DataPrenotazione"],
                        (DateTime)reader["DataInizio"],
                        (DateTime)reader["DataFine"],
                        (decimal)reader["Caparra"],
                        (decimal)reader["Costo"],
                        reader["TipoSoggiorno"].ToString()
                    ));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //Restituisce una lista di ElementoListaPrenotazioni
            return elementiListaPrenotazioni;
        }

        //Metodo per la selezione di un ElementoListaPrenotazioni attraverso il suo id
        public static ElementoListaPrenotazioni GetElementoListaPrenotazioneById(int idPrenotazione)
        {
            ElementoListaPrenotazioni elementoListaPrenotazione = null;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Prenotazione.idPrenotazione, Cliente.Nome, Cliente.Cognome, Cliente.Email, Prenotazione.NumCamera, Prenotazione.DataPrenotazione, Prenotazione.DataInizio, Prenotazione.DataFine, Prenotazione.Caparra, Prenotazione.Costo, Prenotazione.TipoSoggiorno FROM Prenotazione LEFT JOIN Cliente ON Prenotazione.idCliente = Cliente.idCliente WHERE Prenotazione.idPrenotazione = @IdPrenotazione", conn);
                cmd.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    elementoListaPrenotazione = new ElementoListaPrenotazioni(
                        (int)reader["idPrenotazione"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["Email"].ToString(),
                        (int)reader["NumCamera"],
                        (DateTime)reader["DataPrenotazione"],
                        (DateTime)reader["DataInizio"],
                        (DateTime)reader["DataFine"],
                        (decimal)reader["Caparra"],
                        (decimal)reader["Costo"],
                        reader["TipoSoggiorno"].ToString()
                    );
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return elementoListaPrenotazione;
        }


        //Metodo per la creazione di una Lista di ElementoListaPrenotazioni dove si ha PENSIONE COMPLETA
        public static List<ElementoListaPrenotazioni> GetElementiListaPrenotazioniPensione()
        {
            List<ElementoListaPrenotazioni> elementiListaPrenotazioni = new List<ElementoListaPrenotazioni>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                string pensione = "Pensione Completa";
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Prenotazione.idPrenotazione, Cliente.Nome, Cliente.Cognome, Cliente.Email, Prenotazione.NumCamera, Prenotazione.DataPrenotazione, Prenotazione.DataInizio, Prenotazione.DataFine, Prenotazione.Caparra, Prenotazione.Costo, Prenotazione.TipoSoggiorno FROM Prenotazione LEFT JOIN Cliente ON Prenotazione.idCliente = Cliente.idCliente WHERE TipoSoggiorno = @TipoSoggiorno", conn);
                cmd.Parameters.AddWithValue("@TipoSoggiorno", pensione);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    elementiListaPrenotazioni.Add(new ElementoListaPrenotazioni(
                        (int)reader["idPrenotazione"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["Email"].ToString(),
                        (int)reader["NumCamera"],
                        (DateTime)reader["DataPrenotazione"],
                        (DateTime)reader["DataInizio"],
                        (DateTime)reader["DataFine"],
                        (decimal)reader["Caparra"],
                        (decimal)reader["Costo"],
                        reader["TipoSoggiorno"].ToString()
                    ));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return elementiListaPrenotazioni;
        }


        //Metodo che restituisce una lista di ElementoListaPrenotazioni cercando per CF
        public static List<ElementoListaPrenotazioni> GetElementiListaPrenotazioniCF(string CF)
        {

            System.Diagnostics.Debug.WriteLine("CF" + CF);
            List<ElementoListaPrenotazioni> elementiListaPrenotazioni = new List<ElementoListaPrenotazioni>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Prenotazione.idPrenotazione, Cliente.Nome, Cliente.Cognome, Cliente.Email, Prenotazione.NumCamera, Prenotazione.DataPrenotazione, Prenotazione.DataInizio, Prenotazione.DataFine, Prenotazione.Caparra, Prenotazione.Costo, Prenotazione.TipoSoggiorno FROM Prenotazione LEFT JOIN Cliente ON Prenotazione.idCliente = Cliente.idCliente WHERE Cliente.CF = @CF", conn);
                cmd.Parameters.AddWithValue("@CF", CF);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    elementiListaPrenotazioni.Add(new ElementoListaPrenotazioni(
                        (int)reader["idPrenotazione"],
                        reader["Nome"].ToString(),
                        reader["Cognome"].ToString(),
                        reader["Email"].ToString(),
                        (int)reader["NumCamera"],
                        (DateTime)reader["DataPrenotazione"],
                        (DateTime)reader["DataInizio"],
                        (DateTime)reader["DataFine"],
                        (decimal)reader["Caparra"],
                        (decimal)reader["Costo"],
                        reader["TipoSoggiorno"].ToString()
                    ));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return elementiListaPrenotazioni;
        }


    }
}