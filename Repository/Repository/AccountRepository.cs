using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Entities.ViewModels;
using System.Data;
using System.Data.SqlClient;

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
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        private string DecodePass(string encodepassword)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodepassword);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
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

        public int LoginUser(LoginViewModel model, string constr)
        {
            try
            {
                string encodepassword = model.ConfirmPasswordHash;
                var decodedpass = DecodePass(encodepassword);
                var parameters = new DynamicParameters();
                parameters.Add("@email", model.Email);
                parameters.Add("@password_hash", decodedpass);
                var sp = "sp_loginuser";
                using (var con = new SqlConnection(constr))
                {
                    con.Open();
                    int status = Convert.ToInt32(con.Execute(sp, parameters, commandType: CommandType.StoredProcedure));
                    return status;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
