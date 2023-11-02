namespace HotelReviewApp.Common.DTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
