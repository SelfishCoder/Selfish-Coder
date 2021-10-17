using System.Text;

namespace SelfishCoder.Extensions.System
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string input)
        {
            return string.Concat(input.Split(' '));
        }
        
        public static string SplitByCamelCase(this string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsUpper(c)) stringBuilder.Append(" ");
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }
    }
}