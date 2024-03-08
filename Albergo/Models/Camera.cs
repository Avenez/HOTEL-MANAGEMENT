using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using static Albergo.Validations.CameraDuplicity;
using System.Configuration;
using System.Data.SqlClient;
using Albergo.Validations;

namespace Albergo.Models
{
    public class Camera
    {
        [ScaffoldColumn(false)]
        public int idCamera { get; set; }

        [Required]
        [Display(Name = "Numero camera")]
        [CameraDuplicity(ErrorMessage = "Il numero di camera inserito è già asssegnato")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Il campo Numero di Telefono deve contenere solo numeri.")]
        public int NumCamera { get; set; }

        [Required]
        [Display(Name = "Descrizione Camera")]
        public string Descrizione { get; set; }

        [Required]
        [Display(Name = "Tipologia")]
        public string Tipo { get; set; }

        [Required]
        [Display(Name = "Disponibile")]
        public bool Disponibile { get; set; }




        public Camera() { }

        public Camera(int id, int numCamera, string descrizione, string tipo, bool disponibile)
        {
            idCamera = id;
            NumCamera = numCamera;
            Descrizione = descrizione;
            Tipo = tipo;
            Disponibile = disponibile;
        }

        // Metodo per inserire una nuova camera nella tabella Camera
        public static void InserisciNuovaCamera(Camera nuovaCamera)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Camera (NumCamera, Descrizione, Tipo, Disponibile) VALUES (@NumCamera, @Descrizione, @Tipo, @Disponibile)", conn);
                cmd.Parameters.AddWithValue("@NumCamera", nuovaCamera.NumCamera);
                cmd.Parameters.AddWithValue("@Descrizione", nuovaCamera.Descrizione);
                cmd.Parameters.AddWithValue("@Tipo", nuovaCamera.Tipo);
                cmd.Parameters.AddWithValue("@Disponibile", nuovaCamera.Disponibile);

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

        // Metodo per ottenere una lista di camere dalla tabella Camera
        public static List<Camera> GetListaCamere()
        {
            List<Camera> camere = new List<Camera>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Camera";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Camera camera = new Camera
                    {
                        idCamera = Convert.ToInt32(reader["idCamera"]),
                        NumCamera = Convert.ToInt32(reader["NumCamera"]),
                        Descrizione = reader["Descrizione"].ToString(),
                        Tipo = reader["Tipo"].ToString(),
                        Disponibile = Convert.ToBoolean(reader["Disponibile"])
                    };
                    camere.Add(camera);
                }
            }

            return camere;
        }

        public static List<Camera> GetListaCamereDisp()
        {
            List<Camera> camere = new List<Camera>();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Camera WHERE Disponibile = 'True' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Camera camera = new Camera
                    {
                        idCamera = Convert.ToInt32(reader["idCamera"]),
                        NumCamera = Convert.ToInt32(reader["NumCamera"]),
                        Descrizione = reader["Descrizione"].ToString(),
                        Tipo = reader["Tipo"].ToString(),
                        Disponibile = Convert.ToBoolean(reader["Disponibile"])
                    };
                    camere.Add(camera);
                }
            }

            return camere;
        }

        public static void EliminaCamera(int idCamera)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Camera WHERE idCamera = @idCamera", conn);
                cmd.Parameters.AddWithValue("@idCamera", idCamera);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Eliminazione della camera con ID {idCamera} avvenuta con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessuna camera trovata con l'ID specificato");
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

        public static void UpdateDispCamera(int idCamera, bool disponibile)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Camera SET Disponibile = @disponibile WHERE idCamera = @idCamera", conn);
                cmd.Parameters.AddWithValue("@disponibile", disponibile);
                cmd.Parameters.AddWithValue("@idCamera", idCamera);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Aggiornamento della disponibilità della camera con ID {idCamera} avvenuto con successo");
                }
                else
                {
                    Console.WriteLine("Nessuna camera trovata con l'ID specificato");
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

        public static int GetIdCameraFromNumCamera(int numCamera)
        {
            int idCamera = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT idCamera FROM Camera WHERE NumCamera = @numCamera";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@numCamera", numCamera);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idCamera = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return idCamera;
        }

        public static void AggiornaCamera(Camera cameraDaAggiornare)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Camera SET NumCamera = @NumCamera, Descrizione = @Descrizione, Tipo = @Tipo, Disponibile = @Disponibile WHERE idCamera = @IdCamera", conn);
                cmd.Parameters.AddWithValue("@NumCamera", cameraDaAggiornare.NumCamera);
                cmd.Parameters.AddWithValue("@Descrizione", cameraDaAggiornare.Descrizione);
                cmd.Parameters.AddWithValue("@Tipo", cameraDaAggiornare.Tipo);
                cmd.Parameters.AddWithValue("@Disponibile", cameraDaAggiornare.Disponibile);
                cmd.Parameters.AddWithValue("@IdCamera", cameraDaAggiornare.idCamera);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Aggiornamento Camera avvenuto con successo");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nessuna riga Camera aggiornata");
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

        public static Camera GetCameraById(int id)
        {
            Camera camera = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Camera WHERE idCamera = @IdCamera", conn);
                cmd.Parameters.AddWithValue("@IdCamera", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    camera = new Camera
                    {
                        idCamera = (int)reader["idCamera"],
                        NumCamera = (int)reader["NumCamera"],
                        Descrizione = reader["Descrizione"].ToString(),
                        Tipo = reader["Tipo"].ToString(),
                        Disponibile = (bool)reader["Disponibile"]
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

            return camera;
        }




    }
}