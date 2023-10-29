using HotelReviewApp.Data;
using HotelReviewApp.Helper;
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

        public OperationResult<Hotel> CreateHotel(Hotel hotel)
        {
            try
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();

                return new OperationResult<Hotel>
                {
                    Success = true,
                    Data = hotel
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<Hotel>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public OperationResult<Hotel> UpdateHotel(Hotel hotel)
        {
            try
            {
                var existingHotel = _context.Hotels.Find(hotel.Id);
                if (existingHotel == null)
                {
                    return new OperationResult<Hotel>
                    {
                        Success = false,
                        ErrorMessage = "Hotel with the given ID not found."
                    };
                }

                
                existingHotel.HotelName = hotel.HotelName;
                existingHotel.Address = hotel.Address;
                existingHotel.City = hotel.City;
                existingHotel.County = hotel.County;
                existingHotel.Country = hotel.Country;
                existingHotel.RatingAvg = hotel.RatingAvg;
                existingHotel.HotelUrl = hotel.HotelUrl;

                _context.SaveChanges();

                return new OperationResult<Hotel>
                {
                    Success = true,
                    Data = existingHotel
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<Hotel>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }


    }
}
