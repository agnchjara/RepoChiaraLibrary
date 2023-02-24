using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.EntitiesDB
{
    public class DBConnectionProva
    {
        public static string ConnectionString
        {
            get
            {
                string connStr = ConfigurationManager.ConnectionStrings["AWConnection"].ConnectionString;
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connStr);
                sb.ApplicationName = ApplicationName ?? sb.ApplicationName;
                sb.ConnectTimeout = (ConnectionTimeout > 0) ? ConnectionTimeout : sb.ConnectTimeout;
                return sb.ToString();
            }

        }
        /// <summary>
        /// Returns an open connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        /// <summary>
        /// Overrides the connection timeout
        /// </summary>
        public static int ConnectionTimeout { get; set; }
        /// <summary>
        /// Property used to override the name of the application
        /// </summary>
        public static string ApplicationName
        {
            get;
            set;
        }
    }
}

