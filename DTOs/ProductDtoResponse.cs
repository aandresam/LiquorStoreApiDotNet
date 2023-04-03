namespace LiquorStoreApi.DTOs
{
    public class ProductDtoResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }


        public string RegDate { get; set; } = null!;
    }
}
