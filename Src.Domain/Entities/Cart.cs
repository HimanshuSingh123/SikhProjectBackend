using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Entities;

public class Cart
{
    public string Username { get; set; } = default!;
    public string ItemTitle { get; set; } = default!;
    public string ItemDescription { get; set; } = default!;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = default!;
    public int CartId { get; set; }
}

