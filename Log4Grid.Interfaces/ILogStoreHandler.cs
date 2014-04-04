using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Interfaces
{
    public interface ILogStoreHandler
    {
        void Add(Models.LogModel e);
        void Clean(string app,string host);
        void Backup(string app,string host);
    }
}
