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
    /// AddStu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddStu : Page
    {
        string connStr = "server=localhost;user=root;database=managestudent;port=3306;password=181515;";
        MySqlConnection conn;
        MySqlCommand cmd;
        
        public AddStu()
        {
            InitializeComponent();
            MakeCombo();
            MakeMajor();
        }

        public void MakeCombo()
        {
            int[] year = new int[1000];
            int cnt = 0;
            int ycnt = 0;
            for (int i = 1980; i <= DateTime.Now.Year; i++)
            {
                year[cnt] = i;
                cnt++;
            }
            foreach (int i in year)
            {
                if (i == 0)
                    break;
                cboxYear.Items.Add(i);
                ycnt++;
            }
            cboxYear.SelectedIndex = ycnt - 1;
        }

        private void MakeMajor()
        {
            string[] major = { "컴퓨터과학과", "물류통계학과", "환경과학과", "경찰행정학과", "무용과", "유도학과", "태권도학과", "실용음악과" };

            foreach (string s in major)
            {
                cboxMajor.Items.Add(s);
            }
            cboxMajor.SelectedIndex = 0;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            conn = new MySqlConnection(connStr);
            string sql = "insert into studentinfo (id,major,name,birth,password, year) values('" + txbId.Text + "','" + cboxMajor.Text + "','" + txtName.Text + "','" + txtBirth.Text + "', '" + txtBirth.Text +"', '"+cboxYear.Text+"')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("저장되었습니다!");
        
        }
        

        private int AutoMake(string major, string year)
        {
            int num = Convert.ToInt32(year) * 10000;
            
            // string[] major = { "컴퓨터과학과", "물류통계학과", "환경과학과", "경찰행정학과", "무용과", "유도학과", "태권도학과", "실용음악과" };

            if (major == "컴퓨터과학과")
            {
                return num;
            }
            else if (major == "물류통계학과")
            {
                num += 1000;
            }
            else if (major == "환경과학과")
            {
                num += 2000;
            }
            else if (major == "경찰행정학과")
            {
                num += 3000;
            }
            else if (major =="무용과")
            {
                num += 4000;
            }
            else if (major == "유도학과")
            {
                num += 5000;
            }
            else if (major == "태권도학과")
            {
                num += 6000;
            }
            else if (cboxMajor.Text == "실용음학과")
            {
                num += 7000;
            }
            return num;
        }

        private void BtnAutoMake_Click_1(object sender, RoutedEventArgs e)
        {
            int cnt = 0;
            conn = new MySqlConnection(connStr);
            string sql = "select Count(*) AS cnt from Studentinfo where major =  '" +cboxMajor.Text+"' And  Year = '"+ cboxYear.Text +"'";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cnt = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            conn.Close();
            txbId.Text = (AutoMake(cboxMajor.Text, cboxYear.Text) + cnt).ToString();
            
        }
        
    }
}
