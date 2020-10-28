using ServerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMOServer.Game
{
    public class Player
    {
        public PlayerInfo Info { get; set; } = new PlayerInfo() { posInfo = new PositionInfo() };
        public string UserID { get; private set; }
        public string NetSessionID { get; private set; }
        // 현재 속한 룸정보
        public Room MyRoom { get; private set; }

        public void Set(int playerID, string netSessionID, Room room)
        {
           // PlayerID = playerID;
            NetSessionID = netSessionID;
            MyRoom = room;
        }

    }
}
