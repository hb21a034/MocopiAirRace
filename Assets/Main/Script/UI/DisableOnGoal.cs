using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnGoal : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
        CheckpointManager.OnGoal.AddListener(Disable);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
