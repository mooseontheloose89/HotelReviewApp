using HotelReviewApp.Data;
using HotelReviewApp.Helper;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;

namespace HotelReviewApp.Repository
{
    
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Review> GetAllReviews(int HotelId)
        {
            return _context.Reviews.Where(r => r.HotelId == HotelId).ToList();
        }

        public Review GetReview(int id) 
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public List<Review> GetReviewsByHotelId(int HotelId)
        {
            return _context.Reviews.Where(r => r.HotelId == HotelId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r => r.HotelId).ToList();
        }

        public ICollection<Review> GetReviewsByUser(int UserId) 
        {
            return _context.Users.Where(u => u.Id == UserId).SelectMany(u => u.Reviews).ToList();
        }

        public bool ReviewExists(int Id)
        {
            return _context.Reviews.Any(r => r.Id == Id);
        }

        public OperationResult<Review> CreateReview(Review review)
        {
            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();

                return new OperationResult<Review>
                {
                    Success = true,
                    Data = review
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<Review>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
            
        }

        public OperationResult<Review> UpdateReview(Review review)
        {
            try
            {
                var exisitingReview = _context.Reviews.Find(review.Id);
                if (exisitingReview == null)
                {
                    return new OperationResult<Review>
                    {
                        Success = false,
                        ErrorMessage = "Review with the given ID not found."
                    };
                }
                
                exisitingReview.Title = review.Title;
                exisitingReview.Description = review.Description;
                exisitingReview.Rating = review.Rating;
                exisitingReview.DatePosted = DateTime.Now;

                _context.SaveChanges();

                return new OperationResult<Review>
                {
                    Success = true,
                    Data = exisitingReview
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<Review>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
