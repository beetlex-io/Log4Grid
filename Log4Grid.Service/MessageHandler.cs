using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Service
{
    class MessageHandler:Beetle.Express.IServerHandler
    {
        private Config.InterfaceFactory mFactory = new Config.InterfaceFactory();

        private Queue<object> mMsgQueue = new Queue<object>(5000);

        private List<IKende.com.core.Dispatch> mDispatchs = new List<IKende.com.core.Dispatch>();

        long  mDispatchIndex=1;

        public MessageHandler(int threads)
        {
            for (int i = 0; i < threads; i++)
            {
                mDispatchs.Add(new IKende.com.core.Dispatch());
            }
        }

        private IKende.com.core.Dispatch GetDispatch()
        {
            long index = System.Threading.Interlocked.Increment(ref mDispatchIndex);
            return mDispatchs[(int)(index%mDispatchs.Count)];
        }

        class MessageTalk : IKende.com.core.Dispatch.ITack
        {

            public object Message
            {
                get;
                set;
            }

            public Config.InterfaceFactory Factory
            {
                get;
                set;
            }
            public void Execute()
            {
                try
                {
                    if (Message is Models.LogModel)
                    {
                        OnLog((Models.LogModel)Message);
                    }
                    else if (Message is Models.StatModel)
                    {
                        OnState((Models.StatModel)Message);
                    }
                    else
                    {
                        Utils.Log.ErrorFormat("can't process {0}", Message);
                    }

                }
                catch (Exception e_)
                {
                    Utils.Log.ErrorFormat("process {0} error {1}", Message,e_.Message);
                }
            }

            private void OnLog(Models.LogModel e)
            {
                Factory.Store.Add(e);
            }

            private void OnState(Models.StatModel e)
            {
                Factory.Management.Stat(e);
            }
        }  

        public void Connect(Beetle.Express.IServer server, Beetle.Express.ChannelConnectEventArgs e)
        {
            Utils.Log.InfoFormat("{0} connected", e.Channel.EndPoint);
        }

        public void Disposed(Beetle.Express.IServer server, Beetle.Express.ChannelEventArgs e)
        {
            Utils.Log.InfoFormat("{0} disposed", e.Channel.EndPoint);
        }

        public void Error(Beetle.Express.IServer server, Beetle.Express.ErrorEventArgs e)
        {
            Utils.Log.InfoFormat("{0} error {1}", e.Channel.EndPoint,e.Error.Message);
        }

        public void Opened(Beetle.Express.IServer server)
        {
           
        }

        public void Receive(Beetle.Express.IServer server, Beetle.Express.ChannelReceiveEventArgs e)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(e.Data.Array, e.Data.Offset, e.Data.Count);
            object message = Models.ProtobufPacket.Deserialize(buffer);
            MessageTalk mt = new MessageTalk();
            mt.Message = message;
            mt.Factory = mFactory;
            GetDispatch().Add(mt);
            
        }

        public void SendCompleted(Beetle.Express.IServer server, Beetle.Express.ChannelSendEventArgs e)
        {
            
        }
    }
}
