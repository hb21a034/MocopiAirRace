using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CountdownController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float countdownTime = 3f;
    public static UnityEvent OnStart = new UnityEvent();


    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            countdownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        // ゲームを開始する処理をここに追加
        StartGame();

        gameObject.SetActive(false);
    }

    void StartGame()
    {
        // ゲーム開始時の処理をここに追加
        OnStart?.Invoke();
        Debug.Log("Game Started!");
    }
}
