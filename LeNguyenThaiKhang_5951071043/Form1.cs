using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LeNguyenThaiKhang_5951071043
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }
        private void GetStudentRecord()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G0PNN12;Initial Catalog=DemoCRUD;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("select* from StudentsTb1", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;        }
        private bool IsVaLidData()
        {
            if (txtNName.Text==string.Empty
                ||TxtHName.Text==string.Empty
                ||TxtAdress.Text==string.Empty
                ||string.IsNullOrEmpty(Txtphone.Text)
                ||string.IsNullOrEmpty(Txtroll.Text))
            {
                MessageBox.Show("CÓ CHỔ CHƯA NHẬP DỮ LIỆU!!!", "LỖI DỮ LIỆU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void add_Click(object sender, EventArgs e)
        {if (IsVaLidData())
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G0PNN12;Initial Catalog=DemoCRUD;Integrated Security=True");
                
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb1(Name,FatherName,RollNumber,Address,Mobile) VALUES (@Name,@FatherName,@RollNumber,@Address,@Mobile)", con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", Txtroll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", Txtphone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }

        }
        public int StudentID;

        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.StudentRecordData.Rows[e.RowIndex];
            StudentID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
            TxtHName.Text = row.Cells[1].Value.ToString();
            txtNName.Text = row.Cells[2].Value.ToString();
            Txtroll.Text = row.Cells[3].Value.ToString();
            TxtAdress.Text = row.Cells[4].Value.ToString();
            Txtphone.Text = row.Cells[5].Value.ToString();
            ID.Text = row.Cells[0].Value.ToString();
        }

        private void update_Click(object sender, EventArgs e)
        {
            
            if (StudentID > 0 )
            {
                
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G0PNN12;Initial Catalog=DemoCRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb1 SET Name=@Name,FatherName=@FatherName,RollNumber=@RollNumber,Address=@Address,Mobile=@Mobile WHERE StudentID=@ID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", Txtroll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", Txtphone.Text);
                cmd.Parameters.AddWithValue("@ID", ID.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                
            }
            else
            {
                MessageBox.Show("cập nhật bị lỗi!!!", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G0PNN12;Initial Catalog=DemoCRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(" delete from StudentsTb1 where StudentID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.ID.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();

            }
            else
            {
                MessageBox.Show("cập nhật bị lỗi", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
