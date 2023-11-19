using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityStandardAssets.Vehicles.Aeroplane;

public class SetCheckpoint : MonoBehaviour
{
    [SerializeField] TextMeshPro myText;
    [SerializeField] bool showNumber = true;
    [SerializeField] int score = 0;

    [SerializeField] int[] baseScore = new int[3] { 100, 50, 20 };

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

    // playerタグのオブジェクトから離れたら
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 通過したチェックポイントが次のチェックポイントなら
            if (PassedCheckpoint + 1 == Number)
            {
                PassedCheckpoint++;
                ScoreCalc();

                this.gameObject.SetActive(false);
                // Debug.Log("プレイヤーが出た");
            }
            else
            {
                // 直前のチェックポイントの位置まで戻す？　後で考える
            }
        }
    }

    void ScoreCalc()
    {
        // 子要素がいくつかカウント
        int childCount = transform.childCount;
        // 距離に応じてスコアを加算　後で調整
        switch (childCount)
        {
            case 0:
                score = baseScore[0];
                break;
            case 1:
                score = baseScore[1];
                break;
            case 2:
                score = baseScore[2];
                break;
            default:
                score = 0;
                break;
        }
        ScoreManager.CheckPointScore.Add(score);
        SpeedControler.RemainBoostCount++;
        // Debug.Log(childCount);
    }
}
