﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RauCuXanh.Models;
using System.Threading.Tasks;

namespace RauCuXanh.Services
{
    public class ShopService
    {
        string Base_url = "https://639dd5ad3542a2613050ec0f.mockapi.io/api/shops";

        public async Task<Shop> getShopById(string id)
        {
            string url = $"{Base_url}/{id}";

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Shop>(result);

                return json;
            }

            return null;
        }

        public async Task<ObservableCollection<Shop>> getShops()
        {
            string url = Base_url;

            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ObservableCollection<Shop>>(result);

                return json;
            }

            return null;
        }
    }
}