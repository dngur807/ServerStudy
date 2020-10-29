using MessagePack;
using MMOServer.Packet;
using ServerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMOServer.Game
{
    public class Room
    {
        public int Index { get; private set; }
        public int Number { get; private set; }
        int MaxUserCount = 0;
        Dictionary<int, Player> m_playerList = new Dictionary<int, Player>();
        Map m_map = new Map();


        public void Init(int index, int number, int maxUserCount, int mapId)
        {
            Index = index;
            Number = number;
            MaxUserCount = maxUserCount;
           // m_map.LoadMap(mapId);
        }

        public void EnterGame(Player player, string netSessionID)
        {
            if (player == null)
                return;
            MainServer.MainLogger.Debug($"룸 입장 PID { player.Info.playerId }");

            player.Set(player.Info.playerId, netSessionID, this);
            m_playerList.Add(player.Info.playerId, player);

            // 본인한테 정보 전송
            {
                var ntfGameEnter = new PKTNtfGameEnter()
                {
                    Player = player.Info
                };
                var bodyData = MessagePackSerializer.Serialize<PKTNtfGameEnter>(ntfGameEnter);
                var reqData = MessagePackSerializer.Deserialize<PKTNtfGameEnter>(bodyData);


                var sendData = PacketToBytes.Make(PACKETID.NTF_ENTER_GAME, bodyData);
                PacketProcessor.MainServer.SendData(player.NetSessionID, sendData);

                // 본인에게 다른 사람 정보 전달.
                var ntfRoomPlayerList = new PKTNtfRoomPlayerList();
                foreach (Player p in m_playerList.Values)
                {
                    if (player != p)
                        ntfRoomPlayerList.players.Add(p.Info);
                }
                var bodyData2 = MessagePackSerializer.Serialize(ntfRoomPlayerList);
                var sendData2 = PacketToBytes.Make(PACKETID.NTF_ROOM_PLAYER_LIST, bodyData2);
                PacketProcessor.MainServer.SendData(player.NetSessionID, sendData2);
                Console.WriteLine($"PKTNtfGameEnter 본인에게 전송 {m_playerList.Count}");
            }

            // 타인에게 정보 전송
            {
                var ntfGameEnter = new PKTNtfGameEnter();
                ntfGameEnter.Player = player.Info;
                var bodyData = MessagePackSerializer.Serialize(ntfGameEnter);
                var sendData = PacketToBytes.Make(PACKETID.NTF_ENTER_GAME, bodyData);
                foreach (Player p in m_playerList.Values)
                {
                    if (player != p)
                        PacketProcessor.MainServer.SendData(p.NetSessionID, sendData);

                }
                Console.WriteLine($"PKTNtfGameEnter 타인에게 전송 {m_playerList.Count}");
            }
        }

        public bool GetUser(int playerId)
        {
            Player player = null;
            return m_playerList.TryGetValue(playerId, out player);
        }


    }
}
