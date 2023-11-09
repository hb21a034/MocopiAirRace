using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
    [RequireComponent(typeof(Rigidbody))]
    public class FixedAeroplaneController : MonoBehaviour
    {
        [SerializeField] private float m_RollEffect = 1f;             // The strength of effect for roll input.
        [SerializeField] private float rollBoost = 1f;
        [SerializeField] private float m_PitchEffect = 1f;            // The strength of effect for pitch input.
        [SerializeField] private float m_YawEffect = 0.2f;            // The strength of effect for yaw input.
        [SerializeField] private float m_BankedTurnEffect = 0.5f;     // The amount of turn from doing a banked turn.
        [SerializeField] private float m_AerodynamicEffect = 0.02f;   // How much aerodynamics affect the speed of the aeroplane.

        [SerializeField] private float m_AirBrakesEffect = 3f;        // How much the air brakes effect the drag.
        [SerializeField] private float m_DragIncreaseFactor = 0.001f; // how much drag should increase with speed.

        public bool AirBrakes { get; private set; }                     // Whether or not the air brakes are being applied.
        public float ForwardSpeed { get; private set; }                 // How fast the aeroplane is traveling in it's forward direction.
        public float RollAngle { get; private set; }
        public float PitchAngle { get; private set; }
        public float RollInput { get; private set; }
        public float PitchInput { get; private set; }
        public float YawInput { get; private set; }
        public float ThrottleInput { get; private set; }
        public static bool IsBoost { get; set; }

        private float m_OriginalDrag;         // The drag when the scene starts.
        private float m_OriginalAngularDrag;  // The angular drag when the scene starts.
        private float m_AeroFactor;
        private bool m_Immobilized = false;   // used for making the plane uncontrollable, i.e. if it has been hit or crashed.
        private float m_BankedTurnAmount;
        private Rigidbody m_Rigidbody;
        WheelCollider[] m_WheelColliders;


        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            // Store original drag settings, these are modified during flight.
            m_OriginalDrag = m_Rigidbody.drag;
            m_OriginalAngularDrag = m_Rigidbody.angularDrag;

            for (int i = 0; i < transform.childCount; i++)
            {
                foreach (var componentsInChild in transform.GetChild(i).GetComponentsInChildren<WheelCollider>())
                {
                    componentsInChild.motorTorque = 0.18f;
                }
            }
        }


        public void Move(float rollInput, float pitchInput, float yawInput, float throttleInput, bool airBrakes)
        {
            // transfer input parameters into properties.s
            RollInput = rollInput;
            PitchInput = pitchInput;
            YawInput = yawInput;
            ThrottleInput = throttleInput;
            AirBrakes = airBrakes;

            ClampInputs();

            CalculateRollAndPitchAngles();

            CalculateForwardSpeed();

            CalculateDrag();

            CaluclateAerodynamicEffect();

            CalculateTorque();
        }


        private void ClampInputs()
        {
            // clamp the inputs to -1 to 1 range
            RollInput = Mathf.Clamp(RollInput, -1, 1);
            PitchInput = Mathf.Clamp(PitchInput, -1, 1);
            YawInput = Mathf.Clamp(YawInput, -1, 1);
            ThrottleInput = Mathf.Clamp(ThrottleInput, -1, 1);
        }


        private void CalculateRollAndPitchAngles() // 現在の傾きを計算
        {
            // Calculate roll & pitch angles
            // Calculate the flat forward direction (with no y component).
            var flatForward = transform.forward;
            flatForward.y = 0;
            // If the flat forward vector is non-zero (which would only happen if the plane was pointing exactly straight upwards)
            if (flatForward.sqrMagnitude > 0)
            {
                flatForward.Normalize();
                // calculate current pitch angle
                var localFlatForward = transform.InverseTransformDirection(flatForward);
                PitchAngle = Mathf.Atan2(localFlatForward.y, localFlatForward.z);
                // calculate current roll angle
                var flatRight = Vector3.Cross(Vector3.up, flatForward);
                var localFlatRight = transform.InverseTransformDirection(flatRight);
                RollAngle = Mathf.Atan2(localFlatRight.y, localFlatRight.x);
            }
        }

        private void CalculateForwardSpeed() // 現在の速度を計算
        {
            // Forward speed is the speed in the planes's forward direction (not the same as its velocity, eg if falling in a stall)
            var localVelocity = transform.InverseTransformDirection(m_Rigidbody.velocity);
            ForwardSpeed = Mathf.Max(0, localVelocity.z);
            // Debug.Log(ForwardSpeed);
        }

        private void CalculateDrag() // 回転抑制　うまいこときつくすればいい感じになる？
        {
            // increase the drag based on speed, since a constant drag doesn't seem "Real" (tm) enough
            float extraDrag = m_Rigidbody.velocity.magnitude * m_DragIncreaseFactor;
            // Air brakes work by directly modifying drag. This part is actually pretty realistic!
            m_Rigidbody.drag = (AirBrakes ? (m_OriginalDrag + extraDrag) * m_AirBrakesEffect : m_OriginalDrag + extraDrag);
            // Forward speed affects angular drag - at high forward speed, it's much harder for the plane to spin
            m_Rigidbody.angularDrag = m_OriginalAngularDrag * ForwardSpeed;
        }

        private void CaluclateAerodynamicEffect() // これがないと動かないなんで？
        {
            // "Aerodynamic" calculations. This is a very simple approximation of the effect that a plane
            // will naturally try to align itself in the direction that it's facing when moving at speed.
            // Without this, the plane would behave a bit like the asteroids spaceship!
            if (m_Rigidbody.velocity.magnitude > 0)
            {
                // compare the direction we're pointing with the direction we're moving:
                m_AeroFactor = Vector3.Dot(transform.forward, m_Rigidbody.velocity.normalized);
                // multipled by itself results in a desirable rolloff curve of the effect
                m_AeroFactor *= m_AeroFactor;
                // Finally we calculate a new velocity by bending the current velocity direction towards
                // the the direction the plane is facing, by an amount based on this aeroFactor
                var newVelocity = Vector3.Lerp(m_Rigidbody.velocity, transform.forward * ForwardSpeed,
                                               m_AeroFactor * ForwardSpeed * m_AerodynamicEffect * Time.deltaTime);
                m_Rigidbody.velocity = newVelocity;

                // also rotate the plane towards the direction of movement - this should be a very small effect, but means the plane ends up
                // pointing downwards in a stall

                m_Rigidbody.rotation = Quaternion.Slerp(m_Rigidbody.rotation,
                                                      Quaternion.LookRotation(m_Rigidbody.velocity, transform.up),
                                                      m_AerodynamicEffect * Time.deltaTime);
            }
        }

        private void CalculateTorque() // 回転系の制御
        {
            // We accumulate torque forces into this variable:
            var torque = Vector3.zero;
            // Add torque for the pitch based on the pitch input.
            torque += PitchInput * m_PitchEffect * transform.right;
            // Add torque for the yaw based on the yaw input.
            torque += YawInput * m_YawEffect * transform.up;
            // Add torque for the roll based on the roll input.
            // boost中のロール入力を強くする
            if (IsBoost)
            {
                torque += -RollInput * m_RollEffect * transform.forward * rollBoost;
                // Debug.Log("rollBoost");
            }
            else
            {
                torque += -RollInput * m_RollEffect * transform.forward;
                // Debug.Log("roll");
            }

            // torque += -RollInput * m_RollEffect * transform.forward;
            // Add torque for banked turning.
            torque += m_BankedTurnAmount * m_BankedTurnEffect * transform.up;
            // The total torque is multiplied by the forward speed, so the controls have more effect at high speed,
            // and little effect at low speed, or when not moving in the direction of the nose of the plane
            // (i.e. falling while stalled)
            m_Rigidbody.AddTorque(torque * ForwardSpeed * m_AeroFactor);
        }

        // 機体操作不能にしたいときに呼び出す
        // Immobilize can be called from other objects, for example if this plane is hit by a weapon and should become uncontrollable
        public void Immobilize() { m_Immobilized = true; }
        // Reset is called via the ObjectResetter script, if present.
        public void Reset() { m_Immobilized = false; }
    }
}
