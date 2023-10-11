using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ResetPosition : MonoBehaviour
{
    GameObject resetPosition;

    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.gameObject;
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
