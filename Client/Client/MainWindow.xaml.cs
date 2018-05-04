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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        WS.MarkServiceSoapClient client = new WS.MarkServiceSoapClient();
        static int k = 3;

        public MainWindow()
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

        private void bt_login_Click(object sender, RoutedEventArgs e)
        {
            String str_yh = this.txt_yhm.Text.Trim();
            String str_mm = this.txt_mm. Password.Trim();
            if (!(string.IsNullOrEmpty(str_yh) || string.IsNullOrEmpty(str_mm)))
            {
                try
                {
                    int sid = client.get_login(new WS.StudentClass { sNo = str_yh, sPass = str_mm });
                    if (sid > 0)
                    {
                        Option option = new Option();
                        option.Show();
                        this.Close();
                    }
                    else
                    {
                       --k;
                       
                        if (k == 0)
                        {
                            this.Close();
                        }
                        else
                        {
                            showMsg("非法用户！剩余" + k.ToString() + "次登录机会");
                        }
                    }

                }
                catch (Exception exp)
                {
                    showMsg(exp.Message);
                }

            }
            else
            {
                showMsg("用户名或密码不能为空！");
            }
         
        }
    }
}
