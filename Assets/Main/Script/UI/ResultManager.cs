using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] GameObject score;
    [SerializeField] GameObject ranking;


    // Start is called before the first frame update
    void Start()
    {
        ShowResult();
    }

    public void ShowRanking()
    {
        score.SetActive(false);
        ranking.SetActive(true);
    }

    public void ShowResult()
    {
        if (ranking != null)
            ranking.SetActive(false);
        if (score != null)
            score.SetActive(true);
    }

    public void LoadStageSelect()
    {
        SceneManager.LoadScene("2_StageSelect");
    }

}
