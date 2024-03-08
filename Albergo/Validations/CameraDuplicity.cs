using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Validations
{
    public class CameraDuplicity : ValidationAttribute
    {
        protected override ValidationResult IsValid(object NumCamera, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("NumCamera: " + NumCamera);

            string NumCameraToCheck = NumCamera.ToString();

            if (!ControllaEmail(NumCameraToCheck))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Il numero di camera inserito è già in uso");
            }

        }



        public static bool ControllaEmail(string NumCamera)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM Camera WHERE NumCamera = @NumCamera;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumCamera", NumCamera);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToBoolean(reader["trovato"]);
                    }
                }
            }

            return false;
        }

    }
}