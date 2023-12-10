using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    PauseGame pauseGame;

    // Start is called before the first frame update
    void Start()
    {
        pauseGame = GameObject.Find("GameManager").GetComponent<PauseGame>();
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("2_StageSelect");
    }

    public void RetryButton()
    {
        pauseGame.TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeButton()
    {
        pauseGame.TogglePause();
    }
}
