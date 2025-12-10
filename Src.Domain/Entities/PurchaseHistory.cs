using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Entities;
public class PurchaseHistory
{
    public int TransactionId { get; set; }
    public string Username { get; set; } = default!;
    public string ItemTitle { get; set; } = default!;
    public double price { get; set; }
    public int Quantity { get; set; }
    public string ItemType { get; set; } = default!;
    public DateTime PurchaseTimestamp { get; set; }

    public User User { get; set; } = default!;
}

