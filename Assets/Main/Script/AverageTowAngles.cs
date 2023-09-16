using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageTowAngles : MonoBehaviour
{
    [SerializeField] GetAngle getAngle1;
    [SerializeField] GetAngle getAngle2;
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
            averageAngle = (getAngle1.angle - getAngle2.angle) / 2;
        }
        else
        {
            averageAngle = (getAngle1.angle + getAngle2.angle) / 2;
        }
        
        if(debug) {Debug.Log(averageAngle);}
    }
}
