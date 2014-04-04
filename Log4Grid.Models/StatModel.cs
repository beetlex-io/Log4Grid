using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Log4Grid.Models
{
    [ProtoContract]
    public class StatModel
    {
        [ProtoMember(1)]
        public string Host
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public string App
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public string CpuUsage
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public string MemoryUsage
        {
            get;
            set;
        }
    }
}
