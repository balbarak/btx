using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.MockData
{
    public class LoremGenerator
    {
        public static Random Random { get; set; } = new Random();


        public static string GenerateText(int numberOfWords,int numberOfLines = 1)
        {
            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var sb = new StringBuilder();

            for (int p = 0; p < numberOfLines; p++)
            {

                for (int w = 0; w < numberOfWords; w++)
                {
                    if (w > 0)
                        sb.Append(" ");

                    string word = words[Random.Next(words.Length)];

                    if (w == 0)
                        word = word.Substring(0, 1).Trim().ToUpper() + word.Substring(1);

                    sb.Append(word);
                }

                if (p < numberOfLines - 1)
                    sb.Append("\n\n");
            }

            return sb.ToString();
        }
    }
}
