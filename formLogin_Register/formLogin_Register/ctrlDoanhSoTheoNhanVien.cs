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
    public partial class ctrlDoanhSoTheoNhanVien : UserControl
    {
        private static ctrlDoanhSoTheoNhanVien _instance;

        DataAccess dtBase = new DataAccess();

        public static ctrlDoanhSoTheoNhanVien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlDoanhSoTheoNhanVien();
                return _instance;
            }
        }
        public ctrlDoanhSoTheoNhanVien()
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

        private void ctrlDoanhSoTheoNhanVien_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

            cbx1.Items.Add("Toàn kỳ");
            cbx1.Items.Add("Năm nay");
            cbx1.Items.Add("Tháng này");

            load_();
        }

        private void load_()
        {
            cbx1.Text = "Toàn kỳ";
            ToanKy();
        }

        private void ToanKy()
        {
            DataTable dtHang = dtBase.DataReader("select tblDatHang.MaNV, TenNV, sum(TongTien) as Tong, sum(TraNgay) as ThucThu from tblDatHang join tblNhanVien on tblDatHang.MaNV = tblNhanVien.MaNV group by tblDatHang.MaNV, TenNV order by Tong desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 270;
            dgvHang.Columns[2].Width = 210;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã nhân viên";
            dgvHang.Columns[1].HeaderText = "Tên nhân viên";
            dgvHang.Columns[2].HeaderText = "Tổng";
            dgvHang.Columns[3].HeaderText = "Thực thu";

            int Tong = 0;
            int ThuThu = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                ThuThu += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnLoiNhuan.Text = Tong.ToString();
            btnTongTienNhap.Text = ThuThu.ToString();
        }

        private void NamNay()
        {
            DataTable dtHang = dtBase.DataReader("select tblDatHang.MaNV, TenNV, sum(TongTien) as Tong, sum(TraNgay) as ThucThu from tblDatHang join tblNhanVien on tblDatHang.MaNV = tblNhanVien.MaNV where year(NgayDatHang) = year(getdate()) group by tblDatHang.MaNV, TenNV order by Tong desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 270;
            dgvHang.Columns[2].Width = 210;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã nhân viên";
            dgvHang.Columns[1].HeaderText = "Tên nhân viên";
            dgvHang.Columns[2].HeaderText = "Tổng";
            dgvHang.Columns[3].HeaderText = "Thực thu";

            int Tong = 0;
            int ThuThu = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                ThuThu += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnLoiNhuan.Text = Tong.ToString();
            btnTongTienNhap.Text = ThuThu.ToString();
        }

        private void ThangNay()
        {
            DataTable dtHang = dtBase.DataReader("select tblDatHang.MaNV, TenNV, sum(TongTien) as Tong, sum(TraNgay) as ThucThu from tblDatHang join tblNhanVien on tblDatHang.MaNV = tblNhanVien.MaNV where month(NgayDatHang) = month(getdate()) group by tblDatHang.MaNV, TenNV order by Tong desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 270;
            dgvHang.Columns[2].Width = 210;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã nhân viên";
            dgvHang.Columns[1].HeaderText = "Tên nhân viên";
            dgvHang.Columns[2].HeaderText = "Tổng";
            dgvHang.Columns[3].HeaderText = "Thực thu";

            int Tong = 0;
            int ThuThu = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                ThuThu += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnLoiNhuan.Text = Tong.ToString();
            btnTongTienNhap.Text = ThuThu.ToString();
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
            if (txtTimKiem.Text == "Tìm kiếm")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void cbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx1.Text == "Toàn kỳ")
            {
                ToanKy();
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.LightGray;
            }
            else if (cbx1.Text == "Năm nay")
            {
                NamNay();
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.LightGray;
            }
            else
            {
                ThangNay();
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.LightGray;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                if (cbx1.Text == "Toàn kỳ")
                {
                    ToanKy();
                }
                else if (cbx1.Text == "Năm nay")
                {
                    NamNay();
                }
                else
                {
                    ThangNay();
                }
            }
            else if (txtTimKiem.Text != "Tìm kiếm")
            {
                if(cbx1.Text == "Toàn kỳ")
                {
                    DataTable dtHang = dtBase.DataReader("select tblDatHang.MaNV, TenNV, sum(TongTien) as Tong, sum(TraNgay) as ThucThu from tblDatHang join tblNhanVien on tblDatHang.MaNV = tblNhanVien.MaNV where tblDatHang.MaNV like '%" + txtTimKiem.Text.Trim() + "%' or TenNV like '%" + txtTimKiem.Text.Trim() + "%' group by tblDatHang.MaNV, TenNV order by Tong desc");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[0].Width = 150;
                    dgvHang.Columns[1].Width = 270;
                    dgvHang.Columns[2].Width = 210;
                    dgvHang.Columns[3].Width = 210;

                    dgvHang.Columns[0].HeaderText = "Mã nhân viên";
                    dgvHang.Columns[1].HeaderText = "Tên nhân viên";
                    dgvHang.Columns[2].HeaderText = "Tổng";
                    dgvHang.Columns[3].HeaderText = "Thực thu";

                    int Tong = 0;
                    int ThuThu = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                        ThuThu += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                    }

                    btnLoiNhuan.Text = Tong.ToString();
                    btnTongTienNhap.Text = ThuThu.ToString();
                }
                else if(cbx1.Text == "Năm nay")
                {
                    DataTable dtHang = dtBase.DataReader("select tblDatHang.MaNV, TenNV, sum(TongTien) as Tong, sum(TraNgay) as ThucThu from tblDatHang join tblNhanVien on tblDatHang.MaNV = tblNhanVien.MaNV where year(NgayDatHang) = year(getdate()) and (tblDatHang.MaNV like '%" + txtTimKiem.Text.Trim() + "%' or TenNV like '%" + txtTimKiem.Text.Trim() + "%') group by tblDatHang.MaNV, TenNV order by Tong desc");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[0].Width = 150;
                    dgvHang.Columns[1].Width = 270;
                    dgvHang.Columns[2].Width = 210;
                    dgvHang.Columns[3].Width = 210;

                    dgvHang.Columns[0].HeaderText = "Mã nhân viên";
                    dgvHang.Columns[1].HeaderText = "Tên nhân viên";
                    dgvHang.Columns[2].HeaderText = "Tổng";
                    dgvHang.Columns[3].HeaderText = "Thực thu";

                    int Tong = 0;
                    int ThuThu = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                        ThuThu += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                    }

                    btnLoiNhuan.Text = Tong.ToString();
                    btnTongTienNhap.Text = ThuThu.ToString();
                }
                else
                {
                    DataTable dtHang = dtBase.DataReader("select tblDatHang.MaNV, TenNV, sum(TongTien) as Tong, sum(TraNgay) as ThucThu from tblDatHang join tblNhanVien on tblDatHang.MaNV = tblNhanVien.MaNV where month(NgayDatHang) = month(getdate()) and (tblDatHang.MaNV like '%" + txtTimKiem.Text.Trim() + "%' or TenNV like '%" + txtTimKiem.Text.Trim() + "%') group by tblDatHang.MaNV, TenNV order by Tong desc");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[0].Width = 150;
                    dgvHang.Columns[1].Width = 270;
                    dgvHang.Columns[2].Width = 210;
                    dgvHang.Columns[3].Width = 210;

                    dgvHang.Columns[0].HeaderText = "Mã nhân viên";
                    dgvHang.Columns[1].HeaderText = "Tên nhân viên";
                    dgvHang.Columns[2].HeaderText = "Tổng";
                    dgvHang.Columns[3].HeaderText = "Thực thu";

                    int Tong = 0;
                    int ThuThu = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                        ThuThu += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                    }

                    btnLoiNhuan.Text = Tong.ToString();
                    btnTongTienNhap.Text = ThuThu.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
                header.Value = "Báo cáo doanh số theo nhân viên";

                Excel.Range ngay = (Excel.Range)exSheet.Cells[6, 2];
                exSheet.get_Range("B6:E6").Merge(true);
                ngay.Font.Bold = true;
                DateTime d = DateTime.Today;
                ngay.Value = "Ngày " + d.ToString("dd/MM/yyyy");

                exSheet.get_Range("A7:K7").Font.Bold = true;
                exSheet.get_Range("A7:K7").HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                exSheet.Rows.WrapText = true;

                exSheet.get_Range("A9").Value = "STT";
                exSheet.get_Range("B9").Value = "Mã nhân viên";
                exSheet.get_Range("B9").ColumnWidth = 15;
                exSheet.get_Range("C9").Value = "Tên nhân viên";
                exSheet.get_Range("C9").ColumnWidth = 30;

                exSheet.get_Range("D9").Value = "Tiền bán hàng";
                exSheet.get_Range("D9").ColumnWidth = 15;
                exSheet.get_Range("E9").Value = "Thực thu";
                exSheet.get_Range("E9").ColumnWidth = 15;

                for (int i = 0; i < dgvHang.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvHang.Columns.Count; j++)
                    {
                        exSheet.Cells[i + 10, 1] = (i + 1).ToString();
                        exSheet.Cells[i + 10, j + 2] = dgvHang.Rows[i].Cells[j].Value.ToString();
                    }
                }

                Excel.Range loinhuan = (Excel.Range)exSheet.Cells[dgvHang.Rows.Count + 10, 2];
                exSheet.get_Range("B" + (dgvHang.Rows.Count + 10).ToString() + ":E" + (dgvHang.Rows.Count + 10).ToString()).Merge(true);
                loinhuan.Font.Bold = true;
                loinhuan.Value = "Tổng tiền bán hàng: " + btnLoiNhuan.Text;

                Excel.Range tongnhap = (Excel.Range)exSheet.Cells[dgvHang.Rows.Count + 11, 2];
                exSheet.get_Range("B" + (dgvHang.Rows.Count + 11).ToString() + ":E" + (dgvHang.Rows.Count + 11).ToString()).Merge(true);
                tongnhap.Font.Bold = true;
                tongnhap.Value = "Tổng thực thu: " + btnTongTienNhap.Text;

                exSheet.Name = "DoanhSoNhanVien";

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnTongTienNhap_Click(object sender, EventArgs e)
        {

        }

        private void btnLoiNhuan_Click(object sender, EventArgs e)
        {

        }

        private void dgvHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
