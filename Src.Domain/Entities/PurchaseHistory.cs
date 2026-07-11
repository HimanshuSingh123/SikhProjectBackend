namespace Src.Domain.Entities;
public class PurchaseHistory
{
    public int TransactionId { get; set; }
    public string Username { get; set; } = default!;
    public string ItemTitle { get; set; } = default!;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string ItemType { get; set; } = default!;
    public DateTime PurchaseTimestamp { get; set; }
    public int? SubmissionId { get; set; }
    
    //submission nullable because it might not exist when you delete it;
    public Submission? Submission { get; set; }
    public User User { get; set; } = default!;
}

