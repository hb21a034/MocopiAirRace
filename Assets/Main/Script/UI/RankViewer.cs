using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankViewer : MonoBehaviour
{
    public TextMeshProUGUI[] RankingText = new TextMeshProUGUI[5];
    // Start is called before the first frame update

    void OnEnable()
    {
        GetRanking();
    }
    void GetRanking()
    {
        int[] nowRanking = RankSystem.LoadRanking();
        for (int i = 0; i < 5; i++)
        {
            RankingText[i].text = nowRanking[i].ToString("N0");
        }
    }
}
