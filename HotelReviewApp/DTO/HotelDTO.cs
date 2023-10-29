using System.ComponentModel.DataAnnotations;

namespace HotelReviewApp.DTO
{
    public class HotelDTO
    {
        public int Id { get; set; }
        [Required] 
        public string HotelName { get; set; }
        [Required] 
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        [RegularExpression(@"^United Kingdom$", ErrorMessage = "The country must be 'United Kingdom'.")]
        public string Country { get; set; }

        public int RatingAvg { get; set; }
        public string HotelUrl { get; set; }
    }
}
