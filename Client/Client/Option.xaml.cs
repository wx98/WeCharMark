using System;
using System.Collections.Generic;
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
    /// Option.xaml 的交互逻辑
    /// </summary>
    public partial class Option : Window
    {
        public Option()
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


        private void bt_kc_Click(object sender, RoutedEventArgs e)
        {
            SubjectManage sub = new SubjectManage();
            sub.ShowDialog();
        }

        private void bt_xs_Click(object sender, RoutedEventArgs e)
        {
            StudentManage sub = new StudentManage();
            sub.ShowDialog();
        }

        private void bt_rs_Click(object sender, RoutedEventArgs e)
        {
            MarkManage mark = new MarkManage();
            mark.ShowDialog();
        }
    }
}
