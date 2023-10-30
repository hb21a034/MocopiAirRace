using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    public class AeroplaneUserControl2Axis : MonoBehaviour
    {
        // reference to the aeroplane that we're controlling
        // private AeroplaneController m_Aeroplane;
        private FixedAeroplaneController m_Aeroplane;

        private void Awake()
        {
            // m_Aeroplane = GetComponent<AeroplaneController>();
            m_Aeroplane = GetComponent<FixedAeroplaneController>();
        }

        private void FixedUpdate()
        {
            // Read input for the pitch, yaw, roll and throttle of the aeroplane.
            float roll = CrossPlatformInputManager.GetAxis("Horizontal");
            float pitch = CrossPlatformInputManager.GetAxis("Vertical");
            bool airBrakes = CrossPlatformInputManager.GetButton("Fire1");

            // auto throttle up, or down if braking.
            float throttle = airBrakes ? -1 : 1;


            // Pass the input to the aeroplane
            m_Aeroplane.Move(roll, pitch, 0, throttle, airBrakes);
        }
    }
}
