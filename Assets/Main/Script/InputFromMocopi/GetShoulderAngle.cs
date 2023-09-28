using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetShoulderAngle : GetAngle
{
    // Start is called before the first frame update
    [SerializeField] GameObject origin;
    [SerializeField] GameObject target;
    [SerializeField] GameObject offsetParent;
    GameObject offset;

    enum AxisType { Right, Up, Forward }
    [SerializeField] AxisType useAxis;

    public bool init = false;
    public bool debug = false;

    Vector3 planeNormal = Vector3.zero;

    void Start()
    {
        InitAngle();
        setAxis();

    }

    public override void InitAngle()
    {
        offset = new GameObject("offset");
        offset.transform.parent = offsetParent.transform;
        offset.transform.position = target.transform.position;
        setAxis();
    }

    // Update is called once per frame
    void Update()
    {
        // 空間上のベクトル
        Vector3 from = offset.transform.position - origin.transform.position;
        Vector3 to = target.transform.position - origin.transform.position;

        // 平面に投影されたベクトルを求める
        var planeFrom = Vector3.ProjectOnPlane(from, planeNormal);
        var planeTo = Vector3.ProjectOnPlane(to, planeNormal);

        Angle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);

        if (debug) { Debug.Log(Angle); }

        if (init)
        {
            InitAngle();
            init = false;
        }
    }

    void OnDrawGizmos()
    {
        setAxis();
        // Draws a blue line from this transform to the target
        Gizmos.DrawLine(origin.transform.position, target.transform.position);
        Gizmos.color = Color.gray;
        if (offset != null) { Gizmos.DrawLine(origin.transform.position, offset.transform.position); }

        // 軸の描画
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin.transform.position, planeNormal * 0.1f);
        Gizmos.DrawRay(origin.transform.position, -planeNormal * 0.1f);
    }

    void setAxis()
    {
        switch (useAxis)
        {
            case AxisType.Right:
                planeNormal = origin.transform.right;
                break;
            case AxisType.Up:
                planeNormal = origin.transform.up;
                break;
            case AxisType.Forward:
                planeNormal = origin.transform.forward;
                break;
        }
    }
}
