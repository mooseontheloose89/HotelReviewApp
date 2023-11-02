using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using HotelReviewApp.DAL.Models;
using HotelReviewApp.DAL.Interfaces;
using HotelReviewApp.DAL.Data;
using HotelReviewApp.DAL.Repository;
using HotelReviewApp.Common.Helper;
using HotelReviewApp.Common.DTO;




namespace HotelReviewApp.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
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

        public OperationResult<User> CreateUser(CreateUserDTO createUserDto)
        {
            try
            {
                var userEntity = _mapper.Map<User>(createUserDto);

                userEntity.Password = _passwordHasher.HashPassword(userEntity, userEntity.Password);

                _context.Users.Add(userEntity);
                _context.SaveChanges();

                return new OperationResult<User>
                {
                    Success = true,
                    Data = userEntity
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<User>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public OperationResult<User> UpdateUser(User user)
        {
            try
            {
                var existingUser = _context.Users.Find(user.Id);
                if (existingUser == null)
                {
                    return new OperationResult<User>
                    {
                        Success = false,
                        ErrorMessage = "User with the given ID not found."
                    };
                }

                existingUser.Username = user.Username;

                if (existingUser.Password != user.Password)
                {
                    existingUser.Password = _passwordHasher.HashPassword(existingUser, user.Password);
                }

                existingUser.Email = user.Email;

                _context.SaveChanges();

                return new OperationResult<User>
                {
                    Success = true,
                    Data = existingUser
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<User>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public bool VerifyPassword(User user, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public OperationResult<User> ChangeUserPassword(int userId, string newPassword)
        {
            try
            {
                var user = GetUser(userId);
                if (user == null)
                {
                    return new OperationResult<User>
                    {
                        Success = false,
                        ErrorMessage = "User with the given ID not found."
                    };
                }

                user.Password = _passwordHasher.HashPassword(user, newPassword);
                _context.SaveChanges();

                return new OperationResult<User>
                {
                    Success = true,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<User>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }

}
