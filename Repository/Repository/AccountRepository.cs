﻿using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Entities.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Entities.Models;
using System.Web.Mvc;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        #region Encoding the password
        private static string EncodedPass(string password)
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        #endregion

        #region Decoding the password
        private string DecodePass(string encodepassword)
        {
            byte[] data = Convert.FromBase64String(encodepassword);
            var result = System.Text.Encoding.UTF8.GetString(data);
            return result;
            //int paddingLength = base64String.Length % 4;
            //if (paddingLength > 0)
            //{
            //    base64String += new string('=', 4 - paddingLength);
            //}
            //byte[] data = Convert.FromBase64String(encodepassword);
            //var result = System.Text.Encoding.UTF8.GetString(data);
        }
        #endregion

        #region Registration Method
        /// <summary>
        /// Register New User into DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="constr"></param>
        /// <exception cref="Exception"></exception>
        public async Task RegisterNewUser(RegistrationViewModel model, string constr)
        {
            try
            {
                //string password = model.PasswordHash;
                //var encodedData = EncodedPass(password);
                var parameters = new DynamicParameters();
                parameters.Add("@first_name", model.FirstName);
                parameters.Add("@last_name", model.LastName);
                parameters.Add("@email", model.Email);
                parameters.Add("@password_hash", model.PasswordHash);
                parameters.Add("@created_at", DateTime.Now);
                parameters.Add("@role", "USER");
                var sp = "sp_RegisterUser";

                using (var con = new SqlConnection(constr))
                {
                    await con.OpenAsync();
                    var execute = await con.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <param name="model"></param>
        /// <param name="constr"></param>
        /// <returns>status, 1 if email already exists</returns>
        public async Task<int> UserExistsCheck(RegistrationViewModel model, string constr)
        {
            var param = new DynamicParameters();
            param.Add("@email", model.Email);
            var sps = "sp_emailcheck";
            using (var conn = new SqlConnection(constr))
            {
                await conn.OpenAsync();
                int status = (int)await conn.ExecuteScalarAsync(sps, param, commandType: CommandType.StoredProcedure);
                return status;
            }
        }
        #endregion

        #region Login Method

        /// <param name="model"></param>
        /// <param name="constr"></param>
        /// <returns> Role (ADMIN or USER) </returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> LoginUser(LoginViewModel model, string constr)
        {
            try
            {
                //string encodepassword = model.ConfirmPasswordHash;
                //var decodedpass = DecodePass(encodepassword);
                var parameters = new DynamicParameters();
                parameters.Add("@email", model.Email);
                parameters.Add("@password_hash", model.ConfirmPasswordHash);

                var sp = "sp_loginuser";
                using (var con = new SqlConnection(constr))
                {
                    await con.OpenAsync();
                    User Role = await con.QueryFirstOrDefaultAsync<User>(sp, parameters, commandType: CommandType.StoredProcedure);
                    return Role;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public async Task GetAllEmployeeData(string connectionStr)
        {
            var parameters = new DynamicParameters();
            var sp = "get_allEmployeeData";
            using(var con = new SqlConnection(connectionStr))
            {
                await con.OpenAsync();
                var result = await con.QueryAsync(sp, parameters, commandType:CommandType.StoredProcedure);
                
            }
        }
    }
}
