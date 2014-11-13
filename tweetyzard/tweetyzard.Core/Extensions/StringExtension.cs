using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TweetinviCore.Extensions
{
    /// <summary>
    /// Extension methods on string classes
    /// </summary>
    public static class StringExtension
    {
        private static Regex _linkParser;

        private const string TWITTER_URL_REGEX =
            @"(?<=^|\s+)" +                                            // URL can be prefixed by space or start of line
            @"\b(?<start>http(?<isSecured>s?)://(?:www\.)?|www\.|)" +  // Start of an url
            @"(?<firstPathElement>\w+\.)" +                            // first keyword required
            @"(?<secondPathElement>\w{2,})\w*?" +                      // second keyword required
            @"(?<multiplePathElements>(?:\.\w{2,})*)" +                // potential sub-sites
            @"?\.{0}" +                                                // . is forbidden at this stage
            @"(?<specialChar>[/?])?" +                                 // is there a character specifying the url will be extended
            @"(?(specialChar)" +                                       // has a specialChar been detected
            @"(?:" +                                                   // if so
            @"(?:(?:\w|\d)+)" +                                        // Get all the letters
            @"(?:\p{P}+)" +                                            // Followed by at least 1 or multiple punctuation (twitter behavior)
            @")*(?:(?:\w|\d)+))/?";                                    // And the end should be a literal char or a '/'

        // Create on demand
        private static Regex LinkParser
        {
            get
            {
                if (_linkParser == null)
                {
                    _linkParser = new Regex(TWITTER_URL_REGEX, RegexOptions.IgnoreCase);
                }

                return _linkParser;
            }
        }

        #region Extensions

        /// <summary>
        /// Calculate the length of a string using Twitter algorithm
        /// </summary>
        /// <param name="tweet">Text in the tweet</param>
        /// <returns>Size of the current Tweet</returns>
        public static int TweetLenght(this string tweet)
        {
            if (tweet == null)
            {
                return 0;
            }

            int size = tweet.Length;

            foreach (Match link in LinkParser.Matches(tweet))
            {
                // If an url ends with . and 2 followed chars twitter does not
                // consider it as an URL
                if (link.Groups["start"].Value == "" &&
                    link.Groups["multiplePathElements"].Value == "" &&
                    link.Groups["secondPathElement"].Value.Length <= 2)
                {
                    continue;
                }

                size = size - link.Value.Length + 22;
                size += link.Groups["isSecured"].Value == "s" ? 1 : 0;
            }

            return size;
        }

        /// <summary>
        /// Encode the input string so that it can be sent to Twitter
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TwitterEncode(this string s)
        {
            string encodedString = Uri.EscapeDataString(s);

            return encodedString;
        }

        /// <summary>
        /// Clean a string so that it can be used in a URL and
        /// sent to Twitter
        /// </summary>
        /// <param name="s">String to clean</param>
        /// <returns>Cleaned string</returns>
        public static string CleanString(this string s)
        {
            return s != null ? (s.HTMLDecode().MySQLClean().ReplaceNonPrintableCharacters('\\')) : null;
        }

        /// <summary>
        /// Decode a string formatted to be used within a url
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HTMLDecode(this string s)
        {
            return WebUtility.HtmlDecode(s);
        }

        /// <summary>
        /// Give the ability to replace NonPrintableCharacters to another character
        /// This is very useful for streaming as we receive Tweets from around the world
        /// </summary>
        /// <param name="s">String to be updated</param>
        /// <param name="replaceWith">Character to replace by</param>
        /// <returns>String without any of the special characters</returns>
        public static string ReplaceNonPrintableCharacters(this string s, char replaceWith)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] > 51135)
                {
                    result.Append(replaceWith);
                }

                result.Append(s[i]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Clean the string so that it can be used as 
        /// a parameter of a MySql query
        /// </summary>
        /// <param name="s">String to be updated</param>
        /// <returns>String that can be used within MySql</returns>
        public static string MySQLClean(this string s)
        {
            if (s == null)
            {
                return null;
            }

            StringBuilder result = new StringBuilder(s);
            return Regex.Replace(result.Replace("\\", "\\\\").ToString(), "['′ˈ]", "\\'");
        }

        /// <summary>
        /// Creates a groupName by replacing invalidCharacters with unique groupName
        /// </summary>
        public static string CleanForRegexGroupName(this string groupName)
        {
            string res = Regex.Replace(groupName, @"^[^a-zA-Z]", match => String.Format("special{0}", (int)match.Value[0]));
            return Regex.Replace(res, @"[^a-zA-Z0-9]", match => String.Format("special{0}", (int)match.Value[0]));
        }

        /// <summary>
        /// Clean a keyword that you want to search with a regex
        /// </summary>
        public static string CleanForRegex(this string regexKeyword)
        {
            return Regex.Replace(regexKeyword, @"[.^$*+?()[{\|#]", match => String.Format(@"\{0}", match));
        }

        /// <summary>
        /// Create a filtering Regex for all the expected keywords and creates a Group that you can inspect
        /// to see if the specific keyword has been matched
        /// </summary>
        public static string RegexFiltering(string[] keywords)
        {
            StringBuilder patternBuilder = new StringBuilder();
            foreach (var keywordPattern in keywords)
            {
                patternBuilder.Append(String.Format(@"(?=.*(?<{0}>(?:^|\s+){1}(?:\s+|$)))?",
                   CleanForRegexGroupName(keywordPattern), CleanForRegex(keywordPattern)));
            }

            // Check the first group to analyze the result of the Regex : 
            // MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);
            // GroupCollection groups = matches[0].Groups;

            return patternBuilder.ToString();
        }

        #endregion
    }
}