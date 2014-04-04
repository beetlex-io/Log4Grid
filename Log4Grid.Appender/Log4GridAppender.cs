using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Util;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Log4Grid.Appender
{
    public class Log4GridAppender : log4net.Appender.AppenderSkeleton
    {

        private byte[] mBuffer = new Byte[1024*64];

        private CPUCounter mCpuCounter;

        private System.Diagnostics.Process mProcess;

        private System.Threading.Timer mTimer;

        public Log4GridAppender()
        {
            mProcess = System.Diagnostics.Process.GetCurrentProcess();
            mCpuCounter = new CPUCounter(mProcess.ProcessName);
            System.Threading.ThreadPool.QueueUserWorkItem(OnSend);
            mTimer = new System.Threading.Timer(OnStat, null, 1000, 1000);
        }

        private System.Net.IPEndPoint mServerPoint;

        private System.Net.Sockets.Socket[] mSockets;

        private long mIndex;

        public System.Net.Sockets.Socket Client
        {
            get
            {
                if (mSockets == null)
                {
                   
                    mServerPoint = new IPEndPoint(IPAddress.Parse(Host), Port);
                    mSockets = new Socket[10];
                    for (int i = 0; i < 10; i++)
                    {
                        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
                        this.mSockets[i] = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        this.mSockets[i].Bind(ipep);
                    }
                }
                long index = System.Threading.Interlocked.Increment(ref mIndex);
                return mSockets[index % mSockets.Length];
            }
        }

        public string Host
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string ServerName
        {
            get;
            set;
        }

        public string AppName
        {
            get;
            set;
        }

        private Queue<object> mLogQueue = new Queue<object>(1024 * 5);

        private void Push(object log)
        {
            lock (this)
            {
                mLogQueue.Enqueue(log);
            }
        }

        private object Pop()
        {
            lock (this)
            {
                if (mLogQueue.Count > 0)
                    return mLogQueue.Dequeue();
                return null;
            }
        }

        private void OnSend(object state)
        {
            while (true)
            {
                try
                {
                    object message = Pop();
                    if (message != null)
                    {
                        ArraySegment<byte> data = Models.ProtobufPacket.Serialize(message, mBuffer);
                        int start = data.Offset;
                        int sends = data.Count;
                        while (sends > 0)
                        {
                            int count = Client.SendTo(data.Array, start, sends, SocketFlags.None, mServerPoint);
                            start += count;
                            sends -= count;
                            
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }

                catch (Exception e_)
                {
                    LogLog.Error(typeof(Log4GridAppender), e_.Message);
                }
            }
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            try
            {

                Models.LogModel log = new Models.LogModel();
                log.App = this.AppName;
                log.Host = ServerName;
                log.Content = loggingEvent.RenderedMessage;
                log.CreateTime = DateTime.Now;
                if (loggingEvent.Level == log4net.Core.Level.Debug)
                {
                    log.Type = Models.LogType.Debug;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Info)
                {
                    log.Type = Models.LogType.Info;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Warn)
                {
                    log.Type = Models.LogType.Warn;

                }
                else if (loggingEvent.Level == log4net.Core.Level.Error)
                {
                    log.Type = Models.LogType.Error;
                }
                else if (loggingEvent.Level == log4net.Core.Level.Fatal)
                {
                    log.Type = Models.LogType.Fatal;
                }
                else
                {
                    log.Type = Models.LogType.Info;
                }

                Push(log);
            }
            catch (Exception e_)
            {
                LogLog.Error(typeof(Log4GridAppender), e_.Message);
            }
        }

        private void OnStat(object state)
        {
            Models.StatModel sm = new Models.StatModel();
            sm.MemoryUsage = string.Format("{0:0.00}MB", (double)mProcess.WorkingSet64 / 1024 / 1024);
            sm.CpuUsage = string.Format("{0:00}%", mCpuCounter.ProcessUsage(mProcess.Id));
            sm.App = AppName;
            sm.Host = ServerName;
            Push(sm);
        }
    }
    class CPUCounter : IDisposable
    {
        public CPUCounter(string processName)
        {
            mProcessName = System.IO.Path.GetFileNameWithoutExtension(processName);
            mFileName = processName;
            mTimer = new System.Threading.Timer(GetUsage, null, 1000, 1000);
        }

        private string mFileName;

        private System.Threading.Timer mTimer;

        private string mProcessName;

        private Dictionary<int, float> mProcessCpuUsage = new Dictionary<int, float>();

        private List<CounterItem> mCounters = new List<CounterItem>();

        private Dictionary<string, int> mProcessIDs = new Dictionary<string, int>();

        public float ProcessUsage(int pid)
        {
            float result = 0;
            mProcessCpuUsage.TryGetValue(pid, out result);
            return result;
        }

        private CounterItem OnCreateCounter(string processname)
        {
            CounterItem item = mCounters.Find(e => e.ProcessName == processname);
            if (item == null)
            {
                item = new CounterItem();
                item.ProcessName = processname;
                item.Counter = new PerformanceCounter("Process", "% Processor Time");
                item.Counter.InstanceName = processname;
                mCounters.Add(item);
            }
            item.Enabled = true;
            return item;
        }

        private void GetUsage(object state)
        {
            mTimer.Change(-1, -1);
            mProcessIDs.Clear();
            Process[] ps = Process.GetProcessesByName(mProcessName);
            List<CounterItem> disposeditems = new List<CounterItem>();
            if (ps.Length > 0)
            {
                mProcessIDs.Add(ps[0].ProcessName, ps[0].Id);
                OnCreateCounter(ps[0].ProcessName).PID = ps[0].Id;
                for (int i = 1; i < ps.Length; i++)
                {
                    mProcessIDs.Add(ps[i].ProcessName + "#" + i, ps[i].Id);
                    OnCreateCounter(ps[i].ProcessName + "#" + i).PID = ps[i].Id;
                }
            }
            foreach (CounterItem item in mCounters)
            {
                if (item.Enabled)
                {
                    try
                    {
                        mProcessCpuUsage[mProcessIDs[item.ProcessName]] = item.Counter.NextValue();
                    }
                    catch (Exception e_)
                    {
                        Console.WriteLine(e_.Message);
                    }
                    item.Enabled = false;
                }

            }
            mTimer.Change(1000, 1000);

        }

        class CounterItem
        {
            public string ProcessName { get; set; }

            public System.Diagnostics.PerformanceCounter Counter
            {
                get;
                set;
            }

            public bool Enabled
            {
                get;
                set;
            }
            public int PID
            {
                get;
                set;
            }
        }

        public void Dispose()
        {
            if (mTimer != null)
                mTimer.Dispose();
        }
    }
}
