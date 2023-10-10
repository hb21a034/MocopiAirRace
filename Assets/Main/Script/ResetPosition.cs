using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ResetPosition : MonoBehaviour
{
    GameObject resetPosition;

    Vector3 previousPosition;
    float speed;

    [SerializeField] TextMeshProUGUI textmeshPro;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // // 速度の計算
        // Vector3 currentPosition = transform.position;
        // Vector3 displacement = currentPosition - previousPosition;
        // float deltaTime = Time.deltaTime;
        // speed = displacement.magnitude / deltaTime;
        // previousPosition = currentPosition;

        speed = rb.velocity.magnitude;
        // speedをTMproに表示
        textmeshPro.text = speed.ToString("F2");

    }

    public void BuckCheckPoint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (SetCheckpoint.PassedCheckpoint == 0)
            {
                this.transform.position = resetPosition.transform.position;
            }
            else
            {
                this.transform.position = CheckpointManager.CheckPointList[SetCheckpoint.PassedCheckpoint - 1].transform.position;
                this.transform.rotation = CheckpointManager.CheckPointList[SetCheckpoint.PassedCheckpoint - 1].transform.rotation;
            }
            Debug.Log("Reset");
        }
    }

}
