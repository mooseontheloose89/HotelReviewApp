using HotelReviewApp.DAL.Models;
using HotelReviewApp.Common.Helper;

namespace HotelReviewApp.DAL.Interfaces
{
    public interface IHotelRepository
    {
        ICollection<Hotel> GetHotels();
        Hotel GetHotel(int id);
        Hotel GetHotel(string name);
        decimal GetHotelRating(int hotelsId);
        bool HotelExists(int hotelsId);
        OperationResult<Hotel> CreateHotel(Hotel hotel);
        OperationResult<Hotel> UpdateHotel(Hotel hotel);
    }
}
