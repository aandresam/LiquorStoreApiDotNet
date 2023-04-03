using System.ComponentModel.DataAnnotations;

namespace LiquorStoreApi.DTOs
{
    public class ProductDtoRequest
    {
        [Required(ErrorMessage = "El campo nomnre es requerido!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El campo categoria es requerido!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo marca es requerido!")]
        public int BrandId { get; set; }

        [
            Required(ErrorMessage = "El campo precio es requerido!")
        ]
        public decimal Price { get; set; }

        [
            Required(ErrorMessage = "El campo cantidad es requerido!")
        ]
        public int Stock { get; set; }
    }
}
