using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLib
{
    public class PacketDef
    {
        public const Int16 PACKET_HEADER_SIZE = 5;
        public const int MAX_USER_ID_BYTE_LENGTH = 16;
        public const int MAX_USER_PW_BYTE_LENGTH = 16;

        public const int INVALID_ROOM_NUMBER = -1;
    }

    public enum CreatureState
    {
        IDLE = 0,
        MOVING = 1,
        SKILL = 2,
        DEAD = 3,
    }

    public enum MoveDir
    {
        NONE = 0,
        UP = 1,
        DOWN = 2,
        LEFT = 3,
        RIGHT = 4
    }

    public struct PlayerInfo
    {
        public Int32 playerId;
        public string name { get; set; }
        public PositionInfo posInfo { get; set; }
    }

    public struct PositionInfo
    {
        public CreatureState state;
        public MoveDir moveDir;
        public Int32 posX; 
        public Int32 posY;
    }

    [MessagePackObject]
    public class PKTNtfRoomEnter
    {
        [Key(0)]
        public PlayerInfo player;
    }

    [MessagePackObject]
    public class PKTNtfRoomPlayerList
    {
        [Key(0)]
        public List<PlayerInfo> players = new List<PlayerInfo>();
    }



}
