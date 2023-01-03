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
    public class ReviewService
    {
        string Base_url = "http://192.168.1.10:5000/api/reviews";

        public async Task<ObservableCollection<Review>> getReviews()
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ObservableCollection<Review>>(result);

                return json;
            }

            return null;
        }

        public async Task<HttpResponseMessage> createReview(Review rev)
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(rev);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(url, content);

            return response;
        }

        public async Task<HttpResponseMessage> deleteReview(string id)
        {
            string url = $"{Base_url}/{id}";

            HttpClient client = new HttpClient();

            var response = await client.DeleteAsync(url);
            return response;
        }
    }
}
