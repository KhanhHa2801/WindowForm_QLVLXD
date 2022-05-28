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
    public partial class ctrlNhapHang : UserControl
    {
        private static ctrlNhapHang _instance;

        DataAccess dtBase = new DataAccess();

        public static string ma_hdd_ = "";
        public static string tenkh = "";
        public static string tennv = "";

        public static ctrlNhapHang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlNhapHang();
                return _instance;
            }
        }
        public ctrlNhapHang()
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
        private void ctrlNhapHang_Load(object sender, EventArgs e)
        {
            label4.AutoSize = false;
            label4.Height = 3;
            label4.Width = 945;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.ForeColor = Color.Blue;

            txtTimKiem.Text = "Tìm kiếm";
            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

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
            DataTable dtKH = dtBase.DataReader("select MaHDN, TenNCC, TenNV, NgayNhap, TongTien, TraNgay, NoLai from tblNhapKho join tblNhaCungCap on tblNhapKho.MaNCC = tblNhaCungCap.MaNCC join tblNhanVien on tblNhapKho.MaNV = tblNhanVien.MaNV");
            dgvHD.DataSource = dtKH;
            for (int i = 0; i < dgvHD.RowCount; i = i + 2)
            {
                dgvHD.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHD.Columns[1].Width = 260;
            dgvHD.Columns[2].Width = 120;

            dgvHD.Columns[0].HeaderText = "Mã HĐ";
            dgvHD.Columns[1].HeaderText = "Tên Nhà cung cấp";
            dgvHD.Columns[2].HeaderText = "Tên Nhân viên";
            dgvHD.Columns[3].HeaderText = "Ngày nhập";
            dgvHD.Columns[4].HeaderText = "Tổng tiền";
            dgvHD.Columns[5].HeaderText = "Đã trả";
            dgvHD.Columns[6].HeaderText = "Còn nợ";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaHD.Text = "";
            txtTenKH.Text = "";
            txtTenNV.Text = "";
            txtTongTien.Text = "";
            txtNgayDat.Text = "";
            txtTraNgay.Text = "";
            txtNoLai.Text = "";
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                load_();
            }
            else if(txtTimKiem.Text != "Tìm kiếm")
            {
                DataTable dtKH = dtBase.DataReader("select MaHDN, TenNCC, TenNV, NgayNhap, TongTien, TraNgay, NoLai from tblNhapKho join tblNhaCungCap on tblNhapKho.MaNCC = tblNhaCungCap.MaNCC join tblNhanVien on tblNhapKho.MaNV = tblNhanVien.MaNV where MaHDN like N'%" + txtTimKiem.Text + "%' or TenNCC like N'%" + txtTimKiem.Text + "%'");
                dgvHD.DataSource = dtKH;
                for (int i = 0; i < dgvHD.RowCount; i = i + 2)
                {
                    dgvHD.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                dgvHD.Columns[1].Width = 260;
                dgvHD.Columns[2].Width = 120;

                dgvHD.Columns[0].HeaderText = "Mã HĐ";
                dgvHD.Columns[1].HeaderText = "Tên Nhà cung cấp";
                dgvHD.Columns[2].HeaderText = "Tên Nhân viên";
                dgvHD.Columns[3].HeaderText = "Ngày nhập";
                dgvHD.Columns[4].HeaderText = "Tổng tiền";
                dgvHD.Columns[5].HeaderText = "Đã trả";
                dgvHD.Columns[6].HeaderText = "Còn nợ";

                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                txtMaHD.Text = "";
                txtTenKH.Text = "";
                txtTenNV.Text = "";
                txtTongTien.Text = "";
                txtNgayDat.Text = "";
                txtTraNgay.Text = "";
                txtNoLai.Text = "";
            }
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            
            txtMaHD.Text = dgvHD.Rows[numrow].Cells[0].Value.ToString().Trim();
            txtTenKH.Text = dgvHD.Rows[numrow].Cells[1].Value.ToString().Trim();
            txtTenNV.Text = dgvHD.Rows[numrow].Cells[2].Value.ToString().Trim();

            txtTongTien.Text = dgvHD.Rows[numrow].Cells[4].Value.ToString().Trim();
            txtTraNgay.Text = dgvHD.Rows[numrow].Cells[5].Value.ToString().Trim();
            txtNoLai.Text = dgvHD.Rows[numrow].Cells[6].Value.ToString().Trim();

            string[] ngaydat;
            ngaydat = dgvHD.CurrentRow.Cells[3].Value.ToString().Trim().Split(' ');
            txtNgayDat.Text = ngaydat[0];

            ma_hdd_ = txtMaHD.Text;
            tenkh = txtTenKH.Text;
            tennv = txtTenNV.Text;

            //if (frmLogin.chucvu == "Quản lý")
            //{
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //}
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = "";
            txtTenKH.Text = "";
            txtTenNV.Text = "";
            txtTongTien.Text = "";
            txtNgayDat.Text = "";
            txtTraNgay.Text = "";
            txtNoLai.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            load_();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa " + txtMaHD.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                dtBase.UpdateData("delete from tblChiTietNhapKho where MaHDN= '" + txtMaHD.Text + "'");
                dtBase.UpdateData("delete from tblNhapKho where MaHDN= '" + txtMaHD.Text + "'");
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
                header.Value = "DANH SÁCH HÓA ĐƠN NHẬP";

                exSheet.get_Range("A7:J7").Font.Bold = true;
                exSheet.get_Range("A7:J7").HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                exSheet.Rows.WrapText = true;

                exSheet.get_Range("A7").Value = "STT";
                exSheet.get_Range("B7").Value = "Mã Hóa đơn";
                exSheet.get_Range("B7").ColumnWidth = 12;
                exSheet.get_Range("C7").Value = "Tên Đối tác";
                exSheet.get_Range("C7").ColumnWidth = 20;
                exSheet.get_Range("D7").Value = "Nhân viên lập";
                exSheet.get_Range("D7").ColumnWidth = 20;
                exSheet.get_Range("E7").Value = "Ngày nhập";
                exSheet.get_Range("E7").ColumnWidth = 15;
                exSheet.get_Range("F7").Value = "Tổng tiền";
                exSheet.get_Range("F7").ColumnWidth = 10;
                exSheet.get_Range("G7").Value = "Đã Trả";
                exSheet.get_Range("G7").ColumnWidth = 10;
                exSheet.get_Range("H7").Value = "Còn nợ";
                exSheet.get_Range("H7").ColumnWidth = 10;


                for (int i = 0; i < dgvHD.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvHD.Columns.Count; j++)
                    {
                        exSheet.Cells[i + 8, 1] = (i + 1).ToString();
                        exSheet.Cells[i + 8, j + 2] = dgvHD.Rows[i].Cells[j].Value.ToString();
                    }
                }
                exSheet.Name = "HDSi";

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

        private void button1_Click(object sender, EventArgs e)
        {
            frmThem_HDN test = new frmThem_HDN();
            test.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmSuaHD_Nhap test = new frmSuaHD_Nhap();
            test.ShowDialog();
        }
    }
}
