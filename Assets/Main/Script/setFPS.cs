using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFPS : MonoBehaviour
{
    // 起動時にfpsを固定する
    [SerializeField] private int targetFPS = 0;
    private void Awake()
    {
        if (targetFPS != 0)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFPS;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
