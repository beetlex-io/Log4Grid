using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Peanut;

namespace Log4Grid.MySQL
{
    public class MySqlDriver : DriverTemplate<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlParameter, MysqlBuilder>
    {
    }
}
