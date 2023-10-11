using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    [RequireComponent(typeof(AeroplaneController))]
    public class AeroplaneUserMotionControl2 : MonoBehaviour
    {
        // 1と比べてスロットル,ピッチ操作の方法を変更している
        [SerializeField] MotionControl motionControl;

        // these max angles are only used on mobile, due to the way pitch and roll input are handled
        public float maxRollAngle = 80;
        public float maxPitchAngle = 80;
        bool airBrakes = false;

        [Header("Accel")]
        [SerializeField] float minSpeed = 0.0f;
        [SerializeField] float maxSpeed = 0.0f;
        [SerializeField] float accelerationPower = 0.0f; // 加速の強さ
        [SerializeField] float decay = 0.0f;             // 速度減衰の量
        [SerializeField] float dumper = 0.0f;            // ダンパー 入力の機敏さ
        Rigidbody rb;
        float currentSpeed = 0.0f;
        float targetSpeed = 0.0f;

        public static float Throttle { get; private set; }

        // reference to the aeroplane that we're controlling
        private AeroplaneController m_Aeroplane;

        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            m_Aeroplane = GetComponent<AeroplaneController>();
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Read input for the pitch, yaw, roll and throttle of the aeroplane.
            float roll = motionControl.nomalizedRollAngle;
            float pitch = motionControl.nomalizedPitchAngle;
            float yaw = motionControl.nomalizedYawAngle;
            // bool airBrakes = motionControl.airBrakes;

            // auto throttle up, or down if braking.
            // float throttle = airBrakes ? -1 : 1;
            // Throttle = motionControl.nomalizedAccelAmount;
            Throttle = TestProCon.Throttle;

            AdjustInputForMocopiControls(ref roll, ref pitch);

            // Pass the input to the aeroplane
            m_Aeroplane.Move(roll, pitch, 0, 0, airBrakes);

            targetSpeed = Mathf.Clamp(targetSpeed - decay + Throttle * accelerationPower * Time.deltaTime, minSpeed, maxSpeed);  // 目標速度を計算
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, dumper * Time.deltaTime); // 速度減衰
            rb.velocity = transform.forward * currentSpeed;
        }

        private void AdjustInputForMocopiControls(ref float roll, ref float pitch)
        {
            // 体の傾きを直接ロールとピッチに反映する
            float targetRollAngle = -roll * maxRollAngle;
            float targetPitchAngle = pitch * maxPitchAngle;
            transform.localRotation = Quaternion.Euler(new Vector3(targetPitchAngle, transform.localEulerAngles.y, targetRollAngle));
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
