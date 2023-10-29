using HotelReviewApp.Models;

namespace HotelReviewApp.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string username);
        User GetUserByEmail(string email);
        ICollection<User> GetUsers();
        bool UserExists(int id);
    }
}
