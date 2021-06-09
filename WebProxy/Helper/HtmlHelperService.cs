using System.Text;
using HtmlAgilityPack;

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
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var bodyDivs = doc.DocumentNode.SelectNodes("//body//text()[(normalize-space(.) != '') and not(parent::script or style) and not(*)]");

            if (bodyDivs == null)
            {
                return string.Empty;
            }
            foreach (var div in bodyDivs)
            {
                div.InnerHtml = InsertChar(div.InnerText, charToInsert, requiredWordLength);
            }
            return doc.DocumentNode.InnerHtml;
        }

        private static string InsertChar(string html, char charToInsert, uint requiredWordLength)
        {
            var builder = new StringBuilder();
            var wordCharsCount = 0;
            var isWordInContent = true;
            for (var i = 0; i < html.Length; i++)
            {
                builder.Append(html[i]);
                if (html[i] == '<')
                {
                    isWordInContent = false;
                }

                if (isWordInContent && char.IsLetter(html[i]))
                {
                    if (++wordCharsCount == requiredWordLength && ( i == html.Length -1 || !char.IsLetter(html[i + 1])))
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