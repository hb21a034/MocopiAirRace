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
        float time = GameTimer.RemainTime;
        if (time <= 0)
        {
            textmeshPro.text = "000";
        }
        else if (time < 10)
        {
            string timetext = "00" + time.ToString("F0");
            textmeshPro.text = timetext;
        }
        else if (time < 100)
        {
            string timetext = "0" + time.ToString("F0");
            textmeshPro.text = timetext;
        }
        else
        {
            textmeshPro.text = time.ToString("F0");
        }
    }
}
