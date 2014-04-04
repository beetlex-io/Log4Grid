using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Interfaces
{
    public interface ILogSearchHandler
    {
        IList<Models.LogModel> List(string app, string host, Models.LogType? type, DateTime? from, DateTime? to, int pageindex, out int pages);
    }
}
