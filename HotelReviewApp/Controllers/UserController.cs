using AutoMapper;
using HotelReviewApp.DTO;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;
using HotelReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int Id)
        {
            try
            {
                if (!_userRepository.UserExists(Id))
                    return NotFound();

                var user = _mapper.Map<UserDTO>(_userRepository.GetUser(Id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("user/{username}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {
            try
            {               

                var user = _mapper.Map<UserDTO>(_userRepository.GetUser(username));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("/users/{email}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var user = _mapper.Map<UserDTO>(_userRepository.GetUserByEmail(email));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
