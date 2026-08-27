namespace Src.Dto.Common;

public static class ItemCategories
{
    public const string Course = "Course";
    public const string Merch = "Merch";

    public static List<string> GetCategories()
    {
        return new List<string> {Course, Merch};
    }
}

