namespace HotelReviewApp.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public int RatingAvg { get; set; }
        public string HotelUrl { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
