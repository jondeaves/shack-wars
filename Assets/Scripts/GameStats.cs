
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static List<int> playerScores = new List<int>();
    public static int Winner { get; set; }

    public static int WinScore { get; set; }

    public static void Reset()
    {
        GameStats.playerScores = new List<int>();
    }
}
