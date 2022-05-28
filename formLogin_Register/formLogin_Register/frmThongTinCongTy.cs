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
    public partial class frmThongTinCongTy : Form
    {

        DataAccess dtBase = new DataAccess();

        public static string tenCongTy = "";
        public static string tenCuaHang = "";
        public static string diaChi = "";
        public static string SDT = "";
        public static string Email = "";
        public static string maSoThue = "";
        public static string soTaiKhoan = "";
        public static string tenNganHang = "";


        public frmThongTinCongTy()
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmThongTinCongTy_Load(object sender, EventArgs e)
        {
            DataTable t = dtBase.DataReader("Select * from tblThongTinCongTy");

            if(t.Rows.Count < 1)
            {
                txtTenCongTy.Text = "";
                txtTenCuaHang.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
                txtEmail.Text = "";
                txtMaSoThue.Text = "";
                txtSoTaiKhoan.Text = "";
                txtTenNganHang.Text = "";
            }
            else
            {
                tenCongTy = t.Rows[0].ItemArray[0].ToString().Trim();
                tenCuaHang = t.Rows[0].ItemArray[1].ToString().Trim();
                diaChi = t.Rows[0].ItemArray[2].ToString().Trim();
                SDT = t.Rows[0].ItemArray[3].ToString().Trim();
                Email = t.Rows[0].ItemArray[4].ToString().Trim();
                maSoThue = t.Rows[0].ItemArray[5].ToString().Trim();
                soTaiKhoan = t.Rows[0].ItemArray[6].ToString().Trim();
                tenNganHang = t.Rows[0].ItemArray[7].ToString().Trim();

                txtTenCongTy.Text = tenCongTy;
                txtTenCuaHang.Text = tenCuaHang;
                txtDiaChi.Text = diaChi;
                txtSDT.Text = SDT;
                txtEmail.Text = Email;
                txtMaSoThue.Text = maSoThue;
                txtSoTaiKhoan.Text = soTaiKhoan;
                txtTenNganHang.Text = tenNganHang;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            dtBase.UpdateData("update tblThongTinCongTy set TenCty = N'" + txtTenCongTy.Text + "', TenCHang = N'" + txtTenCuaHang.Text + "', DiaChi = N'" + txtDiaChi.Text + "', SDT = N'" + txtSDT.Text + "', Email = N'" + txtEmail.Text + "', MaSoThue = N'" + txtMaSoThue.Text + "', SoTaiKhoan = N'" + txtSoTaiKhoan.Text + "', TenNganHang = N'" + txtTenNganHang.Text + "'");

            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
