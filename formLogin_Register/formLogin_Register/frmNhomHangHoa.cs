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
    public partial class frmNhomHangHoa : Form
    {
        DataAccess dtBase = new DataAccess();

        public frmNhomHangHoa()
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
        private void frmNhomHangHoa_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

            labelKeNgang.AutoSize = false;
            labelKeNgang.Height = 3;
            labelKeNgang.Width = 420;
            labelKeNgang.BorderStyle = BorderStyle.Fixed3D;
            labelKeNgang.ForeColor = Color.Blue;

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
            DataTable dtHang = dtBase.DataReader("select * from tblNhomHang");
            dgvNhomHang.DataSource = dtHang;
            for (int i = 0; i < dgvNhomHang.RowCount; i = i + 2)
            {
                dgvNhomHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvNhomHang.Columns[0].HeaderText = "Mã Hàng";
            dgvNhomHang.Columns[1].HeaderText = "Tên Hàng";

            dgvNhomHang.Columns[0].Width = 150;
            dgvNhomHang.Columns[1].Width = 250;


            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaHang.Text = "";
            txtTenHang.Text = "";
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            load_();
        }

        private void dgvNhomHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            txtMaHang.Text = dgvNhomHang.Rows[numrow].Cells[0].Value.ToString().Trim();
            txtTenHang.Text = dgvNhomHang.Rows[numrow].Cells[1].Value.ToString().Trim();

            //if (frmLogin.chucvu == "Quản lý")
            //{
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            //}
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text == "" || txtTenHang.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đủ dữ liệu");
            }
            else
            {
                DataTable temp = dtBase.DataReader("select * from tblNhomHang where MaNhomHang = '" + txtMaHang.Text.Trim() + "'");

                if (temp.Rows.Count > 0)
                {
                    MessageBox.Show("Mã nhóm hàng đã tồn tại");
                    txtMaHang.Focus();
                }
                else
                {
                    dtBase.UpdateData("insert into tblNhomHang(MaNhomHang, TenNhomHang) values('" + txtMaHang.Text.Trim() + "', N'" + txtTenHang.Text.Trim() + "')");
                    MessageBox.Show("Đã thêm thành công!");
                }
            }
            load_();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                dtBase.UpdateData("Update tblNhomHang set TenNhomHang = N'" + txtTenHang.Text.Trim() + "' Where MaNhomHang = '" + txtMaHang.Text.Trim() + "'");
                load_();
            }
            catch
            {
                MessageBox.Show("Sửa thông tin không hợp lệ!");
                load_();
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa " + txtTenHang.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                dtBase.UpdateData("Delete from tblNhomHang where MaNhomHang = '" + txtMaHang.Text + "'");
                load_();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if(txtTimKiem.Text.Trim() == "")
            {
                load_();
            }
            else
            {
                DataTable dtHang = dtBase.DataReader("select * from tblNhomHang where MaNhomHang like N'%" + txtTimKiem.Text + "%' or TenNhomHang like N'%" + txtTimKiem.Text + "%'");
                dgvNhomHang.DataSource = dtHang;
                for (int i = 0; i < dgvNhomHang.RowCount; i = i + 2)
                {
                    dgvNhomHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                dgvNhomHang.Columns[0].HeaderText = "Mã Hàng";
                dgvNhomHang.Columns[1].HeaderText = "Tên Hàng";

                dgvNhomHang.Columns[0].Width = 150;
                dgvNhomHang.Columns[1].Width = 250;


                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                txtMaHang.Text = "";
                txtTenHang.Text = "";
            }
        }
    }
}
