using Src.Domain.Entities;

public class AccountPermissions
{
    public string AccountTypeName { get; set; } = default!;
    public string Permission { get; set; } = default!;

    public AccountType AccountType { get; set; } = default!;
    public IndividualPermissions IndividualPermissions { get; set; } = default!;
}