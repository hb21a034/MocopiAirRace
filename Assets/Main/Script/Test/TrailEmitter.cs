using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject trailPrefab = null;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(trailPrefab, this.transform.position, this.transform.rotation);
    }
}
