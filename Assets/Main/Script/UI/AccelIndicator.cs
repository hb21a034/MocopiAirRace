using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelIndicator : MonoBehaviour
{
    [SerializeField] float maxHight = 0;
    GameObject defPos;
    // Start is called before the first frame update
    void Start()
    {
        defPos = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
