using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RauCuXanh.Models;

namespace RauCuXanh.Services
{
    public class UserService
    {
        string Base_url = "https://639dd5ad3542a2613050ec0f.mockapi.io/api/users";

        public async Task<User> getUserById(string id)
        {
            string url = $"{Base_url}/{id}"; ;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<User>(result);

                return json;
            }

            return null;
        }

        public async Task<ObservableCollection<User>> getUsers()
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ObservableCollection<User>>(result);

                return json;
            }

            return null;
        }
    }
}
