using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace formLogin_Register
{
    public partial class ctrlTongQuan : UserControl
    {
        private static ctrlTongQuan _instance;

        public static ctrlTongQuan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlTongQuan();
                return _instance;
            }
        }

        DataAccess dtBase = new DataAccess();

        public ctrlTongQuan()
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

        private void chartTop10HangTheoNam()
        {
            DataTable top10SL = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, SUBSTRING(tblHang.TenHang, 0, 11) as th, sum(soluong*Giaban) as doanhthu " +
                "from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD " +
                "where YEAR(ngaygiaohang) = year(getdate()) group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");

            chartTop10Hang.DataSource = top10SL;
            chartTop10Hang.Series["top10hang"].XValueMember = "th";
            chartTop10Hang.Series["top10hang"].YValueMembers = "doanhthu";

            chartTop10Hang.Series[0].ChartType = SeriesChartType.Pie;
        }

        private void chartTop10HangTheoQuy()
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

            DataTable top10SL = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, SUBSTRING(tblHang.TenHang, 0, 11) as th, sum(soluong*Giaban) as doanhthu " +
                "from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD " +
                "where Month(ngaygiaohang) between " + mon_start + " and " + mon_end + " and Year(ngaygiaohang) = " + year + " group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");

            chartTop10Hang.DataSource = top10SL;
            chartTop10Hang.Series["top10hang"].XValueMember = "th";
            chartTop10Hang.Series["top10hang"].YValueMembers = "doanhthu";

            chartTop10Hang.Series[0].ChartType = SeriesChartType.Pie;
        }

        private void chartTop10KHLastYear()
        {
            chart10KH.Series["top10kh"].Points.Clear();
            DataTable top10KH = dtBase.DataReader("select Top 10 Tenkh, sum(tongtien) as tt from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH " +
                "where year(ngaygiaohang) = year(getdate()) - 1 group by TenKH order by tt desc");
            chart10KH.DataSource = top10KH;
            chart10KH.Series["top10kh"].XValueMember = "Tenkh";
            chart10KH.Series["top10kh"].YValueMembers = "tt";
            chart10KH.ChartAreas[0].AxisY.Maximum = 6000000;
            chart10KH.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
        }

        private void chartTop10KHThisYear()
        {
            chart10KH.Series["top10kh"].Points.Clear();
            DataTable top10KH = dtBase.DataReader("select Top 10 Tenkh, sum(tongtien) as tt from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH " +
                "where year(ngaygiaohang) = year(getdate()) group by TenKH order by tt desc");
            chart10KH.DataSource = top10KH;
            chart10KH.Series["top10kh"].XValueMember = "Tenkh";
            chart10KH.Series["top10kh"].YValueMembers = "tt";
            chart10KH.ChartAreas[0].AxisY.Maximum = 6000000;
            chart10KH.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
        }

        private void chartTop10Hang_Click(object sender, EventArgs e)
        {

        }

        private void ctrlTongQuan_Load(object sender, EventArgs e)
        {
            labelHello.Text = "Xin chào " + frmLogin.tendangnhap;

            chartTop10Hang.Series.Add("top10hang");
            chart10KH.Series.Add("top10kh");

            cbxTop10.Items.Add("Quý gần nhất");
            cbxTop10.Items.Add("Năm nay");
            cbxTop10.SelectedItem = "Quý gần nhất";
            chartTop10HangTheoQuy();

            cbxKH.Items.Add("Năm nay");
            cbxKH.Items.Add("Năm ngoái");
            cbxKH.SelectedItem = "Năm nay";
            chartTop10KHThisYear();

            LoadDoanhThu();
        }

        private void LoadDoanhThu()
        {
            //Doanh thu hôm nay
            DataTable hom_nay = dtBase.DataReader("select isnull(TongTien, 0) from tblDatHang where NgayGiaoHang = ''");
            if (hom_nay.Rows.Count == 0)
            {
                btnDoanhSoHomNay.Text = "0";
            }
            else
            {
                btnDoanhSoHomNay.Text = hom_nay.Rows[0][0].ToString();
            }

            // Doan thu hôm qua
            DateTime d1 = DateTime.Now;
            int day = d1.Day;
            int month = d1.Month;
            int year = d1.Year;

            if (day == 1)
            {
                if (month == 1)
                {
                    day = 31;
                    month = 12;
                    year = year - 1;
                }
                else if (month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                {
                    day = 31;
                    month = month - 1;
                }
                else if (month == 2 && year % 4 == 0)
                {
                    day = 29;
                    month = month - 1;
                }
                else
                {
                    day = 30;
                    month = month - 1;
                }
            }
            else
            {
                day = day - 1;
            }

            //Doanh thu hôm qua
            DataTable hom_qua = dtBase.DataReader("select isnull(TongTien, 0) as tt from tblDatHang where DAY(NgayGiaoHang) = " + day + " and MONTH(NgayGiaoHang) = " + month + " and YEAR(NgayGiaoHang) = " + year);
            if (hom_qua.Rows.Count == 0)
            {
                btnDoanhSoHomQua.Text = "0";
            }
            else
            {
                btnDoanhSoHomQua.Text = hom_qua.Rows[0][0].ToString();
            }

            //Doanh thu tháng này
            int month_2 = d1.Month;
            int year_2 = d1.Year;

            DataTable thang_nay = dtBase.DataReader("select isnull(sum(TongTien), 0) as tt from tblDatHang where MONTH(NgayGiaoHang) = " + month_2 + " and YEAR(NgayGiaoHang) = " + year_2);
            if (thang_nay.Rows.Count == 0)
            {
                btnThangNay.Text = "0";
            }
            else
            {
                btnThangNay.Text = thang_nay.Rows[0][0].ToString();
            }

            //Doanh thu tháng trước
            int month_3 = d1.Month;
            int year_3 = d1.Year;
            if (month_3 == 1)
            {
                month_3 = 12;
                year_3 = year_3 - 1;
            }
            else
            {
                month_3 = month_3 - 1;
            }

            DataTable thang_truoc = dtBase.DataReader("select isnull(sum(TongTien), 0) as tt from tblDatHang where MONTH(NgayGiaoHang) = " + month_3 + " and YEAR(NgayGiaoHang) = " + year_3);
            if (thang_truoc.Rows.Count == 0)
            {
                btnThangTruoc.Text = "0";
            }
            else
            {
                btnThangTruoc.Text = thang_truoc.Rows[0][0].ToString();
            }

            //Doanh thu quý này
            int month_4 = d1.Month;
            int year_4 = d1.Year;

            int mon_start1 = (month_4 / 3) * 3 + 1;
            int mon_end1 = mon_start1 + 2;

            DataTable quy_nay = dtBase.DataReader("select isnull(sum(TongTien), 0) as tt from tblDatHang where MONTH(NgayGiaoHang) between " + mon_start1 + " and " + mon_end1 + " and YEAR(NgayGiaoHang) = " + year_4);
            if (quy_nay.Rows.Count == 0)
            {
                btnDoanhSoQuyNay.Text = "0";
            }
            else
            {
                btnDoanhSoQuyNay.Text = quy_nay.Rows[0][0].ToString();
            }

            //Doanh thu quý trước
            int month_5 = d1.Month;
            int year_5 = d1.Year;
            int mon_end2;
            int mon_start2;

            if (month_5 < 3)
            {
                mon_start2 = (month_5 / 3 - 1) * 3 + 1 + 12;
                mon_end2 = mon_start2 + 2;
                year = year - 1;
            }
            else
            {
                mon_start2 = (month_5 / 3 - 1) * 3 + 1;
                mon_end2 = mon_start2 + 2;
            }

            DataTable quy_truoc = dtBase.DataReader("select isnull(sum(TongTien), 0) as tt from tblDatHang where MONTH(NgayGiaoHang) between " + mon_start2 + " and " + mon_end2 + " and YEAR(NgayGiaoHang) = " + year_5);
            if (quy_truoc.Rows.Count == 0)
            {
                btnDoanhSoQuyTruoc.Text = "0";
            }
            else
            {
                btnDoanhSoQuyTruoc.Text = quy_truoc.Rows[0][0].ToString();
            }

            //Doanh thu năm nay
            int year_6 = d1.Year;

            DataTable nam_nay = dtBase.DataReader("select isnull(sum(TongTien), 0) as tt from tblDatHang where YEAR(NgayGiaoHang) = " + year_6);
            if (nam_nay.Rows.Count == 0)
            {
                btnDoanhSoNamNay.Text = "0";
            }
            else
            {
                btnDoanhSoNamNay.Text = nam_nay.Rows[0][0].ToString();
            }

            //Doanh thu năm trước
            int year_7 = d1.Year;
            year_7 = year_7 - 1;

            DataTable nam_truoc = dtBase.DataReader("select isnull(sum(TongTien), 0) as tt from tblDatHang where YEAR(NgayGiaoHang) = " + year_7);
            if (nam_truoc.Rows.Count == 0)
            {
                btnDoanhSoNamTruoc.Text = "0";
            }
            else
            {
                btnDoanhSoNamTruoc.Text = nam_truoc.Rows[0][0].ToString();
            }
        }

        private void cbxTop10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTop10.SelectedItem == "Năm nay")
            {
                chartTop10HangTheoNam();
            }
            if (cbxTop10.SelectedItem == "Quý gần nhất")
            {
                chartTop10HangTheoQuy();
            }
        }

        private void cbxKH_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxKH.SelectedItem == "Năm nay")
            {
                chartTop10KHThisYear();
            }
            if (cbxKH.SelectedItem == "Năm ngoái")
            {
                chartTop10KHLastYear();
            }
        }

        private void btnReloadTop10Hang_Click_1(object sender, EventArgs e)
        {
            if (cbxTop10.SelectedItem == "Năm nay")
            {
                chartTop10HangTheoNam();
            }
            else if (cbxTop10.SelectedItem == "Quý gần nhất")
            {
                chartTop10HangTheoQuy();
            }
        }
    }
}
