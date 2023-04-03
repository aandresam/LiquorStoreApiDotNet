﻿using System;
using System.Collections.Generic;

namespace LiquorStoreApi.Context.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UserId { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public DateTime RegDate { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual User? User { get; set; }
}
