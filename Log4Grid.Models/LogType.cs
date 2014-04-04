using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Log4Grid.Models
{
    [ProtoContract]
    public enum LogType:int
    {
        [ProtoEnum]
        Debug = 2,
        [ProtoEnum]
        Info = 4,
        [ProtoEnum]
        Warn = 8,
        [ProtoEnum]
        Error = 16,
        [ProtoEnum]
        Fatal = 32
    }
}
