using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RauCuXanh.Models;
using System.Threading.Tasks;

namespace RauCuXanh.Services
{
    public class CartService
    {
        string Base_url = "https://639dd5ad3542a2613050ec0f.mockapi.io/api/carts";

        public async Task<ObservableCollection<Cart>> getCarts()
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ObservableCollection<Cart>>(result);

                return json;
            }

            return null;
        }

        public async Task<HttpResponseMessage> createCart(Cart cart)
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(cart);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(url, content);
            return response;
        }

        public async Task<HttpResponseMessage> deleteCart(string id)
        {
            string url = $"{Base_url}/{id}";

            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync(url);
            return response;
        }
    }
}
