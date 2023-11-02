

namespace HotelReviewApp.Common.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}
