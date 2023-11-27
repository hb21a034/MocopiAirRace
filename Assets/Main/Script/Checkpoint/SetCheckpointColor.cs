using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpointColor : MonoBehaviour
{
    Material material;
    [SerializeField, ColorUsage(true, true)] Color defaultColor;
    [SerializeField, ColorUsage(true, true)] Color nextColor;
    bool isChanged = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        ChangeColor(defaultColor);
    }


    // Update is called once per frame
    void Update()
    {
        if (isChanged && SetCheckpoint.PassedCheckpoint + 1 == transform.parent.GetComponent<SetCheckpoint>().Number)
        {
            ChangeColor(nextColor);
        }
    }

    void ChangeColor(Color setColor)
    {
        material.SetColor("_CircleColor", setColor);
        isChanged = true;
    }
}
