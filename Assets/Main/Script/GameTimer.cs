using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float timeLimit;
    bool isWorking = false;
    float startTime;
    public static float RemainTime { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        TimerStart();
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
            else if (CheckpointManager.IsGoal)
            {
                TimerEnd();
                ScoreManager.Score += (int)(RemainTime * 100);
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
    }
}
