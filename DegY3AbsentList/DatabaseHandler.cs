using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BBAAbsentList
{
    class DatabaseHandler
    {
        public static SqlConnection getSqlConnection()
        {
            // SqlConnection conn = new SqlConnection();
            // cn.ConnectionString = @"Data Source=ASANU;Initial Catalog=HonsP1; Integrated Security = True";
            // cn.ConnectionString = @"User id=solver;Password=`1qazxsw23edc;";
            // cn.ConnectionString = @"Data Source=192.168.0.36;User id=solver;Password=`1qazxsw23edc;";
            // conn.ConnectionString = @"Data Source=BBA-PC\SQLEXPRESS;Initial Catalog=bba_sem1_2015;User id=sa;Password=abc123";


            SqlConnection myConnection = new SqlConnection("user id = sa; password = abc123; server = BBA-PROF;"
                                                + "Trusted_Connection = yes; database = degrst18y3;"
                                                + "MultipleActiveResultSets=true; connection timeout = 30");
            /*
            SqlConnection myConnection = new SqlConnection("user id = sa; password = abc123; server = DESKTOP-ONDRO0C\\SQLEXPRESS;"
                                                + "Trusted_Connection = yes; database = degrst18y3;"
                                                + "MultipleActiveResultSets=true; connection timeout = 30");
            */
            myConnection.Open();

            return myConnection;
        }
    }
}