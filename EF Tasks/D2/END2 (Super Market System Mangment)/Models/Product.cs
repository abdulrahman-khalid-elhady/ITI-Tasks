using System;
using System.Collections.Generic;

namespace END2.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
