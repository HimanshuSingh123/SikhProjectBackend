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
    public int? SubmissionId { get; set; }

    public Merch Merch { get; set; } = default!;
    public User User { get; set; } = default!;
}

