using AutoMapper;
using HotelReviewApp.DTO;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HotelReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [ProducesResponseType(200,Type = typeof(Hotel))]
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
    }
}
