using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Log4Grid.Service;
namespace Log4Grid.Service.WinService
{
    public partial class Log4GridService : ServiceBase
    {
        public Log4GridService()
        {
            InitializeComponent();
        }


        private Log4Grid.Service.LogServer mLogServer;

        protected override void OnStart(string[] args)
        {
            try
            {
                Utils.Log.Info("Log4Grid Server Copyright @ www.ikende.com 2014 Version " + typeof(Log4GridService).Assembly.GetName().Version);
                Utils.Log.Info("Website:http://www.ikende.com");
                Utils.Log.Info("Email:henryfan@msn.com");
                mLogServer = new LogServer();
                mLogServer.Open();
                Utils.Log.InfoFormat("Log4Grid windows start at {0}", DateTime.Now);
            }
            catch (Exception e_)
            {
                Utils.Log.ErrorFormat("Log4Grid windows start error {0}", e_.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
               
             
                try
                {
                    if (mLogServer != null)
                        mLogServer.Dispose();
                }
                catch (Exception e)
                {
                    Utils.Log.ErrorFormat("Log4Grid server stop error {0}", e.Message);
                }


                Utils.Log.InfoFormat("Log4Grid windows service stop at {0}", DateTime.Now);
            }
            catch (Exception e_)
            {
                Utils.Log.ErrorFormat("Log4Grid windows service stop error {0}", e_.Message);
            }
        }
    }
}
