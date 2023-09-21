using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTwistAngle : GetAngle
{
    [SerializeField] GameObject refarance;
    [SerializeField] GameObject target;

    enum AxisType { x, y, z }
    [SerializeField] AxisType useAxis;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (useAxis)
        {
            case AxisType.x:
                Angle = target.transform.localEulerAngles.x - refarance.transform.localEulerAngles.x;
                break;
            case AxisType.y:
                Angle = target.transform.localEulerAngles.y - refarance.transform.localEulerAngles.y;
                break;
            case AxisType.z:
                Angle = target.transform.localEulerAngles.z - refarance.transform.localEulerAngles.z;
                break;
        }

        if (180 < Angle)
        {
            Angle -= 360;
        }
    }
}
