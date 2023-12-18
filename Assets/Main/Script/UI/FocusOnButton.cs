using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FocusOnButton : MonoBehaviour
{
    EventSystem eventSystem;
    GameObject focusButton;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のフォーカスされているゲームオブジェクトの取得
        GameObject focusedObject = eventSystem.currentSelectedGameObject;

        // フォーカスの有無を確認
        if (focusedObject == null)
        {
            // フォーカスされていない場合は、最初のボタンにフォーカスを移す
            eventSystem.SetSelectedGameObject(focusButton);
        }
        focusButton = focusedObject;
    }
}
