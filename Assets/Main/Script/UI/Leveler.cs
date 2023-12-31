using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leveler : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] GameObject player;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.material.mainTextureOffset = new Vector2((player.transform.localEulerAngles.x + offset) / 360, 0);
    }
}
