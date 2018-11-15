using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MarkModel;
using MarkDAL;
/// <summary>
/// MarkService 的摘要说明
/// </summary>

using System.Data;[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class MarkService : System.Web.Services.WebService {

    public MarkService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable getSubjectDataTable()
    {
        SubjectService manager = new SubjectService();
        return manager.getSubjectDataTable();
    }

    [WebMethod]
    public bool updateSubject(SubjectClass s)
    {
        SubjectService manager = new SubjectService();
        return manager.updateSubject(s);
    }
    
    [WebMethod]
    public int addSubject(SubjectClass s)
    {
        SubjectService manager = new SubjectService();
        return manager.addSubject(ref s);
    }
    
    [WebMethod]
    public bool deleteSubject(SubjectClass s)
    {
        SubjectService manager = new SubjectService();
        return manager.deleteSubject(s);
    }
   ///////////////////////////////////////////
    [WebMethod]
    public DataTable getStudentDataTable()
    {
        StudentService manager = new StudentService();
        return manager.getStudentDataTable();
    }

    [WebMethod]
    public bool updateStudent(StudentClass s)
    {
        StudentService manager = new StudentService();
        return manager.updateStudent(s);
    }

    [WebMethod]
    public bool addStudent(StudentClass s)
    {
        StudentService manager = new StudentService();
        return manager.addStudent(s);
    }

    [WebMethod]
    public bool deleteStudent(StudentClass s)
    {
        StudentService manager = new StudentService();
        return manager.deleteStudent (s);
    }
    [WebMethod]
    public int get_login(StudentClass s)
    {
        StudentService manager = new StudentService();
        return manager.get_login(s);
    }
    ////////////////////////////////////////////
    [WebMethod]
    public DataTable getMarkDataTable(int sID)
    {
        
        MarkServices manager = new MarkServices();
        return manager.getMarkDataTable(sID);
    }
    [WebMethod]
    public int updateMarkList(List<MarkClass> marks)
    {
        MarkServices manager = new MarkServices();
        return manager.updateMarkList(marks);
    }
}