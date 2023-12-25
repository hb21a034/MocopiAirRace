using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelIndicator : MonoBehaviour
{
    [SerializeField] float maxHight = 0;

    RectTransform rectTransform;
    Vector3 defPos;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        defPos = rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = SpeedControler.Throttle;
        float height = maxHight * speed;
        // Debug.Log(height);

        // rect transformのheightを変更

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Mathf.Abs(height));
        rectTransform.localPosition = new Vector3(defPos.x, defPos.y + (height / 2), defPos.z);

    }
}
