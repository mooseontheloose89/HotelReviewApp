using HotelReviewApp.Models;

namespace HotelReviewApp.Interfaces
{
    public interface IHotelRepository
    {
        ICollection<Hotel> GetHotels();
        Hotel GetHotel(int id);
        Hotel GetHotel(string name);
        decimal GetHotelRating(int hotelsId);
        bool HotelExists(int hotelsId);
    }
}
