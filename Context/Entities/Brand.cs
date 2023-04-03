using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LiquorStoreApi.Context.Entities;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
