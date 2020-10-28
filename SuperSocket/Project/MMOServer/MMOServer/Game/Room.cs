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

        object m_lock = new object();

        public void Init(int index, int number, int maxUserCount, int mapId)
        {
            Index = index;
            Number = number;
            MaxUserCount = maxUserCount;
            m_map.LoadMap(mapId);
        }

        public void EnterGame(int playerID, string netSessionID)
        {
            Player player = null;
            m_playerList.TryGetValue(playerID, out player);

            if (player != null)
                return;

            lock (m_lock)
            {
                player = new Player();
                player.Set(playerID, netSessionID, this);
                m_playerList.Add(playerID, player);

                // 본인한테 정보 전송
                { 
                }

                // 타인에게 정보 전송
                { 
                }
            }
        }

        public bool GetUser(int playerId)
        {
            Player player = null;
            return m_playerList.TryGetValue(playerId, out player);
        }


    }
}
