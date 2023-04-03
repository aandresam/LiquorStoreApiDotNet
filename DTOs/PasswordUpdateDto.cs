using System.ComponentModel.DataAnnotations;

namespace LiquorStoreApi.DTOs
{
    public class PasswordUpdateDto
    {

        [Required(ErrorMessage = "La contraseña actual es requerida!")]
        public string CurrentPassword { get; set; } = null!;

        [
            Required(ErrorMessage = "La contraseña nueva es requerida!"),
        ]
        public string NewPassword { get; set; } = null!;
    }
}
