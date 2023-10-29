using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Aeroplane;

public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        if (FixedAeroplaneUserMotionControl.Player != null)
        {
            this.transform.LookAt(FixedAeroplaneUserMotionControl.Player.transform);
        }
    }
}
