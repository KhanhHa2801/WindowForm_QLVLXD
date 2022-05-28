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
    public partial class ctrlTopDoanhSoKhachHang : UserControl
    {
        private static ctrlTopDoanhSoKhachHang _instance;

        DataAccess dtBase = new DataAccess();

        public static ctrlTopDoanhSoKhachHang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlTopDoanhSoKhachHang();
                return _instance;
            }
        }

        public ctrlTopDoanhSoKhachHang()
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
        private void ctrlTopDoanhSoKhachHang_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

            cbx1.Items.Add("Năm nay");
            cbx1.Items.Add("Năm ngoái");

            load_();
        }

        private void load_()
        {
            cbx1.Text = "Năm nay";
            NamNay();
        }

        private void NamNay()
        {
            DataTable dtHang = dtBase.DataReader("select tblDatHang.MaKH, Tenkh, sum(tongtien) as TongThu, sum(trangay) as trangay, sum(NoLai) as NoLai from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH where year(ngaygiaohang) = year(getdate()) group by tblDatHang.MaKH, TenKH order by TongThu desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 220;
            dgvHang.Columns[2].Width = 150;
            dgvHang.Columns[3].Width = 150;
            dgvHang.Columns[4].Width = 150;

            dgvHang.Columns[0].HeaderText = "Mã Khách hàng";
            dgvHang.Columns[1].HeaderText = "Tên Khách hàng";
            dgvHang.Columns[2].HeaderText = "Tổng thu";
            dgvHang.Columns[3].HeaderText = "Đã trả";
            dgvHang.Columns[4].HeaderText = "Còn nợ";

            int Tong = 0;
            int trangay = 0;
            int Nolai = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                trangay += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                Nolai += int.Parse(dgvHang.Rows[i].Cells[4].Value.ToString().Trim());
            }

            btnTong.Text = Tong.ToString();
            btnTongNo.Text = trangay.ToString();
            btnTongTra.Text = Nolai.ToString();
        }

        private void NamNgoai()
        {
            DataTable dtHang = dtBase.DataReader("select tblDatHang.MaKH, Tenkh, sum(tongtien) as TongThu, sum(trangay) as trangay, sum(NoLai) as NoLai from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH where year(ngaygiaohang) = year(getdate()) - 1 group by tblDatHang.MaKH, TenKH order by TongThu desc");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[0].Width = 150;
            dgvHang.Columns[1].Width = 220;
            dgvHang.Columns[2].Width = 150;
            dgvHang.Columns[3].Width = 150;
            dgvHang.Columns[4].Width = 150;

            dgvHang.Columns[0].HeaderText = "Mã Khách hàng";
            dgvHang.Columns[1].HeaderText = "Tên Khách hàng";
            dgvHang.Columns[2].HeaderText = "Tổng thu";
            dgvHang.Columns[3].HeaderText = "Đã trả";
            dgvHang.Columns[4].HeaderText = "Còn nợ";

            int Tong = 0;
            int trangay = 0;
            int Nolai = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                trangay += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                Nolai += int.Parse(dgvHang.Rows[i].Cells[4].Value.ToString().Trim());
            }

            btnTong.Text = Tong.ToString();
            btnTongNo.Text = trangay.ToString();
            btnTongTra.Text = Nolai.ToString();
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
            if (cbx1.Text == "Năm nay")
            {
                NamNay();
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.LightGray;
            }
            else
            {
                NamNgoai();
                txtTimKiem.Text = "Tìm kiếm";
                txtTimKiem.ForeColor = Color.LightGray;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                if (cbx1.Text == "Năm nay")
                {
                    NamNay();
                }
                else
                {
                    NamNgoai();
                }
            }
            else if (txtTimKiem.Text != "Tìm kiếm")
            {
                if (cbx1.Text == "Năm nay")
                {
                    DataTable dtHang = dtBase.DataReader("select tblDatHang.MaKH, Tenkh, sum(tongtien) as TongThu, sum(trangay) as trangay, sum(NoLai) as NoLai from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH where year(ngaygiaohang) = year(getdate()) and (tblDatHang.MaKH like '%" + txtTimKiem.Text.Trim() + "%' or TenKH like '%" + txtTimKiem.Text.Trim() + "%') group by tblDatHang.MaKH, TenKH order by TongThu desc");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[0].Width = 150;
                    dgvHang.Columns[1].Width = 220;
                    dgvHang.Columns[2].Width = 150;
                    dgvHang.Columns[3].Width = 150;
                    dgvHang.Columns[4].Width = 150;

                    dgvHang.Columns[0].HeaderText = "Mã Khách hàng";
                    dgvHang.Columns[1].HeaderText = "Tên Khách hàng";
                    dgvHang.Columns[2].HeaderText = "Tổng thu";
                    dgvHang.Columns[3].HeaderText = "Đã trả";
                    dgvHang.Columns[4].HeaderText = "Còn nợ";

                    int Tong = 0;
                    int trangay = 0;
                    int Nolai = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                        trangay += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                        Nolai += int.Parse(dgvHang.Rows[i].Cells[4].Value.ToString().Trim());
                    }

                    btnTong.Text = Tong.ToString();
                    btnTongNo.Text = trangay.ToString();
                    btnTongTra.Text = Nolai.ToString();
                }
                else
                {
                    DataTable dtHang = dtBase.DataReader("select tblDatHang.MaKH, Tenkh, sum(tongtien) as TongThu, sum(trangay) as trangay, sum(NoLai) as NoLai from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH where year(ngaygiaohang) = year(getdate()) - 1 and (tblDatHang.MaKH like '%" + txtTimKiem.Text.Trim() + "%' or TenKH like '%" + txtTimKiem.Text.Trim() + "%') group by tblDatHang.MaKH, TenKH order by TongThu desc");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[0].Width = 150;
                    dgvHang.Columns[1].Width = 220;
                    dgvHang.Columns[2].Width = 150;
                    dgvHang.Columns[3].Width = 150;
                    dgvHang.Columns[4].Width = 150;

                    dgvHang.Columns[0].HeaderText = "Mã Khách hàng";
                    dgvHang.Columns[1].HeaderText = "Tên Khách hàng";
                    dgvHang.Columns[2].HeaderText = "Tổng thu";
                    dgvHang.Columns[3].HeaderText = "Đã trả";
                    dgvHang.Columns[4].HeaderText = "Còn nợ";

                    int Tong = 0;
                    int trangay = 0;
                    int Nolai = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        Tong += int.Parse(dgvHang.Rows[i].Cells[2].Value.ToString().Trim());
                        trangay += int.Parse(dgvHang.Rows[i].Cells[3].Value.ToString().Trim());
                        Nolai += int.Parse(dgvHang.Rows[i].Cells[4].Value.ToString().Trim());
                    }

                    btnTong.Text = Tong.ToString();
                    btnTongNo.Text = trangay.ToString();
                    btnTongTra.Text = Nolai.ToString();
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
                header.Value = "Top Doanh Số Khách Hàng Theo " + cbx1.Text;

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
                exSheet.get_Range("B9").Value = "Mã khách hàng";
                exSheet.get_Range("B9").ColumnWidth = 15;
                exSheet.get_Range("C9").Value = "Tên khách hàng";
                exSheet.get_Range("C9").ColumnWidth = 30;

                exSheet.get_Range("D9").Value = "Tổng thu";
                exSheet.get_Range("D9").ColumnWidth = 15;
                exSheet.get_Range("E9").Value = "Đã trả";
                exSheet.get_Range("E9").ColumnWidth = 15;
                exSheet.get_Range("F9").Value = "Còn nợ";
                exSheet.get_Range("F9").ColumnWidth = 15;

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
                loinhuan.Value = "Tổng thu: " + btnTong.Text;


                exSheet.Name = "TopDoanhSoKhachHang";

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
