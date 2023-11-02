using System.ComponentModel.DataAnnotations;

namespace HotelReviewApp.Common.DTO
{
    public class ChangeUserPasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}
