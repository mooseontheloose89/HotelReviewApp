using Asp.Versioning;
using AutoMapper;
using HotelReviewApp.Common.DTO;
using HotelReviewApp.DAL.Interfaces;
using HotelReviewApp.DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HotelReviewApp.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        public HotelController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hotel>))]
        public IActionResult GetHotels()
        {
            try
            {
                var hotels = _mapper.Map<List<HotelDTO>>(_hotelRepository.GetHotels());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{hotelId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Hotel))]
        [ProducesResponseType(400)]
        public IActionResult GetHotel(int hotelId)
        {
            try
            {
                if (!_hotelRepository.HotelExists(hotelId))
                    return NotFound();

                var hotel = _mapper.Map<HotelDTO>(_hotelRepository.GetHotel(hotelId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{hotelId}/rating")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetHotelRating(int hotelId)
        {
            try
            {
                if (!_hotelRepository.HotelExists(hotelId))
                    return NotFound();

                var rating = _hotelRepository.GetHotelRating(hotelId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(rating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201, Type = typeof(HotelDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateHotel([FromBody] HotelDTO hotelDto)
        {
            try
            {
                if (hotelDto == null)
                {
                    return BadRequest(ModelState);
                }

                var hotel = _hotelRepository.GetHotels().Where(h => h.HotelName.Trim()
                .ToUpper() == hotelDto.HotelName.ToUpper()).FirstOrDefault();

                if (hotel != null)
                {
                    ModelState.AddModelError("", "Hotel already exists");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var hotelEntity = _mapper.Map<Hotel>(hotelDto);
                var createResult = _hotelRepository.CreateHotel(hotelEntity);

                if (!createResult.Success)
                {
                    ModelState.AddModelError("", $"Something went wrong saving the hotel: {createResult.ErrorMessage}");
                    return StatusCode(500, ModelState);
                }

                var createdHotelDto = _mapper.Map<HotelDTO>(createResult.Data);
                return CreatedAtAction(nameof(GetHotel), new { hotelId = createdHotelDto.Id }, createdHotelDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{Id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateHotel(int Id, [FromBody] HotelDTO hotelDto)
        {
            try
            {
                if (hotelDto == null || Id != hotelDto.Id || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_hotelRepository.HotelExists(Id))
                {
                    return NotFound();
                }
                var updateResult = _hotelRepository.UpdateHotel(_mapper.Map<Hotel>(hotelDto));

                if (!updateResult.Success)
                {
                    ModelState.AddModelError("", $"Something went wrong updating the hotel with id {Id}. Error: {updateResult.ErrorMessage}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
