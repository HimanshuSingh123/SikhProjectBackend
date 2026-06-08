namespace Src.Domain.Entities;

public class AccountType
{
    public string AccountTypeName { get; set; } = default!;

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<AccountPermissions> AccountPermissions { get; set; } = new List<AccountPermissions>();
}

