namespace IdolVillage.Utilities
{
    internal static class EnumHelper
    {
        public static string ToTitleCase(this Enum input)
        {
            var name = string.Join("", input.ToString().ToCharArray().Select(x => char.IsUpper(x) ? " " + x : x.ToString()));
            return name;
        }
    }
}
