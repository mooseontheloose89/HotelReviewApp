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
        public HotelController(IHotelRepository hotelRepository)
        {
                _hotelRepository = hotelRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hotel>))]
        public IActionResult GetHotels() 
        {
            var hotels = _hotelRepository.GetHotels();
            
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(hotels);
        }
    }
}
