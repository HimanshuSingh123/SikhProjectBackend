namespace Src.Domain.Entities;

public class User
{
    public int UserId {  get; set; }
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string HashedPass { get; set; } = default!;
    public  DateTime CreatedAt { get; set; }
    public string AccountTypeName { get; set; } = default!;

    public ICollection<Submission> Submissions { get; set; } = default!;

    public ICollection<Cart> Carts { get; set; } = default!;
    public ICollection<Favourites> Favourites { get; set; } = default!;
    public ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
    
    public AccountType AccountType { get; set; } = default!;
}