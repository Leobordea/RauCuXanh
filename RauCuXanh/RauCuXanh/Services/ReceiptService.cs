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
    public class ReceiptService
    {
        string Base_url = "https://63a5b2ce318b23efa79b1a4b.mockapi.io/api/receipts";

        public async Task<ObservableCollection<Receipt>> getReceipts()
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ObservableCollection<Receipt>>(result);

                return json;
            }

            return null;
        }

        public async Task<HttpResponseMessage> createReceipt(Receipt rec, IEnumerable<CartItem> cartList)
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(rec);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(url, content);
            var feedback = await response.Content.ReadAsStringAsync();
            Receipt result = JsonConvert.DeserializeObject<Receipt>(feedback);

            foreach (CartItem c in cartList)
            {
                await createReceiptDetail(result.Id, c.Raucu, c.Cart.quantity);
            }

            return response;
        }

        public async Task<HttpResponseMessage> createReceiptDetail(string id, Raucu r, int quantity)
        {
            string url = $"{Base_url}/{id}/receipt_list";

            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(new Receipt_list() { Quantity = quantity, Raucu_id = r.Id });
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(url, content);

            return response;
        }

        public async Task<HttpResponseMessage> deleteReceipt(string id)
        {
            string url = $"{Base_url}/{id}";

            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync(url);
            return response;
        }
    }
}
