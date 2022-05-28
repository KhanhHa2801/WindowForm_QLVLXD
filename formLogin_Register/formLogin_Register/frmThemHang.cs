using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace formLogin_Register
{
    public partial class frmThemHang : Form
    {

        DataAccess dtBase = new DataAccess();

        public static string x = "";

        public frmThemHang()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle rc = ClientRectangle;
            if (rc.IsEmpty)
                return;
            if (rc.Width == 0 || rc.Height == 0)
                return;
            using (LinearGradientBrush brush = new LinearGradientBrush(rc, Color.White, Color.FromArgb(196, 232, 250), 90F))
            {
                e.Graphics.FillRectangle(brush, rc);
            }
        }
        private void txtNgayTao_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void frmThemHang_Load(object sender, EventArgs e)
        {
            DateTime d = DateTime.Today;
            txtNgayTao.Text = d.ToString(" dd / MM / yyyy");

            cbxTenNhomHang.Items.Add("Đồ bảo hộ lao động");
            cbxTenNhomHang.Items.Add("Thiết bị phòng tắm");
            cbxTenNhomHang.Items.Add("Vật tư cầu đường");
            cbxTenNhomHang.Items.Add("Vật liệu xây dựng");

            cbxTenNhomHang.SelectedItem = "";

            cbxTenDVT.Items.Add("Bao");
            cbxTenDVT.Items.Add("Cái");
            cbxTenDVT.Items.Add("Đôi");
            cbxTenDVT.Items.Add("Kg");
            cbxTenDVT.Items.Add("Tấm");
            cbxTenDVT.Items.Add("Tấn");
            cbxTenDVT.Items.Add("Viên");
            cbxTenDVT.Items.Add("Yến");

            cbxTenDVT.SelectedItem = "";

            btnRefesh.Enabled = false;
        }

        private void cbxTenNhomHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmThemHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (MessageBox.Show("BẠN có muốn thoát? ","Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            //{
            //    this.Close();
            //}
            //else
            //{
            //    //frmLogin form1 = new frmLogin();
            //    //form1.ShowDialog();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbxTenNhomHang.Text = "";
            cbxTenDVT.Text = "";
            txtMoTa.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
        }

        private void txtMaHang_TextChanged(object sender, EventArgs e)
        {
            if (txtMaHang.Text != "")
            {
                btnRefesh.Enabled = true;
                btnThem.Enabled = true;
            }
            else
            {
                btnRefesh.Enabled = false;
                btnThem.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text == "" || txtTenHang.Text == "" || cbxTenNhomHang.Text == "" || cbxTenDVT.Text == "" || txtGiaNhap.Text == "" || txtGiaBan.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đủ dữ liệu");
            }
            else
            {
                DataTable temp = dtBase.DataReader("select * from tblHang where MaHang = '" + txtMaHang.Text + "'");

                if (temp.Rows.Count > 0)
                {
                    MessageBox.Show("Mã hàng đã tồn tại");
                    txtMaHang.Focus();
                }
                else
                {
                    if(float.Parse(txtGiaBan.Text) < float.Parse(txtGiaNhap.Text))
                    {
                        if(MessageBox.Show("Giá bán hiện đang nhỏ hơn giá nhập. Bạn chắc chứ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            txtGiaBan.Focus();
                        }
                        else
                        {
                            DataTable t = dtBase.DataReader("select MaNhomHang from tblNhomHang where TenNhomHang = N'" + cbxTenNhomHang.Text + "'");
                            dtBase.UpdateData("insert into tblHang(MaHang, TenHang, GiaNhap, GiaBan, MaNhomHang, DonViTinh, MoTa) values('" + txtMaHang.Text + "', '" + txtTenHang.Text + "', '" + txtGiaNhap.Text + "', '" + txtGiaBan.Text + "', '" + t.Rows[0].ItemArray[0].ToString() + "', '" + cbxTenDVT.Text + txtMoTa.Text + ")");
                            MessageBox.Show("Đã thêm thành công!");
                        }
                    }
                    else
                    {
                        DataTable t = dtBase.DataReader("select MaNhomHang from tblNhomHang where TenNhomHang = N'" + cbxTenNhomHang.Text + "'");
                        dtBase.UpdateData("insert into tblHang(MaHang, TenHang, GiaNhap, GiaBan, MaNhomHang, DonViTinh, MoTa) values('" + txtMaHang.Text + "', '" + txtTenHang.Text + "', '" + txtGiaNhap.Text + "', '" + txtGiaBan.Text + "', '" + t.Rows[0].ItemArray[0].ToString() + "', '" + cbxTenDVT.Text + txtMoTa.Text + ")");
                        MessageBox.Show("Đã thêm thành công!");
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(txtMaHang.Text != "" || txtTenHang.Text != "" || cbxTenNhomHang.Text != "" || cbxTenDVT.Text != "" || txtGiaNhap.Text != "" || txtGiaBan.Text != "")
            {
                if (MessageBox.Show("Dữ liệu đã thay đổi! Bạn có muốn thoát? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {

                }
            }
            else
            {
                this.Close();
            }
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {
            float a;

            if (float.TryParse(txtGiaNhap.Text, out a) == false)
            {
                MessageBox.Show("Giá nhập phải là số!");
            }
        }

        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {
            float a;

            if (float.TryParse(txtGiaBan.Text, out a) == false)
            {
                MessageBox.Show("Giá bán phải là số!");
            }
        }
    }
}
