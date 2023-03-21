using Model.Library;
using Newtonsoft.Json;
using Proxy.Library.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public class API_UserProxy : IUserProxy
    {
        public UserServiceModel Login(LoginServiceModel loginVM)
        {
            UserServiceModel userServiceModel = new UserServiceModel(); 
            string stringLoginVM = JsonConvert.SerializeObject(loginVM);
            StringContent content = new StringContent(stringLoginVM);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:44364/api/");
            //quando si chiama questo metodo parte la request
            var response = httpClient.PostAsync($"User/Login", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;

                //deserializziamo
                userServiceModel  = JsonConvert.DeserializeObject<UserServiceModel>(jsonContent);
            }
            else
            {
                //gestisco l'errore
            }

            return userServiceModel;
        }
    }
}
