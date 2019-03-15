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
using System.Windows.Shapes;

namespace TeamProject2
{
    /// <summary>
    /// StudentMenuWin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StudentMenuWin : Window
    {
        StudentLogin value = new StudentLogin();
        public string MyProperty { get; set; }

        public StudentLogin frm;


        public StudentMenuWin(StudentLogin frm)
        {
            InitializeComponent();
            this.frm = frm;
            makeCombo();
            LoadData();

        }
        string connStr = "server=localhost;user=root;database=managestudent;port=3306;password=181515;";
        MySqlConnection conn;
        public void LoadData()
        {

            string source = @"Data source = localhost; port=3306;Initial Catalog = managestudent; userId=root;password=181515";
            MySqlConnection con = new MySqlConnection(source);
            con.Open();
            string sqlSelectQuery = "SELECT * FROM studentinfo WHERE id='" + frm.TextBoxText + "'";
            MySqlCommand cmd = new MySqlCommand(sqlSelectQuery, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                cboxMajor.Text = dr["Major"].ToString();
                txtGrade.Text = dr["Grade"].ToString();
                txtName.Text = dr["Name"].ToString();
                txtBirth.Text = dr["Birth"].ToString();
                txtAddress.Text = dr["Address"].ToString();
                cboxState.Text = dr["state"].ToString();
                txtPass.Text = dr["Password"].ToString();
            }
            con.Close();

        }


        private void makeCombo()
        {
            string[] state = { "재학", "휴학", "재적", "졸업" };
            string[] major = { "컴퓨터과학과", "물류통계학과", "환경과학과", "경찰행정학과", "무용과", "유도학과", "태권도학과", "실용음악과" };
            
            foreach (string s in state)
            {
                cboxState.Items.Add(s);
            }
            cboxState.SelectedIndex = 0;
            foreach (string s in major)
            {
                cboxMajor.Items.Add(s);
            }
            cboxMajor.SelectedIndex = 0;

        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string Query = "update  studentinfo set password= '"+txtPass.Text+"' ,id='"
                    + txtID.Text + "',major='" + cboxMajor.Text + "',grade='" + txtGrade.Text + "',name='"
                    + txtName.Text + "',birth='" + txtBirth.Text + "',address='" + txtAddress.Text + "',state='"
                    + cboxState.Text + "'where id='" + txtID.Text + "'";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Update!");
            }
            catch
            {
                MessageBox.Show("수정 불가");
            }

        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("로그아웃 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff

            }
            else
            {
                //do yes stuff
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }
    }
}
