using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CheckpointManager : MonoBehaviour
{
    [SerializeField] GameObject[] checkpointsObject;
    public static GameObject[] CheckPointList { get; private set; }
    bool IsGoal = false;

    public static UnityEvent OnGoal = new UnityEvent();


    void Awake()
    {
        for (int i = 0; i < checkpointsObject.Length; i++)
        {
            checkpointsObject[i].GetComponentInChildren<SetCheckpoint>().Number = i + 1;
        }
        CheckPointList = checkpointsObject;
    }

    void Update()
    {
        if (SetCheckpoint.PassedCheckpoint == checkpointsObject.Length)
        {
            if (!IsGoal)
            {
                OnGoal?.Invoke();
            }
            IsGoal = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        //チェックポイントの間に線を引く
        for (int i = 0; i < checkpointsObject.Length - 1; i++)
        {
            if (checkpointsObject[i] != null && checkpointsObject[i + 1] != null)
            {
                Gizmos.DrawLine(checkpointsObject[i].transform.position, checkpointsObject[i + 1].transform.position);
            }
        }
    }

    public void InitAllCheckpoints()
    {
        for (int i = 0; i < checkpointsObject.Length; i++)
        {
            checkpointsObject[i].GetComponent<SetCheckpoint>().enabled = true;
        }
    }
}
