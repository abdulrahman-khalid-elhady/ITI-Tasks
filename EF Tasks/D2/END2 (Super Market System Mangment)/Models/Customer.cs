using System;
using System.Collections.Generic;

namespace END2.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
