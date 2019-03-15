using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeamProject2
{
    /// <summary>
    /// ManagerLogin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ManagerLogin : Page
    {
        public ManagerLogin()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("Data source=localhost;port=3306;Initial Catalog=managestudent;userId=root;password=181515");
        int i;
        public void Login()
        {
            
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from ManageLogin where MID='" + txtUser.Text + "'and MPassword='" + txtPass.Password + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 1)
            {
                ManagerMenuWin mmw = new ManagerMenuWin();
                mmw.Show();
                MainWindow mw = Application.Current.MainWindow as MainWindow;
                mw.Close();
            }
            else
                MessageBox.Show("다시 입력하세요.");

        }
    }
}
