using System.Text;

namespace IdolVillage.Utilities
{
    internal static class StringUtilities
    {
        internal static string GetDescriptionForTooltip(string description)
        {
            var maxLineLength = 30;
            string[] words = description.Split(' ');
            StringBuilder formattedDescription = new StringBuilder();
            int currentLineLength = 0;

            foreach (string word in words)
            {
                if (currentLineLength + word.Length + 1 > maxLineLength)
                {
                    formattedDescription.Append("\n");
                    currentLineLength = 0;
                }

                formattedDescription.Append(word + " ");
                currentLineLength += word.Length + 1;
            }

            return formattedDescription.ToString().Trim();
        }
    }
}
