using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    [SerializeField] GameObject[] checkpointsObject;
    [SerializeField] TargetIndicator targetIndicator;

    void Awake()
    {
        for (int i = 0; i < checkpointsObject.Length; i++)
        {
            checkpointsObject[i].GetComponent<SetCheckpoint>().Number = i + 1;
        }
    }

    void Update()
    {
        // 次のチェックポイントをターゲットにする
        targetIndicator.target = checkpointsObject[SetCheckpoint.PassedCheckpoint].transform;
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
}
