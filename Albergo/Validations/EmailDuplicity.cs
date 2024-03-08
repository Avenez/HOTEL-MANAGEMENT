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
    public class EmailDuplicity : ValidationAttribute
    {

        protected override ValidationResult IsValid(object Email, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("Email: " + Email);

            string pecToCheck = Email.ToString();

            if (ControllaEmail(pecToCheck) == false)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("L'Email inserita è già in uso");
            }

        }



        public static bool ControllaEmail(string email)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM Cliente WHERE Email = @Email;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        return Convert.ToBoolean(reader["trovato"]);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore" + ex);
                    return false;
                }

                finally 
                {
                connection.Close();
                }

                
                
            }

            
        }

    }
}