using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class SpeedControler : MonoBehaviour
{
    [SerializeField] float minSpeed = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float boostSpeed = 0.0f;
    [SerializeField] float boostPower = 0.0f;
    [SerializeField] float accelerationPower = 0.0f; // 加速の強さ
    [SerializeField] float decay = 0.0f;             // 速度減衰の量
    [SerializeField] float dumper = 0.0f;            // ダンパー 入力の機敏さ
    Rigidbody rb;
    float currentSpeed = 0.0f;
    float targetSpeed = 0.0f;
    bool boost = false;

    public static float Throttle { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Throttle = TestProCon.Throttle;

        if (boost)
        {
            targetSpeed = Mathf.Clamp(targetSpeed - decay + Throttle * accelerationPower * boostPower * Time.deltaTime, minSpeed, boostSpeed);
        }
        else
        {
            // 目標速度を計算
            targetSpeed = Mathf.Clamp(targetSpeed - decay + Throttle * accelerationPower * Time.deltaTime, minSpeed, maxSpeed);
        }

        // 速度減衰
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, dumper * Time.deltaTime);

        // Rigidbodyの速度を設定
        rb.velocity = transform.forward * currentSpeed;

    }
    public void AirBrakeButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            boost = true;
            ScoreManager.BoostCount++;
            Debug.Log("boost");
        }
        else if (context.canceled)
        {
            boost = false;
            Debug.Log("boost off-");
        }
    }
}
