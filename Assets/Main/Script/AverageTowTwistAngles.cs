using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AverageTowTwistAngles : GetAngle
{
    [SerializeField] GetAngle getAngle1;
    [SerializeField] GetAngle getAngle2;
    [SerializeField] bool invart = false;

    [SerializeField] bool debug = false;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (invart)
        {
            Angle = getAngle1.Angle / 2 - getAngle2.Angle / 2;
        }
        else
        {
            Angle = getAngle1.Angle / 2 + getAngle2.Angle / 2;
        }

        if (debug) { Debug.Log(Angle); }
    }
}
