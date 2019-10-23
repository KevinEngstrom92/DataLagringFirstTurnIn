using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace DataLagringFörstaInlämning.Domain
{
    class SqlClass
    {
        static string connectionString = @"Data Source = DESKTOP-8BK555E;Initial Catalog = FIRSTTURNINADO; Integrated Security = True;";

       SqlConnection sqlconn = new SqlConnection(connectionString);

    }
}
