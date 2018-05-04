using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// MarkManage.xaml 的交互逻辑
    /// </summary>
    public partial class MarkManage : Window
    {
        WS.MarkServiceSoapClient client = new WS.MarkServiceSoapClient();
        DataTable dt;
        DataView sdv, dv;
        public MarkManage()
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
        private void cbSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSubject.SelectedIndex >= 0)
            {
                int sID = (int)sdv[cbSubject.SelectedIndex]["sID"];
                try
                {
                    dt = client.getMarkDataTable(sID);
                    dv = dt.DefaultView;
                    mGrid.ItemsSource = dv;
                }
                catch (Exception exp) { showMsg(exp.Message); }
            }
            else mGrid.ItemsSource = null;
        }

        private void mGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mGrid.BeginEdit();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cbSubject.SelectedIndex >= 0)
            {
                try
                {
                    int sID = (int)sdv[cbSubject.SelectedIndex]["sID"];
                    List<WS.MarkClass> marks = new List<WS.MarkClass>();
                    foreach (DataRowView row in dv)
                    {
                        WS.MarkClass m = new WS.MarkClass { sNo = row["sNo"].ToString(), sID = sID, mMark = (int)row["mMark"] };
                        marks.Add(m);
                    }
                    int count = client.updateMarkList(marks.ToArray());
                    showMsg("更新" + count.ToString() + "条记录!");
                }
                catch (Exception exp) { showMsg(exp.Message); }
            }
        }

        private void txtMark_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //成绩输入框值允许输入0~9的数字与退格
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Back) e.Handled = false;
            else e.Handled = true;
        }

        private void txtMark_TextChanged(object sender, TextChangedEventArgs e)
        {
            //输入值变化后,判断成绩是否有效,如果无效则会要求重新输入
            TextBox txt = (TextBox)sender;
            String s = txt.Text;
            int m = 0;
            int.TryParse(s, out m);
            if (m < 0 || m > 100)
            {
                showMsg("成绩值无效!");
                if (s.Length == 3) txt.Text = s.Substring(0, 2);
                txt.Focus();
            }
        }

        private void mainWimdow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new WS.MarkServiceSoapClient();
                sdv = client.getSubjectDataTable().DefaultView;
                foreach (DataRowView row in sdv)
                {
                    cbSubject.Items.Add(row["sSubject"].ToString());
                }
            }
            catch (Exception exp) { showMsg(exp.Message); }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbSubject.SelectedIndex >= 0)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "csv|*.csv";
                if ((bool)dlg.ShowDialog())
                {
                    int sID = (int)sdv[cbSubject.SelectedIndex]["sID"];
                    List<WS.MarkClass> marks = new List<WS.MarkClass>();
                    StreamReader sr = new StreamReader(dlg.FileName, Encoding.Default);
                    String s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        String[] st = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (st.Length == 3)
                        {
                            int m = 0;
                            int.TryParse(st[2], out m);
                            if (m < 0 || m > 100) m = 0;
                            marks.Add(new WS.MarkClass { sNo = st[0], sID = sID, mMark = m });
                        }
                    }
                    sr.Close();
                    try
                    {
                        client.updateMarkList(marks.ToArray());
                        dt = client.getMarkDataTable(sID);
                        dv = dt.DefaultView;
                        mGrid.ItemsSource = dv;
                    }
                    catch (Exception exp)
                    { showMsg(exp.Message); }
                }
            }
        }
    }
}
