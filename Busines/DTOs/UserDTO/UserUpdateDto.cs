using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Business.Models.DTOs.UserDTO
{
    public class UserUpdateDto
    {

        [Required(ErrorMessage = "user name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "mobile is required")]
        public string Mobile { get; set; }



    }
}
