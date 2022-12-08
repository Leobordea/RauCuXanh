using RauCuXanh.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RauCuXanh.Services
{
    public interface IUserApi
    {
        [Get("/user/{id}")]
        Task<List<User>> GetUserById(int id);
    }
    public interface IRaucuApi
    {
        [Get("/rauculist")]
        Task<List<Raucu>> GetRaucuList();

        [Get("/raucu/{id}")]
        Task<List<Raucu>> GetRaucuById(int id);
    }
    public interface IShopApi
    {
        [Get("/shops")]
        Task<List<Shop>> GetShops();

        [Get("/shop/{id}")]
        Task<Shop> GetShopById(int id);
    }
    public interface INotificationApi
    {
        [Get("/notifications")]
        Task<List<Notification>> GetNotifications();

        [Get("/notification/{id}")]
        Task<Notification> GetNotificationById(int id);
    }
    public interface ICartApi
    {
        [Get("/carts")]
        Task<List<Object>> GetCarts();
    }
}
