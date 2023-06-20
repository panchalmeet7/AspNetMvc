using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using Entities.ViewModels;
using System.Data;


namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private static string EncodedPass(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                return  Convert.ToBase64String(encData_byte);
                 
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public void RegisterNewUser(RegistrationViewModel model, string constr)
        {
            try
            {
                string password = model.PasswordHash;
                var encodedData = EncodedPass(password);
                var parameters = new DynamicParameters();
                parameters.Add("@first_name", model.FirstName);
                parameters.Add("@last_name", model.LastName);
                parameters.Add("@email", model.Email);
                parameters.Add("@password_hash", encodedData);
                parameters.Add("@created_at", DateTime.Now);
                parameters.Add("@role", "USER");
                var sp = "sp_RegisterUser";
                using (var con = new SqlConnection(constr))
                {
                    con.Open();
                    var execute = con.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
