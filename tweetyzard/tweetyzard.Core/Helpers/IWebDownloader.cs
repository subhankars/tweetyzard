using System;

namespace TweetinviCore.Helpers
{
    public interface IWebDownloader
    {
        byte[] DownloadData(string url);

        bool DownloadFile(string url, string filePath);
        bool DownloadFile(string url, string filename, string folderPath);

        void DownloadFileAsync(
            string url,
            string filePath,
            Action<bool> fileDownloaded,
            Action<long, long> progressChanged = null);
    }
}