using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkModel
{
    public class SubjectClass
    {
        public int    sID      { get; set; }
        public String sSubject { get; set; }
    }
    public class StudentClass 
    {
        public String sNo      { get; set; }
        public String sName    { get; set; }
        public String sPass    { get; set; }
    }
    public class MarkClass 
    {
        public String sNo      { get; set; }
        public int    sID      { get; set; }
        public int    mMark    { get; set; }
    }    

}
