using HotelReviewApp.Data;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;

namespace HotelReviewApp.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly DataContext _context;
        public HotelRepository(DataContext context) 
        {
            _context = context;

        }

        public Hotel GetHotel(int id)
        {
            return _context.Hotels.Where(h => h.Id == id).FirstOrDefault();
        }

        public Hotel GetHotel(string name)
        {
            return _context.Hotels.Where(h => h.HotelName == name).FirstOrDefault();
        }

        public decimal GetHotelRating(int hotelsId)
        {
            var review = _context.Reviews.Where(h => h.HotelId == hotelsId);

                if (review.Count() <= 0)
                return 0;

                return ((decimal)review.Sum(h => h.Rating) / review.Count());

        }

        public ICollection<Hotel> GetHotels() 
        {
            return _context.Hotels.OrderBy(h  => h.Id).ToList();
        }

        public bool HotelExists(int hotelsId)
        {
            return _context.Hotels.Any(h =>  h.Id == hotelsId);
        }
    }
}
