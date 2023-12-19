using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using TMPro;
using System.Net.NetworkInformation;

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
        // iMacで動かない版
        // IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        // foreach (IPAddress ip in hostEntry.AddressList)
        // {
        //     if (ip.AddressFamily == AddressFamily.InterNetwork)
        //     {
        //         ipAddress = ip.ToString();
        //         break;
        //     }
        // }

        foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                {
                    if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = addrInfo.Address.ToString();

                        // use ipAddress as needed ...
                    }
                }
            }
        }
    }
}
