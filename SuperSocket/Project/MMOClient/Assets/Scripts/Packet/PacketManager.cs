using MessagePack;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PacketDefine;

public class PacketManager
{
    #region Singleton
    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance { get { return _instance; } }
    #endregion


    Dictionary<int, Action<PacketSession, ClientPacketData>> PacketHandlerMap = new Dictionary<int, Action<PacketSession, ClientPacketData>>();

    PacketManager()
    {
        Register();
    }

    public void Register()
    {
        PacketHandlerMap.Add((int)PACKETID.NTF_ENTER_GAME, NotifyEnterGame);
    }

    public void NotifyEnterGame(PacketSession session ,ClientPacketData requestData)
    {
        //S_EnterGame enterGamePacket = packet as S_EnterGame;

        var reqData = MessagePackSerializer.Deserialize<PKTNtfGameEnter>(requestData.BodyData);

        Managers.Object.AddPlayer(reqData.Player, isMe : true);
    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        var bodySize = size - PacketDef.PACKET_HEADER_SIZE;

        var packetBody = new byte[bodySize];
        Buffer.BlockCopy(buffer.Array, PacketDef.PACKET_HEADER_SIZE, packetBody, 0, bodySize);

        var packet = new ClientPacketData();
        packet.PacketID = (short)id;
        packet.PacketSize = (short)size;
        packet.BodyData = packetBody;

        Action<PacketSession, ClientPacketData> action = null;
        if (PacketHandlerMap.TryGetValue(id, out action))
            action.Invoke(session, packet);
    }



    public Action<PacketSession, ClientPacketData> GetPacketHandler(short id)
    {
        Action<PacketSession, ClientPacketData> action = null;
        if (PacketHandlerMap.TryGetValue(id, out action))
            return action;
        return null;
    }


}
