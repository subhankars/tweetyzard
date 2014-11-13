﻿using System;
using System.Collections.Generic;

namespace TweetinviCore.Interfaces.Streaminvi
{
    public interface IStreamTrackManager<T> : ITrackManager<T>, ITrackStringAnalyzer
    {
        List<Tuple<string, Action<T>>> GetMatchingTracksAndActions(string inputs);
    }
}