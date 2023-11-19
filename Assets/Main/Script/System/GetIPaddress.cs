using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using TMPro;

public class GetIPaddress : MonoBehaviour
{
    [SerializeField] string ipAddress = "000.000.000.000";

    void Start()
    {
        GetIPAddress();
        if (TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
        {
            textMeshProUGUI.text = ipAddress;
        }
    }

    [ContextMenu("GetIPAddress")]
    public void GetIPAddress()
    {
        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in hostEntry.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = ip.ToString();
                break;
            }
        }
    }
}
