using System;
using System.Diagnostics;
using AddressBook.MAUI.Models;
using AddressBook.MAUI.LanguageResources;
using Newtonsoft.Json;
using System.Text;

namespace AddressBook.MAUI.Services
{
    public class LoginService
    {
        public LoginService()
        {
        }

        public static async Task<LoginModel> Login(LoginModel LoginData1)
        {
            LoginModel UDI = new LoginModel();
            try
            {
                var client = new System.Net.Http.HttpClient();

                LoginObject Loginobj = new LoginObject();
                Loginobj.LoginData = LoginData1;

                client.BaseAddress = new Uri(APIService.ServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var jData = JsonConvert.SerializeObject(Loginobj);
                var content1 = new StringContent(jData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/post", content1);

                var result = response.Content.ReadAsStringAsync().Result;

                var resultobject = JsonConvert.DeserializeObject<LoginResponse>(result);

                UDI = resultobject?.Data?.LoginData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                UDI.Errors = AppResources.MESSAGE_ERROR_SOMETHING_WENT_WRONG_WITH_USER_LOGIN;
            }
            return UDI;
        }
    }
}

