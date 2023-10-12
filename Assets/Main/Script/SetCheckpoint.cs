using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SetCheckpoint : MonoBehaviour
{
    [SerializeField] TextMeshPro myText;
    [SerializeField] bool showNumber = true;

    public static int PassedCheckpoint { get; private set; }
    public int Number { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        PassedCheckpoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (showNumber)
        {
            myText.text = (Number - PassedCheckpoint).ToString();
        }
    }

    // playerタグのオブジェクトに触れたら
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 通過したチェックポイントが次のチェックポイントなら
            if (PassedCheckpoint + 1 == Number)
            {
                PassedCheckpoint++;
                ScoreCalc(other.gameObject);

                this.gameObject.SetActive(false);
            }
            else
            {
                // 直前のチェックポイントの位置まで戻す？　後で考える
            }
        }
    }

    void ScoreCalc(GameObject player)
    {
        // 自信とプレイヤーの距離を計算
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        // 距離に応じてスコアを加算　後で調整
        ScoreManager.Score += 1000;
    }
}
