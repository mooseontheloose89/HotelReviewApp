using HotelReviewApp.Models;

namespace HotelReviewApp.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string username);
        string GetUserByEmail(string email);
        ICollection<User> GetUsers();
        bool UserExists(int id);
    }
}
