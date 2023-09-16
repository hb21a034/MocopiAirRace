using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTwistAngle : MonoBehaviour
{
    [SerializeField] GameObject refarance;
    [SerializeField] GameObject target;

    enum AxisType {x, y, z}
     [SerializeField] AxisType useAxis;

      public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        switch(useAxis)
        {
            case AxisType.x:
                angle = target.transform.localEulerAngles.x - refarance.transform.localEulerAngles.x;
                break;
            case AxisType.y:
                angle = target.transform.localEulerAngles.y - refarance.transform.localEulerAngles.y;
                break;   
            case AxisType.z:
                angle = target.transform.localEulerAngles.z - refarance.transform.localEulerAngles.z;
                break;
        }

        if (180 < angle)
        {
            angle -= 360;
        }   
    }
}
