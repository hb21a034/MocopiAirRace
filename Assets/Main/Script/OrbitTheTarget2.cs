using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbitTheTarget2 : MonoBehaviour
{
    public Transform target;  // 追跡する対象のTransform
    public float followSpeed = 5f;  // 移動のスムーズさを調整するパラメータ
    public float rotationSpeed = 2f;  // 回転のスムーズさを調整するパラメータ
    public float orbitRadius = 5f;  // 目標の周りを回る半径

    Vector3 oldPosition;

    bool isWorking = false;

    void Start()
    {
        CheckpointManager.OnGoal.AddListener(Init);
    }

    void Update()
    {
        if (isWorking)
        {
            target.transform.localEulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
            transform.localPosition = new Vector3(0, 0, orbitRadius);

            Vector3 smoothRotate = Vector3.Lerp(oldPosition, target.position, followSpeed);
            transform.position = smoothRotate;

            // ターゲットの方向を向く
            transform.LookAt(target);

            oldPosition = transform.position;
        }
    }

    void Init()
    {
        isWorking = true;
        transform.SetParent(target);
        transform.localPosition = new Vector3(0, 0, orbitRadius);
    }
}
