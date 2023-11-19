using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusOnEnable : MonoBehaviour
{
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }
    void OnEnable()
    {
        button.Select();
    }
}
