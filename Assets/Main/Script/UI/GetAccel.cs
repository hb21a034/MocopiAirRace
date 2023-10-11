using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetAccel : MonoBehaviour
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
        textmeshPro.text = SpeedControler.Throttle.ToString("F2");
    }
}
