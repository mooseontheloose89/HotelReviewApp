﻿namespace HotelReviewApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}