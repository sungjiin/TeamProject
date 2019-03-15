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
    /// ManagerMenuWin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ManagerMenuWin : Window
    {
        public ManagerMenuWin()
        {

            InitializeComponent();
            LoadData();
            makeCombo();
        }
        string connStr = "server=localhost;user=root;database=managestudent;port=3306;password=181515;";
        MySqlConnection conn;
        MySqlDataAdapter adp;
        MySqlCommand cmd;
        DataSet ds;
        private void LoadData()
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            cmd = new MySqlCommand("SELECT * From StudentInfo", conn);
            adp = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            adp.Fill(ds, "Students");
            dataGridStudent.DataContext = ds;
            conn.Close();
        }

        private void makeCombo()
        {
            string[] state = { "재학", "휴학", "재적", "졸업" };
            string[] major = { "컴퓨터과학과", "물류통계학과", "환경과학과", "경찰행정학과", "무용과", "유도학과", "태권도학과", "실용음악과" };
            string[] grade = { "1학년", "2학년", "3학년", "4학년" };
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
            foreach (string s in grade)
            {
                cboxGrade.Items.Add(s);
            }
            cboxGrade.SelectedIndex = 0;

        }

        private void DataGridStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowSelected = dg.SelectedItem as DataRowView;
            if (rowSelected != null)
            {
                txtID.Text = rowSelected[0].ToString();
                cboxMajor.Text = rowSelected[1].ToString();
                cboxGrade.Text = rowSelected[2].ToString();
                txtName.Text = rowSelected[3].ToString();
                txtBirth.Text = rowSelected[4].ToString();
                txtAddress.Text = rowSelected[5].ToString();
                cboxState.Text = rowSelected[6].ToString();
            }
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            adp = new MySqlDataAdapter("select * from studentinfo where Name like'" + txtCheck.Text + "%'", conn);
            ds = new DataSet();
            adp.Fill(ds);
            dataGridStudent.ItemsSource = ds.Tables[0].DefaultView;
            if (txtCheck.Text == "")
            {
                adp.Fill(ds, "Students");
                dataGridStudent.DataContext = ds;
            }
            conn.Close();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string Query = "update  studentinfo set id='" + txtID.Text + "',major='" + cboxMajor.Text + "',grade='" + cboxGrade.Text + "',name='" + txtName.Text + "',birth='" + txtBirth.Text + "',address='" + txtAddress.Text + "',state='" + cboxState.Text + "'where id='" + txtID.Text + "'";
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("수정되었습니다!");
            }
            catch
            {
                MessageBox.Show("수정 불가");
            }
            finally
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                adp = new MySqlDataAdapter("select * from studentinfo where Name like'" + txtCheck.Text + "%'", conn);
                ds = new DataSet();
                adp.Fill(ds);
                dataGridStudent.ItemsSource = ds.Tables[0].DefaultView;
                if (txtCheck.Text == "")
                {
                    adp.Fill(ds, "Students");
                    dataGridStudent.DataContext = ds;
                }
                conn.Close();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window ad = new AddStuWin();
            ad.ShowDialog();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
