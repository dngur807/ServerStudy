using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientPacketData
{
    public Int16 PacketSize;
    public Int16 PacketID;
    public byte[] BodyData;
}

public class PacketQueue
{

    public static PacketQueue Instance { get; } = new PacketQueue();

    Queue<ClientPacketData> _packetQueue = new Queue<ClientPacketData>();
    object _lock = new object();

    public void Push(ushort id, ClientPacketData packet)
    {
        lock (_lock)
        {
            _packetQueue.Enqueue(new ClientPacketData()
            {
                PacketSize = packet.PacketSize,
                PacketID = packet.PacketID,
                BodyData = packet.BodyData
            });
        }
    }

    public ClientPacketData Pop()
    {
        lock (_lock)
        {
            if (_packetQueue.Count == 0)
                return null;

            return _packetQueue.Dequeue();
        }
    }

    public List<ClientPacketData> PopAll()
    {
        List<ClientPacketData> list = new List<ClientPacketData>();

        lock (_lock)
        {
            while (_packetQueue.Count > 0)
                list.Add(_packetQueue.Dequeue());
        }

        return list;
    }
}
