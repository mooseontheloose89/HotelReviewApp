﻿using AutoMapper;
using HotelReviewApp.DTO;
using HotelReviewApp.Interfaces;
using HotelReviewApp.Models;
using HotelReviewApp.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HotelReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}