using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut.Mappings;

namespace Log4Grid.DBModels
{
    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_Application")]
    public partial class DBApplication : Peanut.Mappings.DataObject
    {
        private string mID;
        public static Peanut.FieldInfo<string> iD = new Peanut.FieldInfo<string>("TBL_Application", "ID");
        private string mName;
        public static Peanut.FieldInfo<string> name = new Peanut.FieldInfo<string>("TBL_Application", "Name");
        private string mRemark;
        public static Peanut.FieldInfo<string> remark = new Peanut.FieldInfo<string>("TBL_Application", "Remark");
        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        [Peanut.Mappings.UID]
        public virtual string ID
        {
            get
            {
                return mID;
                
            }
            set
            {
                mID = value;
                EntityState.FieldChange("ID");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Name
        {
            get
            {
                return mName;
                
            }
            set
            {
                mName = value;
                EntityState.FieldChange("Name");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Remark
        {
            get
            {
                return mRemark;
                
            }
            set
            {
                mRemark = value;
                EntityState.FieldChange("Remark");
                
            }
            
        }
        
    }
    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_Host")]
    public partial class DBHost : Peanut.Mappings.DataObject
    {
        private string mID;
        public static Peanut.FieldInfo<string> iD = new Peanut.FieldInfo<string>("TBL_Host", "ID");
        private string mAppID;
        public static Peanut.FieldInfo<string> appID = new Peanut.FieldInfo<string>("TBL_Host", "AppID");
        private string mName;
        public static Peanut.FieldInfo<string> name = new Peanut.FieldInfo<string>("TBL_Host", "Name");
        private string mCpuUsage;
        public static Peanut.FieldInfo<string> cpuUsage = new Peanut.FieldInfo<string>("TBL_Host", "CpuUsage");
        private string mMemoryUsage;
        public static Peanut.FieldInfo<string> memoryUsage = new Peanut.FieldInfo<string>("TBL_Host", "MemoryUsage");
        private DateTime mLastActiveTime;
        public static Peanut.FieldInfo<DateTime> lastActiveTime = new Peanut.FieldInfo<DateTime>("TBL_Host", "LastActiveTime");
        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        [Peanut.Mappings.UID]
        public virtual string ID
        {
            get
            {
                return mID;
                
            }
            set
            {
                mID = value;
                EntityState.FieldChange("ID");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string AppID
        {
            get
            {
                return mAppID;
                
            }
            set
            {
                mAppID = value;
                EntityState.FieldChange("AppID");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Name
        {
            get
            {
                return mName;
                
            }
            set
            {
                mName = value;
                EntityState.FieldChange("Name");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string CpuUsage
        {
            get
            {
                return mCpuUsage;
                
            }
            set
            {
                mCpuUsage = value;
                EntityState.FieldChange("CpuUsage");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string MemoryUsage
        {
            get
            {
                return mMemoryUsage;
                
            }
            set
            {
                mMemoryUsage = value;
                EntityState.FieldChange("MemoryUsage");
                
            }
            
        }
        ///<summary>
        ///Type:DateTime
        ///</summary>
        [Column()]
        public virtual DateTime LastActiveTime
        {
            get
            {
                return mLastActiveTime;
                
            }
            set
            {
                mLastActiveTime = value;
                EntityState.FieldChange("LastActiveTime");
                
            }
            
        }
        
    }
    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_Log")]
    public partial class DBLog : Peanut.Mappings.DataObject
    {
        private string mID;
        public static Peanut.FieldInfo<string> iD = new Peanut.FieldInfo<string>("TBL_Log", "ID");
        private string mHost;
        public static Peanut.FieldInfo<string> host = new Peanut.FieldInfo<string>("TBL_Log", "Host");
        private string mApp;
        public static Peanut.FieldInfo<string> app = new Peanut.FieldInfo<string>("TBL_Log", "App");
        private DateTime mCreateTime;
        public static Peanut.FieldInfo<DateTime> createTime = new Peanut.FieldInfo<DateTime>("TBL_Log", "CreateTime");
        private string mContent;
        public static Peanut.FieldInfo<string> content = new Peanut.FieldInfo<string>("TBL_Log", "LogContent");
        private Log4Grid.Models.LogType mType;
        public static Peanut.FieldInfo<Log4Grid.Models.LogType> type = new Peanut.FieldInfo<Log4Grid.Models.LogType>("TBL_Log", "Type");
        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        [UID]
        public virtual string ID
        {
            get
            {
                return mID;
                
            }
            set
            {
                mID = value;
                EntityState.FieldChange("ID");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Host
        {
            get
            {
                return mHost;
                
            }
            set
            {
                mHost = value;
                EntityState.FieldChange("Host");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string App
        {
            get
            {
                return mApp;
                
            }
            set
            {
                mApp = value;
                EntityState.FieldChange("App");
                
            }
            
        }
        ///<summary>
        ///Type:DateTime
        ///</summary>
        [Column()]
        public virtual DateTime CreateTime
        {
            get
            {
                return mCreateTime;
                
            }
            set
            {
                mCreateTime = value;
                EntityState.FieldChange("CreateTime");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column("LogContent")]
        public virtual string Content
        {
            get
            {
                return mContent;
                
            }
            set
            {
                mContent = value;
                EntityState.FieldChange("Content");
                
            }
            
        }
        ///<summary>
        ///Type:Log4Grid.Models.LogType
        ///</summary>
        [Column()]
        [EnumToInt]
        public virtual Log4Grid.Models.LogType Type
        {
            get
            {
                return mType;
                
            }
            set
            {
                mType = value;
                EntityState.FieldChange("Type");
                
            }
            
        }
        
    }
    ///<summary>
    ///Peanut Generator Copyright @ FanJianHan 2010-2013
    ///website:http://www.ikende.com
    ///</summary>
    [Table("TBL_User")]
    public partial class DBUser : Peanut.Mappings.DataObject
    {
        private string mName;
        public static Peanut.FieldInfo<string> name = new Peanut.FieldInfo<string>("TBL_User", "Name");
        private string mPassword;
        public static Peanut.FieldInfo<string> password = new Peanut.FieldInfo<string>("TBL_User", "User_PWD");
        private string mEmail;
        public static Peanut.FieldInfo<string> email = new Peanut.FieldInfo<string>("TBL_User", "Email");
        private bool mEnabled;
        public static Peanut.FieldInfo<bool> enabled = new Peanut.FieldInfo<bool>("TBL_User", "Enabled");
        ///<summary>
        ///Type:string
        ///</summary>
        [ID()]
        public virtual string Name
        {
            get
            {
                return mName;
                
            }
            set
            {
                mName = value;
                EntityState.FieldChange("Name");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column("User_PWD")]
        [StringCrypto("log4grid")]
        public virtual string Password
        {
            get
            {
                return mPassword;
                
            }
            set
            {
                mPassword = value;
                EntityState.FieldChange("Password");
                
            }
            
        }
        ///<summary>
        ///Type:string
        ///</summary>
        [Column()]
        public virtual string Email
        {
            get
            {
                return mEmail;
                
            }
            set
            {
                mEmail = value;
                EntityState.FieldChange("Email");
                
            }
            
        }
        ///<summary>
        ///Type:bool
        ///</summary>
        [Column()]
        [BoolToInt]
        public virtual bool Enabled
        {
            get
            {
                return mEnabled;
                
            }
            set
            {
                mEnabled = value;
                EntityState.FieldChange("Enabled");
                
            }
            
        }
        
    }
    
}
