using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Interfaces
{
    public interface IAppManagement
    {
        void Stat(Models.StatModel e);
        IList<Models.ApplicationData> ListApp();
        void CleanApp();
      
    }
}
