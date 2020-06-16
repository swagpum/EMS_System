using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMS201724112145
{
    public class SQLHelper
    {
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>返回一个数据库链接</returns>
        public static SqlConnection Connect()
        {
            string sqlconn = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\EMS.mdf'; ";
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = sqlconn;

            //打开数据库连接
            if(cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            return cn;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="sqlConnection">无返回值</param>
        public static void Close(SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }
    }
}