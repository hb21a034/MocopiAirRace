using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroplaneMotionControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MotionControl motionControl;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(motionControl.pitchAngle, this.transform.localEulerAngles.y, motionControl.rollAngle);
    }
}
