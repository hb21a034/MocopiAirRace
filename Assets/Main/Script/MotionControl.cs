using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// モーションから取得したデータを平滑化してまとめる
public class MotionControl : MonoBehaviour
{
    [SerializeField] AverageTowAngles roll;
    [SerializeField] GetShoulderAngle pitch;
    [SerializeField] AverageTowTwistAngles yaw;
    [SerializeField] AverageTowAngles accel;

    [SerializeField, Range(0, 1)] float smoothingFactor = 0.1f;
    // Start is called before the first frame update

    public float rollAngle = 0f;
    public float pitchAngle = 0f;
    public float yawAngle = 0f;
    public float accelAmount = 0f;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {  
        rollAngle = SmoothingMotionData(roll.averageAngle, rollAngle);
        pitchAngle = SmoothingMotionData(pitch.angle, pitchAngle);
        yawAngle = SmoothingMotionData(yaw.averageAngle, yawAngle);
        accelAmount = SmoothingMotionData(accel.averageAngle, accelAmount);
    }

     public float SmoothingMotionData(float rawData, float smoothedValue)
    {
        // ローパスフィルターを適用して値を平滑化する
        smoothedValue = Mathf.Lerp(smoothedValue, rawData, smoothingFactor);
        return smoothedValue;
    }
}
