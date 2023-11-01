using Asp.Versioning;
using AutoMapper;
using HotelReviewApp.DTO;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;
using HotelReviewApp.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HotelReviewApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper) 
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        public IActionResult GetReviews()
        {
            try
            {
                var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{Id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int Id)
        {
            try
            {
                if (!_reviewRepository.ReviewExists(Id))
                    return NotFound();

                var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(Id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("review/hotel/{HotelId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByHotelId(int HotelId) 
        {
            try
            {
                var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewsByHotelId(HotelId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("review/user/{UserId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByUser(int UserId) 
        {
            try
            {
                var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewsByUser(UserId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
            
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(201, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateReview([FromBody] ReviewDTO reviewDto)
        {
            try
            {
                if (reviewDto == null)
                {
                    return BadRequest(ModelState);
                }

                
                var reviewEntity = _mapper.Map<Review>(reviewDto);
                var createResult = _reviewRepository.CreateReview(reviewEntity);

                if (!createResult.Success)
                {
                    ModelState.AddModelError("", $"Something went wrong saving the review: {createResult.ErrorMessage}");
                    return StatusCode(500, ModelState);
                }

                var createdreviewDto = _mapper.Map<ReviewDTO>(createResult.Data);
                return CreatedAtAction(nameof(GetReview), new { createdreviewDto.Id }, createdreviewDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateReview(int Id, [FromBody] ReviewDTO reviewDto)
        {
            try
            {
                if (reviewDto == null || Id != reviewDto.Id || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_reviewRepository.ReviewExists(Id))
                {
                    return NotFound();
                }
                var updateResult = _reviewRepository.UpdateReview(_mapper.Map<Review>(reviewDto));

                if (!updateResult.Success)
                {
                    ModelState.AddModelError("", $"Something went wrong updating the review with id {Id}. Error: {updateResult.ErrorMessage}");
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
