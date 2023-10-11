using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetSpeed : MonoBehaviour
{
    [SerializeField] GameObject player;
    TextMeshProUGUI textmeshPro;


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        textmeshPro = GetComponent<TextMeshProUGUI>();
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rb.velocity.magnitude;
        textmeshPro.text = speed.ToString("F0");
    }
}
