using HotelReviewApp.DTO;
using HotelReviewApp.Helper;
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
        OperationResult<User> CreateUser(CreateUserDTO createUserDto);
        OperationResult<User> UpdateUser(User user);
        OperationResult<User> ChangeUserPassword(int userId, string newPassword);
        bool VerifyPassword(User user, string providedPassword);
    }
}
