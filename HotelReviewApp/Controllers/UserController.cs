using Asp.Versioning;
using AutoMapper;
using HotelReviewApp.DAL.Interfaces;
using HotelReviewApp.DAL.Models;
using HotelReviewApp.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using HotelReviewApp.Common.DTO;

namespace HotelReviewApp.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [MapToApiVersion("2.0")]
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
        [MapToApiVersion("2.0")]
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
        [MapToApiVersion("2.0")]
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

        [HttpGet("GetUserByEmail/{email}")]
        [MapToApiVersion("2.0")]
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

        [HttpPost]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(200, Type = typeof(CreateUserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateUser([FromBody] CreateUserDTO createUserDto)
        {
            try
            {
                if (createUserDto == null)
                {
                    return BadRequest(ModelState);
                }

                var createResult = _userRepository.CreateUser(createUserDto);

                if (!createResult.Success)
                {
                    ModelState.AddModelError("", $"Something went wrong saving the created User: {createResult.ErrorMessage}");
                    return StatusCode(500, ModelState);
                }

                var createdUserDto = _mapper.Map<CreateUserDTO>(createResult.Data);
                return CreatedAtAction(nameof(GetUser), new { createdUserDto.Id }, createdUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateUser(int Id, [FromBody] UpdateUserDTO updateUserDto)
        {
            try
            {
                if (updateUserDto == null || Id != updateUserDto.Id || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_userRepository.UserExists(Id))
                {
                    return NotFound();
                }

                var userToUpdate = _mapper.Map<User>(updateUserDto);
                userToUpdate.ModifiedDate = DateTime.UtcNow;

                var updateResult = _userRepository.UpdateUser(userToUpdate);

                if (!updateResult.Success)
                {
                    ModelState.AddModelError("", $"Something went wrong updating the user with id {Id}. Error: {updateResult.ErrorMessage}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}/ChangePassword")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ChangePassword(int userId, [FromBody] ChangeUserPasswordDTO changeUserPasswordDto)
        {
            try
            {
                ///*var currentUserId = /* logic to get authenticated user's ID */;
                //if (userId != currentUserId)
                //    return Unauthorized();

                var user = _userRepository.GetUser(userId);

                if (user == null)
                    return NotFound();

                if (!_userRepository.VerifyPassword(user, changeUserPasswordDto.CurrentPassword))

                    return BadRequest("Current password is incorrect.");

                if (changeUserPasswordDto.NewPassword != changeUserPasswordDto.ConfirmNewPassword)
                    return BadRequest("New password and confirm password do not match.");

                var changePasswordResult = _userRepository.ChangeUserPassword(userId, changeUserPasswordDto.NewPassword);
                if (!changePasswordResult.Success)
                {
                    return StatusCode(500, $"Internal server error: {changePasswordResult.ErrorMessage}");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}, {ex.StackTrace}");
            }
        }
    }
}
