using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotizBuch.SQL.Main
{
    public class SQLConstants
    {

        // Benutzt Windows Login als Authentifikation
        public static string ServerConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Notes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    }
}
