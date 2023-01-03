using Newtonsoft.Json;
using RauCuXanh.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace RauCuXanh.Services
{
    public class RaucuService
    {
        private readonly string Base_url = "http://192.168.1.10:5000/api/";

        public async Task<Raucu> getRaucuById(string id)
        {
            string url = $"{Base_url}raucu/{id}"; ;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Raucu>(result);

                return json;
            }

            return null;
        }

        public async Task<ObservableCollection<Raucu>> getRaucuList()
        {
            string url = $"{Base_url}rauculist";

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ObservableCollection<Raucu>>(result);

                return json;
            }

            return null;
        }
    }
}
