using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    public class FixedAeroplaneUserMotionControl : MonoBehaviour
    {
        [SerializeField] MotionControl motionControl;
        [SerializeField] AnimationCurve pitchCurve;

        bool airBrakes = false;
        private FixedAeroplaneController m_Aeroplane;
        public static GameObject Player { get; private set; }

        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            m_Aeroplane = GetComponent<FixedAeroplaneController>();
            Player = this.gameObject;
        }

        private void FixedUpdate()
        {
            float roll = motionControl.nomalizedRollAngle;
            float pitch = motionControl.nomalizedPitchAngle;
            pitch = pitchCurve.Evaluate(pitch);
            float yaw = motionControl.nomalizedYawAngle;
            // bool airBrakes = motionControl.airBrakes;

            // float throttle = airBrakes ? -1 : 1;
            float throttle = motionControl.nomalizedAccelAmount;
            SpeedControler.Throttle = throttle;
            // float throttle = TestProCon.Throttle;

            m_Aeroplane.Move(roll, pitch, 0, throttle, airBrakes);
        }

        public void AirBrakeButton(InputAction.CallbackContext context) // ここに書かないほうがいい気がする
        {
            if (context.started)
            {
                airBrakes = true;
                Debug.Log("AirBrakeButton+");
            }
            else if (context.canceled)
            {
                airBrakes = false;
                Debug.Log("AirBrakeButton-");
            }
        }
    }
}
