using System;
using System.IO;
using System.Net;
using TweetinviCore.Helpers;

namespace TweetinviWebLogic
{
    public class WebHelper : IWebHelper
    {
        public Stream GetResponseStream(string url)
        {
            if (!ValidateUrl(url))
            {
                return null;
            }

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            return response.GetResponseStream();
        }

        private bool ValidateUrl(string url)
        {
            return !String.IsNullOrEmpty(url);
        }
    }
}