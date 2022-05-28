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

using Excel = Microsoft.Office.Interop.Excel;

namespace formLogin_Register
{
    public partial class ctrlNhanVien : UserControl
    {
        private static ctrlNhanVien _instance;

        DataAccess dtBase = new DataAccess();

        string imageName = "";

        public static ctrlNhanVien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlNhanVien();
                return _instance;
            }
        }
        public ctrlNhanVien()
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
        private void ctrlNhanVien_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);



            cbxCV.DataSource = dtBase.DataReader("select* from tblChucVu");
            cbxCV.DisplayMember = "TenChucVu";
            cbxCV.ValueMember = "MACV";
            cbxCV.Text = "";

            cbxGioiTinh.Items.Add("Nam");
            cbxGioiTinh.Items.Add("Nữ");
            cbxGioiTinh.Text = "";

            load_();
        }

        private void load_()
        {
            DataTable dtNV = dtBase.DataReader("select MaNV, TenNV, GioiTinh, NgaySinh, SDT, Anh, TenChucVu, LuongCB, HSL from tblNhanVien join tblChucVu on tblNhanVien.MaCV=tblChucVu.MaCV");
            dgvNV.DataSource = dtNV;
            for (int i = 0; i < dgvNV.RowCount; i = i + 2)
            {
                dgvNV.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            dgvNV.Columns[1].Width = 150;
            //dgvHang.Columns[4].Width = 200;
            //dgvHang.Columns[6].Width = 200;


            dgvNV.Columns[0].HeaderText = "Mã Nhân viên";
            dgvNV.Columns[1].HeaderText = "Tên Nhân viên";
            dgvNV.Columns[2].HeaderText = "Giới tính";
            dgvNV.Columns[3].HeaderText = "Ngày sinh";
            dgvNV.Columns[4].HeaderText = "SĐT";
            dgvNV.Columns[5].HeaderText = "Ảnh";
            dgvNV.Columns[6].HeaderText = "Chức vụ";
            dgvNV.Columns[7].HeaderText = "Lương cơ bản";
            dgvNV.Columns[8].HeaderText = "Hệ số lương";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaNV.Text = "";
            txtTenNV.Text = "";
            cbxGioiTinh.Text = "";
            txtNgaySinh.Text = "";
            txtSDT.Text = "";
            txtAnh.Text = "";
            cbxCV.Text = "";
            txtLuongCB.Text = "";
            txtHeSoLuong.Text = "";
            picNV.Image = null;
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if(txtTimKiem.Text.Trim() == "")
            {
                load_();
            }
            else
            {
                DataTable dtNV = dtBase.DataReader("select MaNV, TenNV, GioiTinh, NgaySinh, SDT, Anh, TenChucVu, LuongCB, HSL from tblNhanVien join tblChucVu on tblNhanVien.MaCV=tblChucVu.MaCV where MaNV like N'%" + txtTimKiem.Text.Trim() + "%' or TenNV like N'%" + txtTimKiem.Text.Trim() + "%'");
                dgvNV.DataSource = dtNV;
                for (int i = 0; i < dgvNV.RowCount; i = i + 2)
                {
                    dgvNV.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                dgvNV.Columns[1].Width = 150;
                //dgvHang.Columns[4].Width = 200;
                //dgvHang.Columns[6].Width = 200;


                dgvNV.Columns[0].HeaderText = "Mã Nhân viên";
                dgvNV.Columns[1].HeaderText = "Tên Nhân viên";
                dgvNV.Columns[2].HeaderText = "Giới tính";
                dgvNV.Columns[3].HeaderText = "Ngày sinh";
                dgvNV.Columns[4].HeaderText = "SĐT";
                dgvNV.Columns[5].HeaderText = "Ảnh";
                dgvNV.Columns[6].HeaderText = "Chức vụ";
                dgvNV.Columns[7].HeaderText = "Lương cơ bản";
                dgvNV.Columns[8].HeaderText = "Hệ số lương";

                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                txtMaNV.Text = "";
                txtTenNV.Text = "";
                cbxGioiTinh.Text = "";
                txtNgaySinh.Text = "";
                txtSDT.Text = "";
                txtAnh.Text = "";
                cbxCV.Text = "";
                txtLuongCB.Text = "";
                txtHeSoLuong.Text = "";
                picNV.Image = null;
            }
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

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            cbxGioiTinh.Text = "";
            txtNgaySinh.Text = "";
            txtSDT.Text = "";
            txtAnh.Text = "";
            cbxCV.Text = "";
            txtLuongCB.Text = "";
            txtHeSoLuong.Text = "";

            picNV.Image = null;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            load_();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" || txtTenNV.Text == "" || cbxGioiTinh.Text == "" || txtNgaySinh.Text == "" || txtSDT.Text == "" || cbxCV.Text == "" || txtLuongCB.Text == "" || txtHeSoLuong.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đủ dữ liệu");
            }
            else
            {
                DataTable temp = dtBase.DataReader("select * from tblNhanVien where MaNV = '" + txtMaNV.Text + "'");

                if (temp.Rows.Count > 0)
                {
                    MessageBox.Show("Mã Nhân viên đã tồn tại");
                    txtMaNV.Focus();
                }
                else if (txtAnh.Text == "")
                {
                    DataTable t = dtBase.DataReader("select MaCV from tblChucVu where TenChucVu = N'" + cbxCV.Text + "'");
                    dtBase.UpdateData("insert into tblNhanVien(MaNV, TenNV, GioiTinh, NgaySinh, SDT, HSL, LuongCB, MaCV) values('" + txtMaNV.Text + "', N'" + txtTenNV.Text + "', N'" + cbxGioiTinh.Text + "', '" + txtNgaySinh.Text + "', '" + txtSDT.Text + "', '" + txtHeSoLuong.Text + "', '" + txtLuongCB.Text + "', N'" + t.Rows[0].ItemArray[0].ToString() +"')");
                    MessageBox.Show("Đã thêm thành công!");
                }
                else
                {
                    DataTable t = dtBase.DataReader("select MaCV from tblChucVu where TenChucVu = N'" + cbxCV.Text + "'");
                    dtBase.UpdateData("insert into tblNhanVien(MaNV, TenNV, GioiTinh, NgaySinh, SDT, HSL, LuongCB, MaCV, Anh) values('" + txtMaNV.Text + "', N'" + txtTenNV.Text + "', N'" + cbxGioiTinh.Text + "', '" + txtNgaySinh.Text + "', '" + txtSDT.Text + "', '" + txtHeSoLuong.Text + "', '" + txtLuongCB.Text + "', N'" + t.Rows[0].ItemArray[0].ToString() + "', N'" + txtAnh.Text +"')");
                    MessageBox.Show("Đã thêm thành công!");
                }
            }
            load_();
        }

        private void txtHeSoLuong_TextChanged(object sender, EventArgs e)
        {
            float a;
            if (txtHeSoLuong.Text != "")
            {
                if (float.TryParse(txtHeSoLuong.Text, out a) == false)
                {
                    MessageBox.Show("Hệ só lương phải là số!");
                }
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (txtSDT.Text != "")
            {
                if (int.TryParse(txtSDT.Text, out a) == false)
                {
                    MessageBox.Show("SĐT phải là số!");
                }
            }
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dgvNV.CurrentRow.Cells[0].Value.ToString().Trim();
            txtTenNV.Text = dgvNV.CurrentRow.Cells[1].Value.ToString().Trim();
            cbxGioiTinh.Text = dgvNV.CurrentRow.Cells[2].Value.ToString().Trim();

            string[] temp;
            temp = dgvNV.CurrentRow.Cells[3].Value.ToString().Trim().Split(' ');
            txtNgaySinh.Text = temp[0];

            txtSDT.Text = dgvNV.CurrentRow.Cells[4].Value.ToString().Trim();
            txtAnh.Text = dgvNV.CurrentRow.Cells[5].Value.ToString().Trim();
            cbxCV.Text = dgvNV.CurrentRow.Cells[6].Value.ToString().Trim();
            txtLuongCB.Text = dgvNV.CurrentRow.Cells[7].Value.ToString().Trim();
            txtHeSoLuong.Text = dgvNV.CurrentRow.Cells[8].Value.ToString().Trim();
            try
            {
                picNV.Image = Image.FromFile(Application.StartupPath.ToString() + "\\Images\\NV\\" + txtAnh.Text);
            }
            catch(Exception ex)
            {
                picNV.Image = null;
                MessageBox.Show("Lỗi hiển thị ảnh");
            }

            if (frmLogin.chucvu == "Quản lý")
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnOpenAnh_Click(object sender, EventArgs e)
        {
            string[] pathAnh;
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "PNG Images|*.png|JPEG Images|*.jpg|All Files|*.*";
            dlgOpen.InitialDirectory = Application.StartupPath.ToString() + "\\Images\\NV";
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picNV.Image = Image.FromFile(dlgOpen.FileName);
                pathAnh = dlgOpen.FileName.Split('\\');
                imageName = pathAnh[pathAnh.Length - 1];
                txtAnh.Text = imageName;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataTable t = dtBase.DataReader("select MaCV from tblChucVu where TenChucVu = N'" + cbxCV.Text + "'");
            //string t2 = "update tblNhanVien set TenNV = N'" + txtTenNV.Text + "', GioiTinh = N'" + cbxGioiTinh.Text + "', NgaySinh = '" + txtNgaySinh.Text + "', SDT = '" + txtSDT.Text + "', Anh = N'" + txtAnh.Text + "', HSL = '" + txtHeSoLuong.Text + "', LuongCB = '" + txtLuongCB.Text + "', MaCV = '" + t.Rows[0].ItemArray[0].ToString() + "' where MaNV = '" + txtMaNV.Text.Trim() + "'";
            //MessageBox.Show(t2);
            dtBase.UpdateData("update tblNhanVien set TenNV = N'" + txtTenNV.Text + "', GioiTinh = N'" + cbxGioiTinh.Text + "', NgaySinh = '" + txtNgaySinh.Text + "', SDT = '" + txtSDT.Text + "', Anh = N'" + txtAnh.Text + "', HSL = '" + txtHeSoLuong.Text + "', LuongCB = '" + txtLuongCB.Text + "', MaCV = '" + t.Rows[0].ItemArray[0].ToString() + "' where MaNV = '" + txtMaNV.Text.Trim() + "'");
            load_();
        }

        private void txtLuongCB_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (txtLuongCB.Text != "")
            {
                if (int.TryParse(txtLuongCB.Text, out a) == false)
                {
                    MessageBox.Show("Lương phải là số!");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa " + txtTenNV.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                dtBase.UpdateData("Delete from tblNhanVien where MaNV = '" + txtMaNV.Text + "'");
                load_();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

            try
            {
                Excel.Range tenCuaHang = (Excel.Range)exSheet.Cells[1, 1];
                exSheet.get_Range("A1:C1").Merge(true);
                tenCuaHang.Font.Size = 12;
                tenCuaHang.Font.Bold = true;
                tenCuaHang.Font.Color = Color.Blue;
                if (frmThongTinCongTy.tenCongTy == "")
                {
                    tenCuaHang.Value = "Đại lý vật tư xây dựng Trúc-Khánh-Hoa-Trang";

                }
                else
                {
                    tenCuaHang.Value = frmThongTinCongTy.tenCongTy;
                }

                Excel.Range dcCuaHang = (Excel.Range)exSheet.Cells[2, 1];
                exSheet.get_Range("A2:C2").Merge(true);
                dcCuaHang.Font.Size = 12;
                dcCuaHang.Font.Bold = true;
                dcCuaHang.Font.Color = Color.Blue;
                if (frmThongTinCongTy.diaChi == "")
                {
                    dcCuaHang.Value = "Địa chỉ: Ngôi nhà nhỏ bên Hồ Sen";

                }
                else
                {
                    dcCuaHang.Value = frmThongTinCongTy.diaChi;
                }

                Excel.Range sdt = (Excel.Range)exSheet.Cells[3, 1];
                exSheet.get_Range("A3:C3").Merge(true);
                sdt.Font.Size = 12;
                sdt.Font.Bold = true;
                sdt.Font.Color = Color.Blue;
                if (frmThongTinCongTy.SDT == "")
                {
                    sdt.Value = "Điện thoại: 0345.999.999";

                }
                else
                {
                    sdt.Value = frmThongTinCongTy.SDT;
                }

                Excel.Range header = (Excel.Range)exSheet.Cells[5, 2];
                exSheet.get_Range("B5:G5").Merge(true);
                header.Font.Size = 13;
                header.Font.Bold = true;
                header.Font.Color = Color.Red;
                header.Value = "DANH SÁCH NHÂN  VIÊN";

                exSheet.get_Range("A7:H7").Font.Bold = true;
                exSheet.get_Range("A7:H7").HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                exSheet.get_Range("A7").Value = "STT";
                exSheet.get_Range("B7").Value = "Mã NV";

                exSheet.get_Range("C7").Value = "Tên nhân viên";
                exSheet.get_Range("C7").ColumnWidth = 20;

                exSheet.get_Range("D7").Value = "Giới tính";
                exSheet.get_Range("E7").Value = "Ngày sinh";
                exSheet.get_Range("E7").ColumnWidth = 15;

                exSheet.get_Range("F7").Value = "SĐT";
                exSheet.get_Range("F7").ColumnWidth = 15;
                exSheet.get_Range("G7").Value = "Chức vụ";
                
                exSheet.get_Range("H7").Value = "Lương cơ bản";
                exSheet.get_Range("H7").ColumnWidth = 15;

                DataTable dtNV2 = dtBase.DataReader("select MaNV, TenNV, GioiTinh, NgaySinh, SDT, TenChucVu, LuongCB from tblNhanVien join tblChucVu on tblNhanVien.MaCV=tblChucVu.MaCV");

                for (int i = 0; i < dtNV2.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dtNV2.Columns.Count; j++)
                    {
                        exSheet.Cells[i + 8, 1] = (i + 1).ToString();
                        exSheet.Cells[i + 8, j + 2] = dtNV2.Rows[i].ItemArray[j].ToString();
                    }
                }
                exSheet.Name = "NhanVien";

                exBook.Activate();
                SaveFileDialog dlgSave = new SaveFileDialog();
                dlgSave.Filter = "Excel Document(*.xls)|*.xls |Word Document(*.doc) | *.doc | All files(*.*) | *.* ";
                dlgSave.FilterIndex = 1;
                dlgSave.AddExtension = true;
                dlgSave.DefaultExt = ".xls";

                if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    exBook.SaveAs(dlgSave.FileName.ToString());
                }
                exApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                exBook = null;
                exSheet = null;
            }
        }
    }
}
