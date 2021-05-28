using System.Text;

namespace WebProxy.Helper
{
    public class HtmlHelperService
    {
        /// <summary>
        /// Insert char after every word in content with specified length
        /// </summary>
        /// <param name="html">Html to change</param>
        /// <param name="charToInsert">Char to insert</param>
        /// <param name="requiredWordLength">Word length</param>
        /// <returns>Page specified char after every word with specified length</returns>
        public static string InsertCharAfterEveryWordWithNCharsInContent(string html, char charToInsert,
            uint requiredWordLength)
        {
            var builder = new StringBuilder();
            var wordCharsCount = 0;
            var isWordInContent = false;
            for (var i = 0; i < html.Length; i++)
            {
                builder.Append(html[i]);

                if (html[i] == '<')
                {
                    isWordInContent = false;
                }

                if (isWordInContent && char.IsLetter(html[i]))
                {
                    if (++wordCharsCount == requiredWordLength && !char.IsLetter(html[i + 1]))
                    {
                        builder.Append(charToInsert);
                    }
                }
                else
                {
                    wordCharsCount = 0;
                }


                if (html[i] == '>')
                {
                    isWordInContent = true;
                }
            }

            return builder.ToString();
        }
    }
}