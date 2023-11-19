using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPlayerIntrusion : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 親が次のちぇっくぽいんとなら
            if (transform.parent.GetComponent<SetCheckpoint>().Number - SetCheckpoint.PassedCheckpoint == 1)
            {
                // 親オブジェクトの SphereCollider コンポーネントを取得
                SphereCollider parentCollider = transform.parent.GetComponent<SphereCollider>();
                // 自身の SphereCollider コンポーネントを取得
                SphereCollider myCollider = GetComponent<SphereCollider>();
                // 親のcolliderを自身のcolliderで上書き
                parentCollider.radius = myCollider.radius * transform.localScale.x;

                // プレイヤーがチェックポイントを通過したら
                // this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
