using AutoMapper;
using HotelReviewApp.Data;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;

namespace HotelReviewApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }    

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Username).ToList();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }    
    }
}
