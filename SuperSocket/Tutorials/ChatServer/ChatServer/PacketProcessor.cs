using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace ChatServer
{
    class PacketProcessor
    {
        bool IsThreadRunning = false;

        // receive쪽에서 처리하지 않아도 Post에서 블럭킹 되지 않는다.
        // BufferBlock<T>(DataflowBlockOptions) 에서 DataflowBlockOptions의 BoundedCapacity로 버퍼 가능 수 지정. BoundedCapacity 보다 크게 쌓이면 블럭킹 된다
        BufferBlock<ServerPacketData> MsgBuffer = new BufferBlock<ServerPacketData>();

        public void InsertPacket(ServerPacketData data)
        {
            MsgBuffer.Post(data);
        }

        public void Destory()
        {
            IsThreadRunning = false;
            MsgBuffer.Complete();
        }

    }
}
