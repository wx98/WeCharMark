using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkModel;
using System.Data;
using MarkDAL;
namespace MarkBLL
{
    public class SubjectManager
    {
        public DataTable getSubjectDataTable()
        {
            SubjectService get = new SubjectService();
            get.getSubjectDataTable();
            return get.getSubjectDataTable();
        }
        public bool updateSubject(SubjectClass s)
        {
            SubjectService update = new SubjectService();
            return update.updateSubject(s);
        }
        public int addSubjec(ref SubjectClass s)
        {
       
            SubjectService add = new SubjectService();
            return add.addSubject(ref s);
        }
        public Boolean deleteSubject(ref SubjectClass s)
        {
            SubjectService delete = new SubjectService();
            return delete.deleteSubject(s);
        }
    }
}
