using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainTime : MonoBehaviour
{
    TextMeshProUGUI textmeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textmeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameTimer.RemainTime < 0)
        {
            textmeshPro.text = "Time Up";
        }
        else
        {
            textmeshPro.text = GameTimer.RemainTime.ToString("F1");
        }
    }
}
