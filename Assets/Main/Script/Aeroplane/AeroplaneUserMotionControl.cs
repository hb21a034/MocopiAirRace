using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    [RequireComponent(typeof(AeroplaneController))]
    public class AeroplaneUserMotionControl : MonoBehaviour
    {
        [SerializeField] MotionControl motionControl;

        // these max angles are only used on mobile, due to the way pitch and roll input are handled
        public float maxRollAngle = 80;
        public float maxPitchAngle = 80;
        bool airBrakes = false;

        [SerializeField] AnimationCurve pitchCurve;

        // reference to the aeroplane that we're controlling
        private AeroplaneController m_Aeroplane;

        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            m_Aeroplane = GetComponent<AeroplaneController>();
        }

        private void FixedUpdate()
        {
            /* Read input for the pitch, yaw, roll and throttle of the aeroplane. */
            float roll = -motionControl.nomalizedRollAngle;
            float pitch = motionControl.nomalizedPitchAngle;
            float yaw = motionControl.nomalizedYawAngle;
            // bool airBrakes = motionControl.airBrakes;

            /* auto throttle up, or down if braking. */
            // float throttle = airBrakes ? -1 : 1;
            float throttle = motionControl.nomalizedAccelAmount;
            // float throttle = TestProCon.Throttle;

            // AdjustInputForMocopiControls(ref roll, ref pitch);
            AdjustInputForMocopiControls2(ref roll, ref pitch);

            /* Pass the input to the aeroplane */
            // m_Aeroplane.Move(roll, pitch, 0, throttle, airBrakes);
            m_Aeroplane.Move(0, pitch, 0, 0, airBrakes);
        }

        private void AdjustInputForMocopiControls(ref float roll, ref float pitch)
        {
            // 体の傾きを直接ロールとピッチに反映する
            float targetRollAngle = roll * maxRollAngle;
            float targetPitchAngle = pitch * maxPitchAngle;
            transform.localRotation = Quaternion.Euler(new Vector3(targetPitchAngle, transform.localEulerAngles.y, targetRollAngle));
        }

        private void AdjustInputForMocopiControls2(ref float roll, ref float pitch)
        {
            // 体の傾きを直接ロールに反映する ピッチは標準のまま
            float targetRollAngle = roll * maxRollAngle * (Mathf.Cos(transform.localEulerAngles.x * Mathf.Deg2Rad) * 0.9f + 0.05f);
            pitch = pitchCurve.Evaluate(pitch);
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetRollAngle));
        }

        public void AirBrakeButton(InputAction.CallbackContext context)
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
