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
    public partial class ctrlTopMatHangBanChay : UserControl
    {
        private static ctrlTopMatHangBanChay _instance;

        DataAccess dtBase = new DataAccess();

        public static ctrlTopMatHangBanChay Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlTopMatHangBanChay();
                return _instance;
            }
        }
        public ctrlTopMatHangBanChay()
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
        private void ctrlTopMatHangBanChay_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

            cbx1.Items.Add("Toàn kỳ");
            cbx1.Items.Add("Năm nay");
            cbx1.Items.Add("Quý gần nhất");
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
            DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong,  sum(convert(int, soluong)*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 320;
            dgvHang.Columns[2].Width = 160;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã hàng";
            dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
            dgvHang.Columns[2].HeaderText = "Số lượng bán";
            dgvHang.Columns[3].HeaderText = "Doanh thu";

            int Tong = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnTongDoanhThu.Text = Tong.ToString();
        }

        private void NamNay()
        {
            DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong,  sum(convert(int, soluong)*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where YEAR(ngaygiaohang) = year(getdate()) group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 320;
            dgvHang.Columns[2].Width = 160;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã hàng";
            dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
            dgvHang.Columns[2].HeaderText = "Số lượng bán";
            dgvHang.Columns[3].HeaderText = "Doanh thu";

            int Tong = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnTongDoanhThu.Text = Tong.ToString();
        }

        private void QuyNay()
        {
            DateTime d1 = DateTime.Now;
            int month = d1.Month;
            int year = d1.Year;
            int mon_start;
            int mon_end;
            if (month < 3)
            {
                mon_start = (month / 3 - 1) * 3 + 1 + 12;
                mon_end = mon_start + 2;
                year = year - 1;
            }
            else
            {
                mon_start = (month / 3 - 1) * 3 + 1;
                mon_end = mon_start + 2;
            }

            DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong, sum(soluong*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where Month(ngaygiaohang) between " + mon_start + " and " + mon_end + " and Year(ngaygiaohang) = " + year + " group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 320;
            dgvHang.Columns[2].Width = 160;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã hàng";
            dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
            dgvHang.Columns[2].HeaderText = "Số lượng bán";
            dgvHang.Columns[3].HeaderText = "Doanh thu";

            int Tong = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnTongDoanhThu.Text = Tong.ToString();
        }

        private void ThangNay()
        {
            DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong,  sum(convert(int, soluong)*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where Month(ngaygiaohang) = Month(getdate()) group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 320;
            dgvHang.Columns[2].Width = 160;
            dgvHang.Columns[3].Width = 210;

            dgvHang.Columns[0].HeaderText = "Mã hàng";
            dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
            dgvHang.Columns[2].HeaderText = "Số lượng bán";
            dgvHang.Columns[3].HeaderText = "Doanh thu";

            int Tong = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
            }

            btnTongDoanhThu.Text = Tong.ToString();
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
            else if(cbx1.Text == "Quý gần nhất")
            {
                QuyNay();
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
                else if (cbx1.Text == "Quý gần nhất")
                {
                    QuyNay();
                }
                else
                {
                    ThangNay();
                }
            }
            else if (txtTimKiem.Text != "Tìm kiếm")
            {
                if (cbx1.Text == "Toàn kỳ")
                {

                    {
                        DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong,  sum(convert(int, soluong)*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where tblChiTietDatHang.mahang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%' group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
                        dgvHang.DataSource = dtHang;
                        for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                        {
                            dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                        }

                        dgvHang.Columns[0].Width = 150;
                        dgvHang.Columns[1].Width = 270;
                        dgvHang.Columns[2].Width = 210;
                        dgvHang.Columns[3].Width = 210;

                        dgvHang.Columns[0].HeaderText = "Mã hàng";
                        dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
                        dgvHang.Columns[2].HeaderText = "Số lượng bán";
                        dgvHang.Columns[3].HeaderText = "Doanh thu";

                        int Tong = 0;

                        for (int i = 0; i < dgvHang.RowCount - 1; i++)
                        {
                            Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                        }

                        btnTongDoanhThu.Text = Tong.ToString();
                    }
                }
                else if (cbx1.Text == "Năm nay")
                {

                    {
                        DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong,  sum(convert(int, soluong)*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where YEAR(ngaygiaohang) = year(getdate()) and (tblChiTietDatHang.mahang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%') group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
                        dgvHang.DataSource = dtHang;
                        for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                        {
                            dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                        }

                        dgvHang.Columns[0].Width = 150;
                        dgvHang.Columns[1].Width = 270;
                        dgvHang.Columns[2].Width = 210;
                        dgvHang.Columns[3].Width = 210;

                        dgvHang.Columns[0].HeaderText = "Mã hàng";
                        dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
                        dgvHang.Columns[2].HeaderText = "Số lượng bán";
                        dgvHang.Columns[3].HeaderText = "Doanh thu";

                        int Tong = 0;

                        for (int i = 0; i < dgvHang.RowCount - 1; i++)
                        {
                            Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                        }

                        btnTongDoanhThu.Text = Tong.ToString();
                    }
                }
                else if (cbx1.Text == "Quý gần nhất")
                {

                    {
                        DateTime d1 = DateTime.Now;
                        int month = d1.Month;
                        int year = d1.Year;
                        int mon_start;
                        int mon_end;
                        if (month < 3)
                        {
                            mon_start = (month / 3 - 1) * 3 + 1 + 12;
                            mon_end = mon_start + 2;
                            year = year - 1;
                        }
                        else
                        {
                            mon_start = (month / 3 - 1) * 3 + 1;
                            mon_end = mon_start + 2;
                        }

                        DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong, sum(soluong*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where Month(ngaygiaohang) between " + mon_start + " and " + mon_end + " and Year(ngaygiaohang) = " + year + " and (tblChiTietDatHang.mahang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%') group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
                        dgvHang.DataSource = dtHang;
                        for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                        {
                            dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                        }

                        dgvHang.Columns[0].Width = 150;
                        dgvHang.Columns[1].Width = 270;
                        dgvHang.Columns[2].Width = 210;
                        dgvHang.Columns[3].Width = 210;

                        dgvHang.Columns[0].HeaderText = "Mã hàng";
                        dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
                        dgvHang.Columns[2].HeaderText = "Số lượng bán";
                        dgvHang.Columns[3].HeaderText = "Doanh thu";

                        int Tong = 0;

                        for (int i = 0; i < dgvHang.RowCount - 1; i++)
                        {
                            Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                        }

                        btnTongDoanhThu.Text = Tong.ToString();
                    }
                }
                else
                {

                    {
                        DataTable dtHang = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, tblHang.TenHang, sum(convert(int, soluong)) as SoLuong,  sum(convert(int, soluong)*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where Month(ngaygiaohang) = Month(getdate()) and (tblChiTietDatHang.mahang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%') group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");
                        dgvHang.DataSource = dtHang;
                        for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                        {
                            dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                        }

                        dgvHang.Columns[0].Width = 150;
                        dgvHang.Columns[1].Width = 270;
                        dgvHang.Columns[2].Width = 210;
                        dgvHang.Columns[3].Width = 210;

                        dgvHang.Columns[0].HeaderText = "Mã hàng";
                        dgvHang.Columns[1].HeaderText = "Tên mặt hàng";
                        dgvHang.Columns[2].HeaderText = "Số lượng bán";
                        dgvHang.Columns[3].HeaderText = "Doanh thu";

                        int Tong = 0;

                        for (int i = 0; i < dgvHang.RowCount - 1; i++)
                        {
                            Tong += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                        }

                        btnTongDoanhThu.Text = Tong.ToString();
                    }
                }
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
                header.Value = "Top Mặt hàng bán chạy theo " + cbx1.Text;

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
                exSheet.get_Range("B9").Value = "Mã mặt hàng";
                exSheet.get_Range("B9").ColumnWidth = 15;
                exSheet.get_Range("C9").Value = "Tên mặt hàng";
                exSheet.get_Range("C9").ColumnWidth = 30;

                exSheet.get_Range("D9").Value = "Số lượng bán";
                exSheet.get_Range("D9").ColumnWidth = 15;
                exSheet.get_Range("E9").Value = "Doanh thu";
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
                loinhuan.Value = "Tổng tiền bán hàng: " + btnTongDoanhThu.Text;


                exSheet.Name = "TopHangBanChay";

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
