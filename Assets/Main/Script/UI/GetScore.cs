using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    TextMeshProUGUI textmeshPro;

    [SerializeField] int scoreIndex = 0;    // 0: タイム, 1: ブースト, 2: ループ, 3: チェックポイント, 4: 合計
    [SerializeField] int scoreType = 0;     // 0: raw, 1: bass, 2: score

    // Start is called before the first frame update
    void Start()
    {
        textmeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textmeshPro.text = ScoreManager.ScoreDatabase[scoreIndex][scoreType].ToString("N0");
    }
}
