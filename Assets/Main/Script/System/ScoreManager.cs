using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int nowScore = 0;
    [SerializeField] int boostCount = 0;
    [SerializeField] float remainingTime = 0;
    [SerializeField] int verticalSpinCount = 0;
    [SerializeField] int horizontalSpinCount = 0;

    [Space(10)]
    [Header("基礎点 上から順に,タイム,ブースト,回転,チェックポイント")]
    [SerializeField] int[] baseScore = new int[4];

    public static int Score { get; private set; }

    public static int BoostCount { get; set; }          // ブースト回数
    public static float RemainingTime { get; set; }     // 残り時間
    public static int VerticalSpinCount { get; set; }   // 総縦回転数
    public static int HorizontalSpinCount { get; set; } // 総横回転数

    public static List<int> CheckPointScore { get; set; } = new List<int>();   // チェックポイント通過時のスコアのリスト
    public static float[][] ScoreDatabase { get; private set; } = new float[5][]; // スコア関連のデータをすべて保存


    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用
        nowScore = Score;
        boostCount = BoostCount;
        remainingTime = RemainingTime;
        verticalSpinCount = VerticalSpinCount;
        horizontalSpinCount = HorizontalSpinCount;
    }

    public void GameEnd(float time)
    {
        RemainingTime = time;
        ScoreCalc();
    }

    // すべてのスコアを計算しデータベースに登録
    void ScoreCalc()
    {
        // タイム
        float[] timeData = new float[3];
        timeData[0] = RemainingTime;
        timeData[1] = baseScore[0];
        timeData[2] = timeData[0] * timeData[1];
        ScoreDatabase[0] = timeData;

        // ブースト
        float[] boostData = new float[3];
        boostData[0] = BoostCount;
        boostData[1] = baseScore[1];
        boostData[2] = BoostCount * baseScore[1];
        ScoreDatabase[1] = boostData;

        // 回転
        float[] spinData = new float[3];
        spinData[0] = VerticalSpinCount + HorizontalSpinCount;
        spinData[1] = baseScore[2];
        spinData[2] = (VerticalSpinCount + HorizontalSpinCount) * baseScore[2];
        ScoreDatabase[2] = spinData;

        // チェックポイント
        float[] checkPointData = new float[3];
        checkPointData[0] = CheckPointScore.Sum();
        checkPointData[1] = baseScore[3];
        checkPointData[2] = CheckPointScore.Sum() * baseScore[3];
        ScoreDatabase[3] = checkPointData;

        // 合計
        float[] totalData = new float[3];
        for (int i = 0; i < ScoreDatabase.Length - 1; i++)
        {
            totalData[2] += ScoreDatabase[i][2];
        }
        ScoreDatabase[4] = totalData;

        Score = (int)ScoreDatabase[4][2];
        RankSystem.SaveRanking();
    }

    void init()
    {
        Score = 0;
        BoostCount = 0;
        RemainingTime = 0;
        VerticalSpinCount = 0;
        HorizontalSpinCount = 0;
        CheckPointScore.Clear();
    }
}
