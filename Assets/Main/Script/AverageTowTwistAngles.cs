using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageTowTwistAngles : MonoBehaviour
{
    [SerializeField] GetTwistAngle getAngle1;
    [SerializeField] GetTwistAngle getAngle2;
    [SerializeField] bool invart = false;
    public float averageAngle;

    [SerializeField] bool debug = false;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invart)
        {
            averageAngle = getAngle1.angle/2 - getAngle2.angle/2;
        }
        else
        {
            averageAngle = getAngle1.angle/2 + getAngle2.angle/2 ;
        }
        
        if(debug) {Debug.Log(averageAngle);}
    }
}
