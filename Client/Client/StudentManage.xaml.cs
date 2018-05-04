using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// StudentManage.xaml 的交互逻辑
    /// </summary>
    public partial class StudentManage : Window
    {
        WS.MarkServiceSoapClient client = new WS.MarkServiceSoapClient();
        DataTable dt;
        DataView dv;
        public StudentManage()
        {
            InitializeComponent();
        }
        void showMsg(String s)
        {
            MessageBox.Show(s, "Information", MessageBoxButton.OK);
        }
        bool confirm(String s)
        {
            return (MessageBox.Show(s, "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            String sNo = txtNo.Text.Trim(),
                   sName = txtName.Text.Trim(),
                   sPass = txtPass.Password.Trim();
            if (sNo != "" && sName != "")
            {
                DataRow row = dt.AsEnumerable().FirstOrDefault(x => x["sNo"].ToString() == sNo);
                if (row != null)
                {
                    showMsg(sNo + "已存在！"); return;
                }
                if (sPass != "") sPass = sNo;
                try
                {
                    if (client.addStudent(new WS.StudentClass { sNo = sNo,sName = sName ,sPass  =sPass}))
                    {
                        row = dt.NewRow();
                        row["sNo"] = sNo;
                        row["sName"] = sName;
                        row["sPass"] = sPass;
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                }
                catch (Exception exp)
                {
                    showMsg(exp.Message);
                }
            }
        }
        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            String sName = txtName.Text.Trim(),
                   sPass = txtName.Text.Trim();
            int index = mGrid.SelectedIndex;
            if (sName != "" && index >= 0)
            {
                String sNo = dv[index]["sNo"].ToString();
                if (sPass == "") sPass = sNo;
                try {
                    if (client.updateStudent(new WS.StudentClass { sNo = sNo, sName = sName, sPass = sPass }))
                    {
                        dv[index]["sName"] = sName;
                        dv[index]["sPass"] = sPass;
                        dt.AcceptChanges();
                    }
                }
                catch (Exception exp)
                {
                    showMsg(exp.Message);
                }
            }
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = mGrid.SelectedIndex;
            if (index >= 0)
            {
                if (confirm("删除该学生？？"))
                {
                    try
                    {
                        if (client.deleteStudent(new WS.StudentClass { sNo = dv[index]["sNo"].ToString(), sName = dv[index]["sName"].ToString(), sPass = dv[index]["sPass"].ToString() }))
                        {
                            dv[index].Delete();
                            dt.AcceptChanges();
                        }
                    }
                    catch (Exception exp) { showMsg(exp.Message); }
                }
            }
        }
        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new WS.MarkServiceSoapClient();
                dt = client.getStudentDataTable();
                dv = dt.DefaultView;
                mGrid.ItemsSource = dv;
            }
            catch (Exception exp)
            {
                showMsg(exp.Message);
            }
        }

        
    }
}
