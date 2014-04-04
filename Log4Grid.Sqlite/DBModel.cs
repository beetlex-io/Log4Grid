using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut.Mappings;
namespace Log4Grid.Sqlite
{
    [Table("Application")]
    interface IDBApplication
    {
        [ID]
        [Peanut.Mappings.UID]
        string ID { get; set; }
        [Column]
        string Name { get; set; }
        [Column]
        string Remark { get; set; }
    }
    [Table("Host")]
    interface IDBHost
    {
        [ID]
        [Peanut.Mappings.UID]
        string ID { get; set; }
        [Column]
        string AppID { get; set; }
        [Column]
        string Name
        {
            get;
            set;
        }
        [Column]
        string CpuUsage
        {
            get;
            set;
        }
        [Column]
        string MemoryUsage
        {
            get;
            set;
        }
        [Column]
        DateTime LastActiveTime { get; set; }
    }
    [Table("Log")]
    interface IDBLog
    {
        [ID]
        [UID]
        string ID { get; set; }
        [Column]
        string Host
        {
            get;
            set;
        }
        [Column]
        string App
        {
            get;
            set;
        }
        [Column]
        DateTime CreateTime
        {
            get;
            set;
        }
        [Column]
        string Content
        {
            get;
            set;
        }
        [Column]
        [EnumToInt]
        Log4Grid.Models.LogType Type
        {
            get;
            set;
        }
    }
    [Table("User")]
    interface IDBUser
    {
        [ID]
        string Name { get; set; }
        [Column]
        [StringCrypto("log4grid")]
        string Password { get; set; }
        [Column]
        string Email { get; set; }
        [Column]
        [BoolToInt]
        bool Enabled { get; set; }
    }

}
