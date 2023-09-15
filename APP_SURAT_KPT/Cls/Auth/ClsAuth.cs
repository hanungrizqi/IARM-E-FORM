﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using APP_SURAT_KPT.ViewModels.Account;


namespace APP_SURAT_KPT.Cls.Auth
{
    public class ClsAuth
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<LoginResponseVM> GetUserData(LoginVM request)
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            LoginResponseVM errorResponse = new LoginResponseVM();
            string API_PATH = ConfigurationManager.AppSettings["API_PATH"];
            var values = new Dictionary<string, string>
              {
                  { "Username", request.Nrp },
                  { "Password" , request.Password }
              };

            var content = new FormUrlEncodedContent(values);
            try
            {
                var response = await client.PostAsync(API_PATH + "api/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var responseData = System.Text.Json.JsonSerializer.Deserialize<LoginResponseVM>(responseString);
                    return responseData;
                }
                else
                {
                    errorResponse.Success = false;
                    errorResponse.Message = $"Terjadi kesalahan dalam menyambungkan ke server. <br />Message : {response.ReasonPhrase}";
                    return errorResponse;
                }
            }
            catch (HttpRequestException ex )
            {
                errorResponse.Success = false;
                errorResponse.Message = $"Terjadi kesalahan dalam menyambungkan ke server. <br />Message : {ex.Message}";
                return errorResponse;
            }
            catch (Exception ex)
            {
                errorResponse.Success = false;
                errorResponse.Message = $"Terjadi kesalahan dalam menyambungkan ke server. <br />Message : {ex.Message}";
                return errorResponse;

            }
        }
    }
}