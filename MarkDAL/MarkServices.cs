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
     public class MarkServices
    {
        public int updateMarkList(List<MarkClass>marks)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                foreach (MarkClass m in marks)
                {
                    SqlParameter pNo = new SqlParameter
                    {
                        ParameterName = "@sNo",
                        SqlDbType = SqlDbType.Char,
                        Value = m.sNo
                    };
                    SqlParameter pID = new SqlParameter
                    {
                        ParameterName = "@sID",
                        SqlDbType = SqlDbType.Int,
                        Value = m.sID
                    };
                    SqlParameter pMark = new SqlParameter
                    {
                        ParameterName = "@mMark",
                        SqlDbType = SqlDbType.Int,
                        Value = m.mMark
                    };
                    if (DB.executeCommand("update marks set mMark = @mMark where sNo =@sNo AND sID = @sID", pMark,pNo,pID) > 0)
                        ++count;
                }
                con.Close();

            }
            return count;
        }
        
        void  insertStudentList(DBHelper DB,SqlParameter pID)
        {
            SqlDataAdapter adapter = DB.getAdapter("select sNo from students where sNo not in (select sNo from marks where sID = @sID)",pID);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                SqlParameter pNO = new SqlParameter
                {
                    ParameterName = "@sNo",
                    SqlDbType = SqlDbType.Char,
                    Value = row["sNo"]
                };
                DB.executeCommand("insert into marks(sNO,sID,mMark) values (@sNo,@sID,0)",pNO,pID );
            }            
        }

        public DataTable getMarkDataTable(int sID)
        {
            DataTable dt = new DataTable("marks");
            using (SqlConnection con = new SqlConnection(DBHelper.conString))
            {
                DBHelper DB = new DBHelper(con);
                SqlParameter pID = new SqlParameter
                {
                    ParameterName = "@sID",
                    SqlDbType = SqlDbType.Int,
                    Value = sID
                };
                insertStudentList(DB, pID);
                String sql = "SELECT   st.sNo, st.sName, su.sSubject, m.mMark FROM students AS st INNER JOIN marks AS m ON st.sNo = m.sNo INNER JOIN subjects AS su ON m.sID = su.sID AND su.sID = @sID ORDER BY st.sNo, su.sSubject";
                SqlDataAdapter adapter = DB.getAdapter(sql, pID);
               
                adapter.Fill(dt);
                con.Close();
            }
            return dt;
        }   
    }
}
