using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Models
{
    public class ApplicationData
    {
        public ApplicationData()
        {

        }

        public string ID { get; set; }

        public string Name
        {
            get;
            set;
        }

        public IList<ApplicationHost> Hosts
        {
            get;
            set;
        }

    }
    public class ApplicationHost
    {
        public string ID { get; set; }

        public string Name
        {
            get;
            set;
        }

        public string CpuUsage
        {
            get;
            set;
        }

        public string MemoryUsage
        {
            get;
            set;
        }

        public DateTime LastActiveTime { get; set; }

        public bool Enabled { get; set; }


    }
}
