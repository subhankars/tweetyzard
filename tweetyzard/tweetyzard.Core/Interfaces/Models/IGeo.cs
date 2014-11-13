using System.Collections.Generic;

namespace TweetinviCore.Interfaces.Models
{
    /// <summary>
    /// Geographic information of a location
    /// </summary>
    public interface IGeo
    {
        string Type { get; set; }
        List<ICoordinates> Coordinates { get; set; }
    }
}