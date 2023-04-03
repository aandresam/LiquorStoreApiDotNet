using System.ComponentModel.DataAnnotations;

namespace LiquorStoreApi.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El campo Email es requerido!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El campo Password es requerido!")]
        public string Password { get; set; } = null!;
    }
}
