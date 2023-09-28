using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// モーションから取得したデータを平滑化してまとめる
public class MotionControl : MonoBehaviour
{
    [SerializeField] GetAngle roll;
    [SerializeField] GetAngle pitch;
    [SerializeField] GetAngle yaw;
    [SerializeField] GetAngle accel;

    // Start is called before the first frame update

    [Header("Roll")]
    [SerializeField, Range(0f, 90f)] float maxRollAngle = 80f;
    [SerializeField, Range(0f, -90f)] float minRollAngle = -80f;
    [SerializeField, Range(0f, 1f)] float rollSmoothingFactor = 0.1f;
    public float rollAngle = 0f;
    public float nomalizedRollAngle = 0f;

    [Header("Pitch")]
    [SerializeField, Range(0f, 90f)] float maxPitchAngle = 80f;
    [SerializeField, Range(0f, -90f)] float minPitchAngle = -80f;
    [SerializeField, Range(0f, 0.3f)] float pitchSmoothingFactor = 0.1f;
    public float pitchAngle = 0f;
    public float nomalizedPitchAngle = 0f;

    [Header("Yaw")]
    [SerializeField, Range(0f, 90f)] float maxYawAngle = 80f;
    [SerializeField, Range(0f, -90f)] float minYawAngle = -80f;
    [SerializeField, Range(0f, 0.3f)] float yawSmoothingFactor = 0.1f;
    public float yawAngle = 0f;
    public float nomalizedYawAngle = 0f;

    [Header("Accel")]
    [SerializeField, Range(0f, 90f)] float maxAccelAngle = 80f;
    [SerializeField, Range(0f, -90f)] float minAccelAngle = -80f;
    [SerializeField, Range(0f, 0.3f)] float accelSmoothingFactor = 0.1f;
    public float accelAmount = 0f;
    public float nomalizedAccelAmount = 0f;

    [Header("AirBrakes")]
    [SerializeField] float airBreakAngle = -50f;
    public bool airBrakes = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 平滑化
        rollAngle = SmoothingMotionData(roll.Angle, rollAngle, rollSmoothingFactor);
        pitchAngle = SmoothingMotionData(pitch.Angle, pitchAngle, pitchSmoothingFactor);
        yawAngle = SmoothingMotionData(yaw.Angle, yawAngle, yawSmoothingFactor);
        accelAmount = SmoothingMotionData(accel.Angle, accelAmount, accelSmoothingFactor);

        // 正規化
        nomalizedRollAngle = -NomalizeAngle(rollAngle, maxRollAngle, minRollAngle);
        nomalizedPitchAngle = NomalizeAngle(pitchAngle, maxPitchAngle, minPitchAngle);
        nomalizedYawAngle = NomalizeAngle(yawAngle, maxYawAngle, minYawAngle);
        nomalizedAccelAmount = NomalizeAngle(accelAmount, maxAccelAngle, minAccelAngle);

        // ブレーキ
        if (accelAmount < airBreakAngle)
        {
            airBrakes = true;
        }
        else
        {
            airBrakes = false;
        }

    }

    public float SmoothingMotionData(float rawData, float smoothedValue, float smoothingFactor)
    {
        // ローパスフィルターを適用して値を平滑化する
        smoothedValue = Mathf.Lerp(smoothedValue, rawData, smoothingFactor);
        return smoothedValue;
    }

    // 角度を-1~1の間で正規化する
    float NomalizeAngle(float angle, float maxAngle, float minAngle)
    {
        float nomalizeAngle = 0f;
        if (0 < angle)
        {
            nomalizeAngle = angle / maxAngle;
            if (1 < nomalizeAngle)
            {
                nomalizeAngle = 1;
            }
        }
        else
        {
            nomalizeAngle = -angle / minAngle;
            if (nomalizeAngle < -1)
            {
                nomalizeAngle = -1;
            }
        }

        return nomalizeAngle;
    }
}
