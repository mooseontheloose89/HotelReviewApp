﻿namespace HotelReviewApp.Common.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
    }
}
