using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MarkModel;


namespace MarkDAL
{
    public class SubjectService 
    {
        public int addSubject(ref SubjectClass s){
            int ID = 0;
            using (SqlConnection con = new SqlConnection(DBHelper.conString)) {
                DBHelper DB = new DBHelper(con);
                SqlParameter pSubject = new SqlParameter
                {
                    ParameterName = "@sSubject",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sSubject
                };
                if (DB.executeCommand("insert into subjects(sSubject) values (@sSubject)", pSubject) > 0)
                {
                    ID = DB.getScalar("select top 1 sID from subjects order by sID desc");
                    s.sID = ID;
                }
                con.Close();
            }
           return ID;
        }

        public bool deleteSubject(SubjectClass s)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(DBHelper.conString)) { 
                DBHelper DB = new DBHelper(con);
                SqlParameter pID = new SqlParameter
                {
                    ParameterName = "@sID",
                    SqlDbType = SqlDbType.Int,
                    Value = s.sID
                };
                if (DB.executeCommand("delete from subjects where sID = @sID", pID) > 0)
                    flag = true;
                con.Close();
            }
            return flag;
        }
        public bool updateSubject(SubjectClass s)
        {
            bool flag = false;
            using (SqlConnection con = new SqlConnection(DBHelper.conString)) { 
                DBHelper DB = new DBHelper(con);
                SqlParameter pID = new SqlParameter
                {
                    ParameterName = "@sID",
                    SqlDbType = SqlDbType.Int,
                    Value = s.sID
                };
                SqlParameter pSubject = new SqlParameter
                {
                    ParameterName = "@sSubject",
                    SqlDbType = SqlDbType.Char,
                    Value = s.sSubject
                };
                if (DB.executeCommand("update subjects set sSubject = @sSubject where sID =@sID", pSubject,pID) > 0) flag = true;
                con.Close();
            };
            return flag;
        }
        public DataTable getSubjectDataTable() 
        {
            DataTable dt = new DataTable("Subjects");
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlDataAdapter adapter = DB.getAdapter("select * from subjects order by sSubject");
                adapter.SelectCommand = DBHelper.cmd;
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }
    }

}

