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
    public class TelDuplicity : ValidationAttribute
    {
        protected override ValidationResult IsValid(object Tel, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("Telefono: " + Tel);

            string telToCheck = Tel.ToString();

            if (!ControllaEmail(telToCheck))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Il numero ti telefono inserito inserita è già in uso");
            }

        }



        public static bool ControllaEmail(string tel)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM Cliente WHERE Tel = @Tel;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tel", tel);
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