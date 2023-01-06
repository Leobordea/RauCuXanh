using RauCuXanh.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RauCuXanh.Services
{
    public interface IUserApi
    {
        [Get("/user/{id}")]
        Task<User> GetUserById(int id);
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
        Task<List<Cart>> GetCarts();

        [Post("/carts")]
        Task<HttpResponseMessage> CreateCart([Body] Cart cart);

        [Put("/carts")]
        Task<HttpResponseMessage> UpdateCart([Body] Cart payload);

        [Delete("/carts")]
        Task<HttpResponseMessage> DeleteCart([Body] Cart cart);
    }

    public interface IReceiptApi
    {
        [Get("/receipts")]
        Task<List<Receipt_list>> GetReceipts();

        [Get("/receipt/{id}")]
        Task<List<Receipt>> GetReceiptDetail(int id);

        [Post("/receipts")]
        Task<HttpResponseMessage> CreateCart([Body] Receipt rec);

        [Put("/receipts")]
        Task<HttpResponseMessage> UpdateCart([Body] Receipt payload);
    }
    public interface IReviewApi
    {
        [Get("/reviews")]
        Task<List<Review>> GetReviews();
    }
}
