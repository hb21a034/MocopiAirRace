using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] ScoreElementBass[] scoreElementBass = new ScoreElementBass[4];
    [SerializeField] TextMeshProUGUI totalScore;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetScoreText();
    }

    void SetScoreText()
    {
        for (int i = 0; i < scoreElementBass.Length; i++)
        {
            if (scoreElementBass[i].raw != null)
                scoreElementBass[i].raw.text = ScoreManager.ScoreDatabase[i][0].ToString();
            if (scoreElementBass[i].bass != null)
                scoreElementBass[i].bass.text = ScoreManager.ScoreDatabase[i][1].ToString();
            if (scoreElementBass[i].score != null)
                scoreElementBass[i].score.text = ScoreManager.ScoreDatabase[i][2].ToString();
        }
        totalScore.text = ScoreManager.ScoreDatabase[4][2].ToString();
    }
}
