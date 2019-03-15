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
    /// StudentLogin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StudentLogin : Page
    {
        public string MyID { get; set; }
        public StudentLogin()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("Data source=localhost;port=3306;Initial Catalog=managestudent;userId=root;password=181515");
        int i;
        public string TextBoxText { get { return txtUser.Text; } set { txtUser.Text = value; } }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from studentinfo where id='" + txtUser.Text + "'and password='" + txtPass.Password + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 1)
            {
                MyID = txtUser.Text;
                StudentMenuWin ek = new StudentMenuWin(this);
                ek.MyProperty += MyID;
                ek.Show();
                MainWindow mw = Application.Current.MainWindow as MainWindow;
                mw.Close();
            }
            else
            {
                MessageBox.Show("다시 입력");
            }

        }
    }
}
