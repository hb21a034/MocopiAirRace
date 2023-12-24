using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankSystem : MonoBehaviour
{
    [SerializeField] int stage = 0;
    [SerializeField] static int stageNum = 0;
    int maxStage = 3;

    void Start()
    {
        stageNum = stage * 100;
    }

    public static int[] LoadRanking()
    {
        int[] Ranking = new int[6];

        for (int i = 1; i < 6; i++)
        {
            int rankName = i + stageNum;
            Ranking[i - 1] = PlayerPrefs.GetInt((rankName).ToString());
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
            int rankName = i + stageNum;
            PlayerPrefs.SetInt(rankName.ToString(), nowRanking[^i]);
        }
    }

    public void ResetRanking()
    {
        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j < maxStage; j++)
            {
                int rankName = j + stageNum;
                PlayerPrefs.SetInt(rankName.ToString(), 0);
            }
        }
    }
}
