using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] float delaySpeed = 60f;        // DelayZoneに入った時の速度
    Rigidbody rb;
    float currentSpeed = 0.0f;
    float targetSpeed = 0.0f;

    public static bool IsBoost { get; private set; } = false;
    public static int RemainBoostCount { get; set; }
    public static float Throttle { get; set; }
    public static UnityEvent OnBoost = new UnityEvent();
    public static UnityEvent OnBoostEnd = new UnityEvent();
    public static UnityEvent OnDelay = new UnityEvent();
    public static UnityEvent OnDelayEnd = new UnityEvent();

    Coroutine oldBoostEnd;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RemainBoostCount = 1;
        IsBoost = false;
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
            Boost();
            RemainBoostCount--;
        }
    }

    public void Boost()
    {
        IsBoost = true;
        OnBoost?.Invoke();
        ScoreManager.BoostCount++;
        Debug.Log("boost");

        // ブースト中なら以前のコルーチンを再起動
        if (oldBoostEnd != null)
        {
            StopCoroutine(oldBoostEnd);
            OnBoostEnd?.Invoke();
        }
        oldBoostEnd = StartCoroutine(BoostEnd());

    }

    // n秒後にboost解除
    IEnumerator BoostEnd()
    {
        yield return new WaitForSeconds(boostTime);
        OnBoostEnd?.Invoke();
        IsBoost = false;
    }

    // DelayZoneに入ったら速度制限
    float defaultMaxSpeed;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DelayZone"))
        {
            defaultMaxSpeed = maxSpeed;
            maxSpeed = delaySpeed;
            OnDelay?.Invoke();
        }
    }

    // DelayZoneから出たら速度制限解除
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DelayZone"))
        {
            maxSpeed = defaultMaxSpeed;
            OnDelayEnd?.Invoke();
        }
    }
}
