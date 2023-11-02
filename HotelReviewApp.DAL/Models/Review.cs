namespace HotelReviewApp.DAL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
    }
}
