using System;
using System.Net;
using TweetinviCore.Helpers;

namespace TweetinviWebLogic
{
    public class WebDownloader : IWebDownloader
    {
        public byte[] DownloadData(string url)
        {
            if (!ValidateUrl(url))
            {
                return null;
            }

            using (var webClient = new WebClient())
            {
                return webClient.DownloadData(new Uri(url));
            }
        }

        public void DownloadFileAsync(
            string url, 
            string filePath, 
            Action<bool> fileDownloaded, 
            Action<long, long> progressChanged = null)
        {
            if (!ValidateUrl(url) || !ValidateFilePath(filePath))
            {
                if (fileDownloaded != null)
                {
                    fileDownloaded(false);
                }

                return;
            }

            var webClient = new WebClient();

            if (progressChanged != null)
            {
                webClient.DownloadProgressChanged += (sender, args) =>
                {
                    progressChanged(args.BytesReceived, args.TotalBytesToReceive);
                };
            }

            if (fileDownloaded != null)
            {
                webClient.DownloadFileCompleted += (sender, args) =>
                {
                    fileDownloaded(!args.Cancelled && args.Error == null);
                };
            }

            var uri = new Uri(url);
            webClient.DownloadFileAsync(uri, filePath);
        }

        public bool DownloadFile(string url, string filePath)
        {
            if (!ValidateUrl(url) || !ValidateFilePath(filePath))
            {
                return false;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, filePath);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DownloadFile(string url, string filename, string folderPath)
        {
            string filePath = String.Format("{0}{1}", folderPath, filename);
            return DownloadFile(url, filePath);
        }

        private bool ValidateUrl(string url)
        {
            return !String.IsNullOrEmpty(url);
        }

        private bool ValidateFilePath(string filePath)
        {
            return !String.IsNullOrEmpty(filePath);
        }
    }
}