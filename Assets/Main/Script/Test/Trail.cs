using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float destroyTime = 0.0f;
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // じわじわと小さくなる
        this.transform.localScale = new Vector3(this.transform.localScale.x - 0.01f, this.transform.localScale.y - 0.01f, this.transform.localScale.z - 0.01f);
        // カメラのほうを向く
        this.transform.LookAt(Camera.main.transform);
    }
}
