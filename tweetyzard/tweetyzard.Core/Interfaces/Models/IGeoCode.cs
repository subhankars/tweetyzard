﻿using TweetinviCore.Enum;

namespace TweetinviCore.Interfaces.Models
{
    public interface IGeoCode
    {
        ICoordinates Coordinates { get; set; }
        double Radius { get; set; }
        DistanceMeasure DistanceMeasure { get; set; }
    }
}