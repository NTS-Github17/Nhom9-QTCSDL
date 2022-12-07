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
using System.Management;
using System.Web.UI.Design;

namespace Nhom9_QuanLyBanHang
{
    public partial class frmKhachHang : Form
    {
        string sCon = "Data Source=127.0.0.1;Initial Catalog=QuanLyNhaHang;Persist Security Info=True;User ID=sqlserver;Password=QT@2022CSDL";

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DB");
            }

            string sQuery = "select * from KhachHang";
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
            DataSet ds = new DataSet(); 
            adapter.Fill(ds, "KhachHang");
            guna2DataGridView1.DataSource = ds.Tables["KhachHang"];

            con.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DB");
            }

            //Kiem tra tinh hop le cua du lieu ...
            string sMaKH = txt_MaKH.Text;
            string sTenKH = txt_TenKH.Text;
            string sSoDT = txt_SoDienThoai.Text;
            string sDiaChi = txt_DiaChiKH.Text;
            string sMaSoThue = txt_MaSoThue.Text;
            int iLoaiKH = 0;
            if (rbCaNhan.Checked == true)
            {
                iLoaiKH = 0;
            }
            else
            {
                iLoaiKH = 1;
            }

            string sQuery = "insert into KhachHang values(@makh, @tenkh, @sodt, @diachi, @loaikh, @masothue)";
            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@makh", sMaKH);
            cmd.Parameters.AddWithValue("@tenkh", sTenKH);
            cmd.Parameters.AddWithValue("@sodt", sSoDT);
            cmd.Parameters.AddWithValue("@diachi", sDiaChi);
            cmd.Parameters.AddWithValue("@loaikh", iLoaiKH);
            cmd.Parameters.AddWithValue("@masothue", sMaSoThue);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm mới thành công.");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xảy ra lỗi trong quá trình thêm mới.");
            }

            con.Close();    

        }

        private void txt_SoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_MaKH.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["MaKH"].Value.ToString();
            txt_TenKH.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["TenKH"].Value.ToString();
            txt_SoDienThoai.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["SoDT"].Value.ToString();
            txt_DiaChiKH.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["DiaChiKH"].Value.ToString();
            txt_MaSoThue.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["MaSoThue"].Value.ToString();
            int iLoaiKH = Convert.ToInt16(guna2DataGridView1.Rows[e.RowIndex].Cells["LoaiKH"].Value);
            if (iLoaiKH == 0)
            {
                rbCaNhan.Checked = true;
            }
            else
            {
                rbDoanhNghiep.Checked = true;
            }

            txt_MaKH.Enabled= false;
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DB");
            }

            string sMaKH = txt_MaKH.Text;
            string sTenKH = txt_TenKH.Text;
            string sSoDT = txt_SoDienThoai.Text;
            string sDiaChi = txt_DiaChiKH.Text;
            string sMaSoThue = txt_MaSoThue.Text;
            int iLoaiKH = 0;
            if (rbCaNhan.Checked == true)
            {
                iLoaiKH = 0;
            }
            else
            {
                iLoaiKH = 1;
            }

            string sQuery = "update KhachHang set TenKH = @tenkh, SoDT = @sodt, DiaChiKH = @diachi, " +
                            "LoaiKH = @loaikh, MaSoThue = @masothue" +
                            "where MaKH = @makh";
            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@makh", sMaKH);
            cmd.Parameters.AddWithValue("@tenkh", sTenKH);
            cmd.Parameters.AddWithValue("@sodt", sSoDT);
            cmd.Parameters.AddWithValue("@diachi", sDiaChi);
            cmd.Parameters.AddWithValue("@loaikh", iLoaiKH);
            cmd.Parameters.AddWithValue("@masothue", sMaSoThue);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công.");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xảy ra lỗi trong quá trình cập nhật!");
            }

            con.Close();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi kết nối DB.");
            }

            string sMaKH = txt_MaKH.Text;
            string sQuery = "delete KhachHang where MaKH = @makh";
            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@makh", sMaKH);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xảy ra lỗi trong quá trình xóa!");
            }

            con.Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
