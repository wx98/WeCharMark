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
    /// SubjectManage.xaml 的交互逻辑
    /// </summary>
    public partial class SubjectManage : Window
    {
        WS.MarkServiceSoapClient client = new WS.MarkServiceSoapClient();
        DataTable dt;
        DataView dv;

        public SubjectManage()
        {
            InitializeComponent();
        }
        void showMsg(string s)
        {
            MessageBox.Show(s, "Information", MessageBoxButton.OK);
        }
        bool confirm(String s)
        {
            return (MessageBox.Show(s, "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes);
        }
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            String s = txtSubject.Text.Trim();
            if (s != "")
            {
                try
                {
                    int sID = client.addSubject(new WS.SubjectClass { sID = 0, sSubject = s });
                    if (sID > 0)
                    {
                        DataRow row = dt.NewRow();
                        row["sID"] = sID;
                        row["sSUbject"] = s;
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
                }
                catch (Exception exp) { showMsg(exp.Message); }
            }
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            String s = txtSubject.Text.Trim();
            int index = mGrid.SelectedIndex;
            if (s != "" && index > 0)
            {
                if (confirm("确定要更新?"))
                {
                    try
                    {
                        if (client.updateSubject(new WS.SubjectClass { sID = (int)dv[index]["sID"], sSubject = s }))
                        {
                            dv[index]["sSubject"] = s;
                            dt.AcceptChanges();
                        }
                    }
                    catch (Exception exp) { showMsg(exp.Message); }
                }
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = mGrid.SelectedIndex;
            if (index >= 0)
            {
                if (confirm("删除该课程？"))
                {
                    try
                    {
                        if (client.deleteSubject(new WS.SubjectClass { sID = (int)dv[index]["sID"], sSubject = dv[index]["sSubject"].ToString() }))
                        {
                            dv[index].Delete();
                            dt.AcceptChanges();
                        }
                    }
                    catch (Exception exp) { showMsg(exp.Message); }
                }
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                client = new WS.MarkServiceSoapClient();
                dt = client.getSubjectDataTable();
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
