using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTwistAngle : GetAngle
{
    [SerializeField] GameObject refarance;
    [SerializeField] GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Angle = refarance.transform.rotation.eulerAngles.y - target.transform.rotation.eulerAngles.y;

        if (180 < Angle)
        {
            Angle -= 360;
        }
    }
}
