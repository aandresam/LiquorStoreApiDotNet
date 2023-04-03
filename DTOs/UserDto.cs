using System.ComponentModel.DataAnnotations;

namespace LiquorStoreApi.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "El Nombre es requerido!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El Apellido es requerido!")]
        public string LastName { get; set; } = null!;

        [
            Required(ErrorMessage = "El Teléfono es requerido!")
        ]
        public string PhoneNumber { get; set; } = null!;
    }
}
