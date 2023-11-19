using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OrbitTheTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 1f;

    float time = 0f;
    bool isWorking = false;
    // Start is called before the first frame update
    void Start()
    {
        CheckpointManager.OnGoal.AddListener(setTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking) { Orbit(); }
    }

    void Orbit()
    {
        time += Time.deltaTime;
        // ターゲットの周囲をdistanceの距離を保ちながらspeedの速さで周回する
        transform.position = target.position
                                + new Vector3(math.cos(time * speed) * distance,
                                              height,
                                              math.sin(time * speed) * distance);

        // ターゲットの方向を向く
        transform.LookAt(target);
    }

    void setTime()
    {
        isWorking = true;
        time = 5 / speed;
    }
}
