using System.ComponentModel.DataAnnotations;

namespace LiquorStoreApi.DTOs
{
    public class BrandDto
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Name { get; set; } = null!;
    }
}
