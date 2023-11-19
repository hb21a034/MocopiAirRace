using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledOnGoal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        CheckpointManager.OnGoal.AddListener(Enable);
    }

    void Enable()
    {
        gameObject.SetActive(true);
    }
}
