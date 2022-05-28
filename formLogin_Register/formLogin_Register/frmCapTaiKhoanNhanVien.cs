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
    public partial class frmCapTaiKhoanNhanVien : Form
    {
        DataAccess dtBase = new DataAccess();

        Caesar caesar = new Caesar();
        public frmCapTaiKhoanNhanVien()
        {
            InitializeComponent();
            load_();
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
        private void frmCapTaiKhoanNhanVien_Load(object sender, EventArgs e)
        {
            //DateTime d = DateTime.Today;
            //txtNgayTao.Text = d.ToString("dd/MM/yyyy");

            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

            btnHienPass.Visible = false;

            cbxQuyen.SelectedItem = "";

            load_();
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Tìm kiếm")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void load_()
        {
            DataTable dtNV = dtBase.DataReader("select MaNV, TenNV from tblNhanVien where TenNV not in (select TenDangNhap from tblTaiKhoan2)");
            dgvNhanVien.DataSource = dtNV;
            for (int i = 0; i < dgvNhanVien.RowCount; i = i + 2)
            {
                dgvNhanVien.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvNhanVien.Columns[0].Width = 70;
            dgvNhanVien.Columns[1].Width = 120;

            dgvNhanVien.Columns[0].HeaderText = "Mã NV";
            dgvNhanVien.Columns[1].HeaderText = "Tên Nhân viên";

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHienPass.Enabled = false;

            txtTenNV.Text = "";
            txtMatKhau.Text = "";
            txtMatKhau2.Text = "";
            cbxQuyen.Text = "";

            DataTable dtTK = dtBase.DataReader("select * from tblTaiKhoan2");
            dgvTaiKhoan.DataSource = dtTK;
            for (int i = 0; i < dgvTaiKhoan.RowCount; i = i + 2)
            {
                dgvTaiKhoan.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvTaiKhoan.Columns[0].HeaderText = "Tên nhân viên";
            dgvTaiKhoan.Columns[1].HeaderText = "Mật khẩu đang lưu trữ";
            dgvTaiKhoan.Columns[2].HeaderText = "Quyền hạn";

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            load_();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            txtTenNV.Text = dgvNhanVien.Rows[numrow].Cells[1].Value.ToString().Trim();
            txtMatKhau.Text = "";
            txtMatKhau2.Text = "";
            cbxQuyen.Text = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnHienPass.Enabled = false;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if(txtTimKiem.Text.Trim() == "Tìm kiếm")
            {
                load_();
            }
            else if(txtTimKiem.Text.Trim() != "Tìm kiếm")
            {
                DataTable dtNV = dtBase.DataReader("select MaNV, TenNV from(select MaNV, TenNV from tblNhanVien where TenNV not in (select TenDangNhap from tblTaiKhoan2)) b where MaNV like '%" + txtTimKiem.Text.Trim() +"%' or TenNV like '%" + txtTimKiem.Text.Trim() + "%'");
                dgvNhanVien.DataSource = dtNV;
                for (int i = 0; i < dgvNhanVien.RowCount; i = i + 2)
                {
                    dgvNhanVien.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                dgvNhanVien.Columns[0].Width = 70;
                dgvNhanVien.Columns[1].Width = 120;

                dgvNhanVien.Columns[0].HeaderText = "Mã NV";
                dgvNhanVien.Columns[1].HeaderText = "Tên Nhân viên";

                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnHienPass.Enabled = false;

                txtTenNV.Text = "";
                txtMatKhau.Text = "";
                txtMatKhau2.Text = "";
                cbxQuyen.Text = "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(cbxQuyen.Text == "" || txtMatKhau.Text.Trim() == "" || txtMatKhau2.Text.Trim() == "")
            {
                MessageBox.Show("Điền đầy đủ dữ liệu trước khi thêm!");
                return;
            }
            else if (txtMatKhau.Text.Trim() != txtMatKhau2.Text.Trim())
            {
                MessageBox.Show("Xác nhận mật khẩu chưa đúng!");
                txtMatKhau.Focus();
                return;
            }
            else
            {
                string en_pass = "";
                en_pass = caesar.encrypt(txtMatKhau.Text.Trim(), 13);
                dtBase.UpdateData("insert into tblTaiKhoan2(TenDangNhap, MatKhau, ChucVu) values(N'" + txtTenNV.Text.Trim() + "', N'" + en_pass + "', N'" + cbxQuyen.Text.Trim() + "')");
                load_();
            }

        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            txtTenNV.Text = dgvTaiKhoan.Rows[numrow].Cells[0].Value.ToString().Trim();
            cbxQuyen.Text = dgvTaiKhoan.Rows[numrow].Cells[2].Value.ToString().Trim();

            string mk = dgvTaiKhoan.Rows[numrow].Cells[1].Value.ToString().Trim();
            txtMatKhau.Text = caesar.decrypt(mk, 13);
            txtMatKhau2.Text = caesar.decrypt(mk, 13);

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHienPass.Enabled = true;
        }

        private void btnHienPass_Click(object sender, EventArgs e)
        {
            
            //btnHienPass.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtTenNV.Text == frmLogin.tendangnhap)
            {
                if (MessageBox.Show("Bạn có chắc tự xóa mình khỏi hệ thống?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    dtBase.UpdateData("Delete from tblTaiKhoan2 where TenDangNhap = N'" + txtTenNV.Text + "'");
                    load_();
                    return;
                }
            }
            else if (MessageBox.Show("Bạn có muốn xóa " + txtTenNV.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                dtBase.UpdateData("Delete from tblTaiKhoan2 where TenDangNhap = N'" + txtTenNV.Text + "'");
                load_();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(txtMatKhau.Text.Trim() != txtMatKhau2.Text.Trim())
            {
                MessageBox.Show("Xác nhận mật khẩu chưa đúng!");
                return;
            }
            else
            {
                string enstr = caesar.encrypt(txtMatKhau.Text, 13);
                dtBase.DataReader("Update tblTaiKhoan2 Set MatKhau = N'" + enstr + "' where TenDangNhap = N'" + txtTenNV.Text + "'");
                dtBase.DataReader("Update tblTaiKhoan2 Set ChucVu = N'" + cbxQuyen.Text + "' where TenDangNhap = N'" + txtTenNV.Text + "'");
                load_();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (txtTenNV.Text != "" || cbxQuyen.Text != "" || txtMatKhau.Text != "" || txtMatKhau2.Text != "")
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
    }
}
