using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankSystem : MonoBehaviour
{
    public static int[] LoadRanking()
    {
        int[] Ranking = new int[6];

        for (int i = 1; i < 6; i++)
        {
            Ranking[i - 1] = PlayerPrefs.GetInt(i.ToString());
        }
        return Ranking;
    }

    public static void SaveRanking()
    {
        int[] nowRanking = LoadRanking();
        nowRanking[5] = ScoreManager.Score;
        System.Array.Sort(nowRanking);
        for (int i = 1; i < 6; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), nowRanking[^i]);
        }
    }
}
