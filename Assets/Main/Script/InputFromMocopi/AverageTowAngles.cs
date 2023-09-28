using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageTowAngles : GetAngle
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
            Angle = (getAngle1.Angle - getAngle2.Angle) / 2;
        }
        else
        {
            Angle = (getAngle1.Angle + getAngle2.Angle) / 2;
        }

        if (debug) { Debug.Log(Angle); }
    }

    public override void InitAngle()
    {
        getAngle1.InitAngle();
        getAngle2.InitAngle();
    }
}
