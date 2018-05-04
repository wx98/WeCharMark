using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarkDAL
{
    class DBHelper
    {
        /*数据库IP,用户名密码
          Database connection character*/
        public static String Data_IP = "192.168.1.102";
        public static String Data_UserName = "sa";
        public static String Data_UserPass = "123456";
        public static String conString = "Database=WeCharMark_DataBase;Server=" + Data_IP + ";User ID=" + Data_UserName + ";Password=" + Data_UserPass + ";";
        
        private SqlConnection con;
        public static SqlCommand cmd;
        public DBHelper(SqlConnection con)
        {
            con.Open();
            this.con = con;
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public int executeCommand(String sql)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            return cmd.ExecuteNonQuery();
        }
        public int executeCommand(String sql,params SqlParameter[] values)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }
        public SqlDataReader getReader(String sql) 
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public SqlDataReader getReader(String sql, params SqlParameter[] values)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public SqlDataAdapter getAdapter(String sql)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter();
            return adapter;
        }
        public SqlDataAdapter getAdapter(String sql, params SqlParameter[] values)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            return adapter;
        }
        public int getScalar(String sql)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            return (int)cmd.ExecuteScalar();
        }
        public int getScalar(String sql, params SqlParameter[] values)
        {
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            return (int)cmd.ExecuteScalar();
        }
    }
}
