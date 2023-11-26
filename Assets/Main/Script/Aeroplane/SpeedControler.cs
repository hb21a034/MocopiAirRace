using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Vehicles.Aeroplane;

public class SpeedControler : MonoBehaviour
{
    [SerializeField] float minSpeed = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float boostSpeed = 0.0f;
    [SerializeField] float boostPower = 0.0f;
    [SerializeField] float accelerationPower = 0.0f; // 加速の強さ
    [SerializeField] float decay = 0.0f;             // 速度減衰の量
    [SerializeField] float dumper = 0.0f;            // ダンパー 入力の機敏さ
    [SerializeField] float boostTime = 3.0f;         // ブーストの時間
    Rigidbody rb;
    float currentSpeed = 0.0f;
    float targetSpeed = 0.0f;

    public static bool IsBoost { get; private set; } = false;
    public static int RemainBoostCount { get; set; }
    public static float Throttle { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RemainBoostCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Throttle = TestProCon.Throttle;
        FixedAeroplaneController.IsBoost = IsBoost;

        if (IsBoost)
        {
            targetSpeed = Mathf.Clamp(targetSpeed + 1 * accelerationPower * boostPower * Time.deltaTime, minSpeed, boostSpeed);

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
    public void BoostButton(InputAction.CallbackContext context)
    {
        if (context.performed && 0 < RemainBoostCount && !IsBoost)
        {
            IsBoost = true;
            ScoreManager.BoostCount++;
            RemainBoostCount--;
            Debug.Log("boost");
            StartCoroutine(BoostEnd());
        }
    }

    // n秒後にboost解除
    IEnumerator BoostEnd()
    {
        yield return new WaitForSeconds(boostTime);
        IsBoost = false;
    }
}
