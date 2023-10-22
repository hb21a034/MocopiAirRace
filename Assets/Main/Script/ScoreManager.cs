using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.PlayerLoop;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int nowScore = 0;
    [SerializeField] int boostCount = 0;
    [SerializeField] float remainingTime = 0;
    [SerializeField] int verticalSpinCount = 0;
    [SerializeField] int horizontalSpinCount = 0;

    public static int Score { get; private set; }

    public static int BoostCount { get; set; }          // ブースト回数
    public static float RemainingTime { get; set; }       // 残り時間
    public static int VerticalSpinCount { get; set; }   // 総縦回転数
    public static int HorizontalSpinCount { get; set; } // 総横回転数
    List<int> checkPointScore = new List<int>();       // チェックポイント通過時のスコアのリスト
    public static ScoreManager instance;                // シングルトン

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); }
    }

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

    public void OnPassedCheckpoint(int score)
    {
        checkPointScore.Add(score);
        Score += score;
    }
    public void GameEnd(float time)
    {
        RemainingTime = time;
        Score += (int)(RemainingTime * 10000);
        Score += BoostCount * 100000;
        Score += VerticalSpinCount * 100000;
        Score += HorizontalSpinCount * 100000;
    }

    void init()
    {
        Score = 0;
        BoostCount = 0;
        RemainingTime = 0;
        VerticalSpinCount = 0;
        HorizontalSpinCount = 0;
        checkPointScore.Clear();
    }
}
