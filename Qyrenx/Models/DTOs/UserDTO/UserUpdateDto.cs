using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Models.DTOs.UserDTO
{
    public class UserUpdateDto
    {

        [Required(ErrorMessage = "user name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "mobile is required")]
        public int Mobile { get; set; }



    }
}
