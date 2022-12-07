using RauCuXanh.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RauCuXanh.Services
{
    public interface IFreeToPlayApi
    {
        [Get("/games?sort-by=alphabetical")]
        Task<List<Game>> GetF2PAsync();
    }
    public interface IRaucuApi
    {
        [Get("/rauculist")]
        Task<List<Raucu>> GetRaucuList();
    }
    public interface IShopApi
    {
        [Get("/shops")]
        Task<List<Shop>> GetShops();

        [Get("/shop/{id}")]
        Task<Shop> GetShopById(int id);
    }
    public interface ICartApi
    {
        [Get("/carts")]
        Task<List<Object>> GetCarts();
    }
}
