using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyChildrenSphereCollider : MonoBehaviour
{
    [SerializeField] GameObject refarenceObject;
    void Start()
    {
        // 子オブジェクトの SphereCollider コンポーネントを取得
        SphereCollider childCollider = refarenceObject.GetComponent<SphereCollider>();

        if (childCollider != null)
        {
            // 親オブジェクトに SphereCollider が既にあるかどうか確認
            SphereCollider parentCollider = GetComponent<SphereCollider>();

            if (parentCollider == null)
            {
                // 親オブジェクトに SphereCollider がない場合、コピーを作成
                parentCollider = gameObject.AddComponent<SphereCollider>();
            }

            // SphereCollider のパラメータを子オブジェクトのものに合わせる
            parentCollider.center = childCollider.center;
            parentCollider.radius = childCollider.radius;
        }
    }
}
