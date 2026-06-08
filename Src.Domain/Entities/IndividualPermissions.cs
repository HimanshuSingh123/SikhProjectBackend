namespace Src.Domain.Entities;

public class IndividualPermissions
{
    public string Permission { get; set; } = default!;

    public ICollection<AccountPermissions> AccountPermissions { get; set; } = new List<AccountPermissions>();
}

