using Albergo.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Albergo.Validations.EmailDuplicity;
using static Albergo.Validations.TelDuplicity;
using static Albergo.Validations.CFDuplicity;
using System.Configuration;
using System.Data.SqlClient;

namespace Albergo.Models
{
    public class Cliente
    {
        [ScaffoldColumn(false)]
        public int idCliente { get; set; }

        [Required]
        [Display(Name = "Email Cliente")]
        [EmailAddress]
        [EmailDuplicity(ErrorMessage = "Email inserita già in uso")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome Cliente")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Il campo Nome deve contenere solo lettere.")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Cognome Cliente")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Il campo Cognome deve contenere solo lettere.")]
        public string Cognome { get; set; }

        [Required]
        [Display(Name = "Codice Fiscale")]
        [CFDuplicity(ErrorMessage = "Il CF inserito è già in uso")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il campo Codice Fiscale deve contenere 16 caratteri.")]
        public string CF { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Il campo Codice Fiscale deve contenere 16 caratteri.")]
        public string Citta { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Il campo Codice Fiscale deve contenere 16 caratteri.")]
        public string Provincia { get; set; }

        [Required]
        [TelDuplicity]
        [Display(Name = "Numero di Telefono")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo Numero di Telefono deve contenere solo numeri.")]
        public string Tel { get; set; }


        public string FullCLiente => $"{idCliente} {Nome} {Cognome} {Email} {CF}";

        
        public Cliente () { }

        public Cliente(int IdCliente ,string email, string nome, string cognome, string cf, string citta, string provincia, string tel)
        {
            idCliente = IdCliente;
            Email = email;
            Nome = nome;
            Cognome = cognome;
            CF = cf;
            Citta = citta;
            Provincia = provincia;
            Tel = tel;
        }



        public static void InserisciNuovoCliente(Cliente nuovoCliente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (Email, Nome, Cognome, CF, Citta, Provincia, Tel) VALUES (@Email, @Nome, @Cognome, @CF, @Citta, @Provincia, @Tel)", conn);
                cmd.Parameters.AddWithValue("@Email", nuovoCliente.Email);
                cmd.Parameters.AddWithValue("@Nome", nuovoCliente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", nuovoCliente.Cognome);
                cmd.Parameters.AddWithValue("@CF", nuovoCliente.CF);
                cmd.Parameters.AddWithValue("@Citta", nuovoCliente.Citta);
                cmd.Parameters.AddWithValue("@Provincia", nuovoCliente.Provincia);
                cmd.Parameters.AddWithValue("@Tel", nuovoCliente.Tel);

                cmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine("Inserimento avvenuto con Successo");
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

        public static List<Cliente> GetListaClienti()
        {
            List<Cliente> clienti = new List<Cliente>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cliente";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        Email = reader["Email"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        CF = reader["CF"].ToString(),
                        Citta = reader["Citta"].ToString(),
                        Provincia = reader["Provincia"].ToString(),
                        Tel = reader["Tel"].ToString()
                    };
                    clienti.Add(cliente);
                }
            }

            return clienti;
        }

        public static void EliminaCliente(int idCliente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Cliente WHERE idCliente = @idCliente", conn);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Eliminazione del cliente con ID {idCliente} avvenuta con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessun cliente trovato con l'ID specificato");
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

        public static void AggiornaCliente(Cliente clienteDaAggiornare)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Cliente SET Email = @Email, Nome = @Nome, Cognome = @Cognome, CF = @CF, Citta = @Citta, Provincia = @Provincia, Tel = @Tel WHERE idCliente = @IdCliente", conn);
                cmd.Parameters.AddWithValue("@Email", clienteDaAggiornare.Email);
                cmd.Parameters.AddWithValue("@Nome", clienteDaAggiornare.Nome);
                cmd.Parameters.AddWithValue("@Cognome", clienteDaAggiornare.Cognome);
                cmd.Parameters.AddWithValue("@CF", clienteDaAggiornare.CF);
                cmd.Parameters.AddWithValue("@Citta", clienteDaAggiornare.Citta);
                cmd.Parameters.AddWithValue("@Provincia", clienteDaAggiornare.Provincia);
                cmd.Parameters.AddWithValue("@Tel", clienteDaAggiornare.Tel);
                cmd.Parameters.AddWithValue("@IdCliente", clienteDaAggiornare.idCliente);

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


        public static Cliente GetClienteById(int id)
        {
            Cliente cliente = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE idCliente = @IdCliente", conn);
                cmd.Parameters.AddWithValue("@IdCliente", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        idCliente = (int)reader["idCliente"],
                        Email = reader["Email"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        CF = reader["CF"].ToString(),
                        Citta = reader["Citta"].ToString(),
                        Provincia = reader["Provincia"].ToString(),
                        Tel = reader["Tel"].ToString()
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

            return cliente;
        }

    }
}