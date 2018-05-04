using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkModel;

namespace MarkDAL
{
     public class StudentService
    {
        public bool addStudent(StudentClass s)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlParameter pNo = new SqlParameter
                {
                    ParameterName = "@sNo",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sNo
                };
                SqlParameter pName = new SqlParameter
                {
                    ParameterName = "@sName",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sName
                };
                SqlParameter pPass = new SqlParameter
                {
                    ParameterName = "@sPass",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sPass
                };
                try
                {
                    if (DB.executeCommand("insert into students (sNo,sName,sPass) values (@sNo,@sName,@sPass)", pNo, pName, pPass) > 0) flag = true;
                }
                catch { }
                con.Close();
            }
            return flag;
        }

        public bool deleteStudent(StudentClass s)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlParameter pNo = new SqlParameter
                {
                    ParameterName = "@sNo",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sNo
                };
                if (DB.executeCommand("delete from students where sNo = @sNo", pNo) > 0) 
                    flag = true;
                con.Close();
            }
            return flag;
        }

        public bool updateStudent(StudentClass s)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlParameter pNo = new SqlParameter
                {
                    ParameterName = "@sNo",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sNo
                };
                SqlParameter pName = new SqlParameter
                {
                    ParameterName = "@sName",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sName
                };
                SqlParameter pPass = new SqlParameter
                {
                    ParameterName = "@sPass",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sPass
                };
                if (DB.executeCommand("update students set sName = @sName ,sPass = @sPass where sNo =@sNo", pNo, pName, pPass) > 0)
                    flag = true;
                con.Close();
            }
            return flag;
        }

        public DataTable getStudentDataTable()
        {
            DataTable dt = new DataTable("Students");
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlDataAdapter adapter = DB.getAdapter("select * from students order by sNo");
                adapter.SelectCommand = DBHelper.cmd;
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
          
        public int get_login(StudentClass st)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlParameter pNo = new SqlParameter
                {
                    ParameterName = "@sNo",
                    SqlDbType = SqlDbType.Char,
                    Value = st.sNo
                };
                SqlParameter pPass = new SqlParameter
                {
                    ParameterName = "@sPass",
                    SqlDbType = SqlDbType.Char,
                    Value = st.sPass
                };
                if (DB.getScalar("select count(*) from students where sNo=@sNo and sPass=@sPass", pNo, pPass) > 0)
                {
                    con.Close();
                    return 1;
                }
                else
                {
                    con.Close();
                    return 0;
                }

            }

        }
    }
}
