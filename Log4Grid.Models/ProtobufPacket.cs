using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Models
{
    public class ProtobufPacket
    {
        public static ArraySegment<byte> Serialize(object message, byte[] buffer)
        {

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer))
            {
                stream.Position = 0;
                if (message is LogModel)
                {
                    stream.WriteByte((byte)1);
                }
                else
                {
                    stream.WriteByte((byte)2);
                }
                ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(stream, message);
                return new ArraySegment<byte>(buffer, 0, (int)stream.Position);
            }
        }
        public static object Deserialize(ArraySegment<byte> buffer)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer.Array,0,buffer.Count))
            {
                stream.Position = 0;
                int type = stream.ReadByte();
                if (type == 1)
                {
                    return ProtoBuf.Meta.RuntimeTypeModel.Default.Deserialize(stream, null, typeof(LogModel));
                }
                else
                {
                    return ProtoBuf.Meta.RuntimeTypeModel.Default.Deserialize(stream, null, typeof(StatModel));
                }
            }
        }
    }
}
