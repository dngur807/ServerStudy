using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager 
{
    ServerSession _session = new ServerSession();


    public void Init()
    {

        IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 8888);

        Connector connector = new Connector();
        connector.Connect(endPoint, () => { return _session; }, 1);
    }

    public void Update()
    {
        List<ClientPacketData> list = PacketQueue.Instance.PopAll();
        foreach (ClientPacketData packet in list)
        {
            Action<PacketSession , ClientPacketData> handler =  PacketManager.Instance.GetPacketHandler(packet.PacketID);
            if (handler != null)
                handler.Invoke(_session , packet);

            Debug.Log($"update packet ID {packet.PacketID}");
        }
    }
}
