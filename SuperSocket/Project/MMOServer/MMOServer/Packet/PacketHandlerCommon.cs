using MMOServer.Game;
using ServerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMOServer.Packet
{
    public class PacketHandlerCommon : PacketHandler
    {
        public void RegistPacketHandler(Dictionary<int, Action<ServerPacketData>> packetHandlerMap)
        {
            packetHandlerMap.Add((int)PACKETID.NTF_IN_CONNECT_CLIENT, NotifyInConnectClient);

            packetHandlerMap.Add((int)PACKETID.REQ_LOGIN, RequestLogin);
        }

        public void NotifyInConnectClient(ServerPacketData requestData)
        {
            var sessionID = packetData.SessionID;

            var player = PlayerManager.Instance.Add();
            var temp_Info = player.Info;
            var temp_PosInfo = player.Info.posInfo;
            {
                temp_Info.name = $"Player_{temp_Info.playerId}";
                temp_PosInfo.state = CreatureState.IDLE;
                temp_PosInfo.moveDir = MoveDir.NONE;
                temp_PosInfo.posX = 0;
                temp_PosInfo.posY = 0;
                temp_Info.posInfo = temp_PosInfo;
                player.Info = temp_Info;
            }

            RoomManager.Instance.Find(1).EnterGame(player);

            MainServer.MainLogger.Debug($"Current Connected Session Count: {ServerNetwork.SessionCount}");
        }

        public void RequestLogin(ServerPacketData packetData)
        {
            var sessionID = packetData.SessionID;
            MainServer.MainLogger.Debug("로그인 요청 받음");
        }
    }
}
