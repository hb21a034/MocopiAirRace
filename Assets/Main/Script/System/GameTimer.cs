using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float timeLimit;
    bool isWorking = false;
    float startTime;
    public static float RemainTime { get; private set; }

    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        CountdownController.OnStart.AddListener(TimerStart);
        CheckpointManager.OnGoal.AddListener(TimerEnd);
        scoreManager = GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            RemainTime = timeLimit - (Time.time - startTime);
            if (RemainTime <= 0)
            {
                TimerEnd();
            }
        }
    }

    void TimerStart()
    {
        startTime = Time.time;
        isWorking = true;
    }
    void TimerEnd()
    {
        isWorking = false;
        scoreManager.GameEnd(RemainTime);
    }
}
