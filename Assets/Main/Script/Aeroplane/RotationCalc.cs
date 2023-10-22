using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationCalc : MonoBehaviour
{
    public static int VerticalConsecutiveSpinCount { get; private set; }    // 連続回転数
    public static int HorizontalConsecutiveSpinCount { get; private set; }


    private Quaternion previousRotation;
    [SerializeField] float verticalRotation;
    [SerializeField] float horizontalRotation;


    private void Start()
    {
        previousRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        // 現在と過去の回転の差分をとる
        Quaternion angleDelta = Quaternion.Inverse(previousRotation) * transform.rotation;

        //　差分から基準軸に対する回転量をeular角で取得
        float verticalRotationAngle = (angleDelta.eulerAngles.x + 180) % 360 - 180;
        float horizontalRotationAngle = (angleDelta.eulerAngles.z + 180) % 360 - 180;

        // 逆回転したらカウントを初期化
        if (Mathf.Abs(verticalRotation) <= Mathf.Abs(verticalRotationAngle + verticalRotation))
        {
            verticalRotation += verticalRotationAngle;
        }
        else
        {
            verticalRotation = 0;
            VerticalConsecutiveSpinCount = 0;
        }

        if (Mathf.Abs(horizontalRotation) <= Mathf.Abs(horizontalRotationAngle + horizontalRotation))
        {
            horizontalRotation += horizontalRotationAngle;
        }
        else
        {
            horizontalRotation = 0;
            HorizontalConsecutiveSpinCount = 0;
        }

        // +-360度以上回転したらカウントを増やす
        if (360 < Mathf.Abs(verticalRotation))
        {
            SuccessfulVerticalSpin();
            verticalRotation = 0;
        }
        if (360 < Mathf.Abs(horizontalRotation))
        {
            SuccessfulHorizontalSpin();
            horizontalRotation = 0;
        }

        previousRotation = transform.rotation;

    }

    //　回転が成功したときに呼ばれる
    private void SuccessfulVerticalSpin()
    {
        VerticalConsecutiveSpinCount += 1;
        ScoreManager.VerticalSpinCount += 1;
    }
    private void SuccessfulHorizontalSpin()
    {
        HorizontalConsecutiveSpinCount += 1;
        ScoreManager.HorizontalSpinCount += 1;
    }
}
