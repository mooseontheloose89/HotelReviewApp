using HotelReviewApp.DAL.Models;
using HotelReviewApp.Common.Helper;

namespace HotelReviewApp.DAL.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        List<Review> GetReviewsByHotelId(int hotelId);
        ICollection<Review> GetAllReviews(int hotelId);
        ICollection<Review> GetReviewsByUser(int userId);
        bool ReviewExists(int Id);
        OperationResult<Review> CreateReview(Review review);
        OperationResult<Review> UpdateReview(Review review);

    }
}
