using Albergo.Models;
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
    public class CFDuplicity : ValidationAttribute
    {



        protected override ValidationResult IsValid(object CF, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("CF: " + CF);

            string CFToCheck = CF.ToString();

            if (ControllaPIva(CFToCheck) == false)
            {
                System.Diagnostics.Debug.WriteLine("Trovato False: " + ControllaPIva(CFToCheck));
                return ValidationResult.Success;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Trovato True: " + ControllaPIva(CFToCheck));
                return new ValidationResult("Il CF inserito è già in uso");
            }

        }



        public static bool ControllaPIva(string CF)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString(); ;
            string query = "SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS trovato FROM Cliente WHERE CF = @CF;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CF", CF);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    bool trovato = false;

                    if (reader.Read())
                    {
                        trovato = Convert.ToBoolean(reader["trovato"]);
                        return trovato;
                        
                    }
                    
                }
            }

            return false;
        }

    }
}