using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Business.Models.DTOs.UserDTO
{
    public class UserViewDto
    {
        [Required(ErrorMessage = "userid is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "username is required")]

        public string Name { get; set; }

        [Required(ErrorMessage = "email is required")]

        public string Email { get; set; }

        [Required(ErrorMessage = "modile is required")]

        public int Mobile { get; set; }

        public bool IsBlock { get; set; }

        public string Role { get; set; }

        public DateTime Date { get; set; }

    }
}
