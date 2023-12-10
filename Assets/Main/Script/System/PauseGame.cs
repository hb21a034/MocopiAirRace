using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        CheckpointManager.OnGoal.AddListener(Destroy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Pause()
    {
        // ポーズ中は時間を止める
        Time.timeScale = 0f;
        // ポーズ画面をアクティブにする
        pauseUI.SetActive(true);
    }
    void Resume()
    {
        // ポーズを解除する
        Time.timeScale = 1f;
        // ポーズ画面を非アクティブにする
        pauseUI.SetActive(false);
    }

    public void TogglePause()
    {
        // ゲームがポーズ中なら再開を、していなければポーズをする
        if (Time.timeScale == 0f)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
        Debug.Log("destroy");
    }
}
