using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideImage : MonoBehaviour
{
    [SerializeField] Sprite[] documents;
    Image image;
    int nowPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = documents[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Send")]
    void PageSend()
    {
        if (nowPage < documents.Length - 1)
        {
            nowPage++;
            image.sprite = documents[nowPage];
        }
    }
    [ContextMenu("Back")]
    void PageBack()
    {
        if (0 < nowPage)
        {
            nowPage--;
            image.sprite = documents[nowPage];
        }
    }

}
