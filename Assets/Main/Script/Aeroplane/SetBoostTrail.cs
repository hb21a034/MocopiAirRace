using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetBoostTrail : MonoBehaviour
{
    [SerializeField] GameObject boostTrail;
    [SerializeField] GameObject trailPosR;
    [SerializeField] GameObject trailPosL;
    GameObject tmpBoostTrailL;
    GameObject tmpBoostTrailR;
    // Start is called before the first frame update
    void Start()
    {
        SpeedControler.OnBoost.AddListener(InstanciateBoostTrail);
        SpeedControler.OnBoostEnd.AddListener(DestroyBoostTrail);
        CheckpointManager.OnGoal.AddListener(InstanciateBoostTrail);
    }

    void InstanciateBoostTrail()
    {
        tmpBoostTrailL = Instantiate(boostTrail, trailPosR.transform.position, Quaternion.identity);
        tmpBoostTrailR = Instantiate(boostTrail, trailPosL.transform.position, Quaternion.identity);
        tmpBoostTrailL.transform.parent = transform;
        tmpBoostTrailR.transform.parent = transform;
        Debug.Log("trail");
    }
    void DestroyBoostTrail()
    {
        Destroy(tmpBoostTrailL);
        Destroy(tmpBoostTrailR);
    }
}
