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

        [Post("/users")]
        Task<HttpResponseMessage> CreateUser([Body] Dictionary<string, object> data);

        [Put("/users/{id}")]
        Task<HttpResponseMessage> UpdateUser(int id, [Body] User user);
    }
    public interface IRaucuApi
    {
        [Get("/rauculist")]
        Task<List<Raucu>> GetRaucuList();

        [Get("/raucu/{id}")]
        Task<Raucu> GetRaucuById(int id);
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
        Task<List<Receipt>> GetReceiptsByUser([Body] Dictionary<string, object> data);

        [Get("/receipt/{id}")]
        Task<List<Receipt>> GetReceiptById(int id);

        [Get("/receiptlist")]
        Task<List<Receipt_list>> GetReceiptList();

        [Post("/receipts")]
        Task<HttpResponseMessage> CreateReceipt([Body] Receipt payload);

        [Post("/receiptlist")]
        Task<HttpResponseMessage> CreateReceiptList([Body] Receipt_list payload);

        [Put("/receipts")]
        Task<HttpResponseMessage> UpdateReceipt([Body] Dictionary<string, object> payload);
    }
    public interface IReviewApi
    {
        [Get("/reviews")]
        Task<List<Review>> GetReviews();

        [Post("/reviews")]
        Task<HttpResponseMessage> CreateReview([Body] Dictionary<string, object> payload);

        [Put("/reviews")]
        Task<HttpResponseMessage> UpdateReview([Body] Dictionary<string, object> payload);
    }
    public interface IBookmarkApi
    {
        [Get("/bookmarks")]
        Task<List<Bookmark>> GetBookmarks();

        [Post("/bookmarks")]
        Task<HttpResponseMessage> CreateBookmark([Body] Bookmark payload);

        [Delete("/bookmarks")]
        Task<HttpResponseMessage> DeleteBookmark([Body] Bookmark payload);
    }
}
