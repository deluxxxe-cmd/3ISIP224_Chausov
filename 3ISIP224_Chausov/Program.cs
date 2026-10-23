using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP224_Chausov
{
    public class TextStatistics
    {
        public string Text { get; private set; }
        public int WordCount { get; private set; }
        public string ShortestWord { get; private set; }
        public int SentenceCount { get; private set; }
        public int VowelCount { get; private set; }
        public int ConsonantCount { get; private set; }
        public string LongestWord { get; private set; }
        public Dictionary<char, int> LetterFrequency { get; private set; }
        public TextStatistics(
            string text,
            int wordCount,
            string shortestWord,
            int sentenceCount,
            int vowelCount,
            int consonantCount,
            string longestWord,
            Dictionary<char, int> letterFrequency)
        {
        }
    }
}
