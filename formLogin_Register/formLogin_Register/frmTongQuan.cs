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
    public partial class frmTongquan : Form
    {
        DataAccess dtBase = new DataAccess();

        public frmTongquan()
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
            DataTable top10SL = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, SUBSTRING(tblHang.TenHang, 0, 11) as th, sum(soluong*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where YEAR(ngaygiaohang) = year(getdate()) group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");

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

            DataTable top10SL = dtBase.DataReader("select top 10 tblChiTietDatHang.mahang, SUBSTRING(tblHang.TenHang, 0, 11) as th, sum(soluong*Giaban) as doanhthu from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang=tblHang.MaHang join tblDatHang on tblDatHang.MaHDD = tblChiTietDatHang.MaHDD where Month(ngaygiaohang) between " + mon_start + " and " + mon_end + " and Year(ngaygiaohang) = " + year + " group by tblChiTietDatHang.MaHang, tblHang.TenHang order by doanhthu desc");

            chartTop10Hang.DataSource = top10SL;
            chartTop10Hang.Series["top10hang"].XValueMember = "th";
            chartTop10Hang.Series["top10hang"].YValueMembers = "doanhthu";

            chartTop10Hang.Series[0].ChartType = SeriesChartType.Pie;
        }

        private void chartTop10KHLastYear()
        {
            chart10KH.Series["top10kh"].Points.Clear();
            DataTable top10KH = dtBase.DataReader("select Top 10 Tenkh, sum(tongtien) as tt from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH where year(ngaygiaohang) = year(getdate()) - 1 group by TenKH order by tt desc");
            chart10KH.DataSource = top10KH;
            chart10KH.Series["top10kh"].XValueMember = "Tenkh";
            chart10KH.Series["top10kh"].YValueMembers = "tt";
            chart10KH.ChartAreas[0].AxisY.Maximum = 6000000;
            chart10KH.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
        }

        private void chartTop10KHThisYear()
        {
            chart10KH.Series["top10kh"].Points.Clear();
            DataTable top10KH = dtBase.DataReader("select Top 10 Tenkh, sum(tongtien) as tt from tblDatHang join tblKhachHang on tblDatHang.MaKH=tblKhachHang.MaKH where year(ngaygiaohang) = year(getdate()) group by TenKH order by tt desc");
            chart10KH.DataSource = top10KH;
            chart10KH.Series["top10kh"].XValueMember = "Tenkh";
            chart10KH.Series["top10kh"].YValueMembers = "tt";
            chart10KH.ChartAreas[0].AxisY.Maximum = 6000000;
            chart10KH.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
        }

        private void chartTop10Hang_Click(object sender, EventArgs e)
        {

        }

        private void frmTongquan_Load(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlTongQuan.Instance);
            ctrlTongQuan.Instance.Dock = DockStyle.Fill;
            ctrlTongQuan.Instance.BringToFront();
        }

        private void btnReloadTop10Hang_Click(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxTop10.SelectedItem == "Năm nay")
            {
                chartTop10HangTheoNam();
            }
            if(cbxTop10.SelectedItem == "Quý gần nhất")
            {
                chartTop10HangTheoQuy();
            }
        }

        private void cbxKH_SelectedIndexChanged(object sender, EventArgs e)
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

            if(day == 1)
            {
                if(month == 1)
                {
                    day = 31;
                    month = 12;
                    year = year - 1;
                }else if(month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                {
                    day = 31;
                    month = month - 1;
                }else if(month == 2 && year % 4 == 0)
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
            if(month_3 == 1)
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

        private void btnThang_Click(object sender, EventArgs e)
        {

        }

        private void tổngQuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlTongQuan.Instance);
            ctrlTongQuan.Instance.Dock = DockStyle.Fill;
            ctrlTongQuan.Instance.BringToFront();
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Controls.Add(ctrlHeThong.Instance);
            //ctrlHeThong.Instance.Dock = DockStyle.Fill;
            //ctrlHeThong.Instance.BringToFront();
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlHangHoa.Instance);
            ctrlHangHoa.Instance.Dock = DockStyle.Fill;
            ctrlHangHoa.Instance.BringToFront();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlKhachHang.Instance);
            ctrlKhachHang.Instance.Dock = DockStyle.Fill;
            ctrlKhachHang.Instance.BringToFront();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlNhanVien.Instance);
            ctrlNhanVien.Instance.Dock = DockStyle.Fill;
            ctrlNhanVien.Instance.BringToFront();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlNCC.Instance);
            ctrlNCC.Instance.Dock = DockStyle.Fill;
            ctrlNCC.Instance.BringToFront();
        }

        private void bánSỉToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlHoaDonSi.Instance);
            ctrlHoaDonSi.Instance.Dock = DockStyle.Fill;
            ctrlHoaDonSi.Instance.BringToFront();
        }

        private void danhMụcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlHangHoa.Instance);
            ctrlHangHoa.Instance.Dock = DockStyle.Fill;
            ctrlHangHoa.Instance.BringToFront();
        }

        private void kháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlKhachHang.Instance);
            ctrlKhachHang.Instance.Dock = DockStyle.Fill;
            ctrlKhachHang.Instance.BringToFront();
        }

        private void nhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(frmLogin.chucvu == "Quản lý")
            {
                this.Controls.Add(ctrlNhanVien.Instance);
                ctrlNhanVien.Instance.Dock = DockStyle.Fill;
                ctrlNhanVien.Instance.BringToFront();
            }
            else
            {
                MessageBox.Show("Quản lý mới có quyền truy cập mục này");
            }
            
        }

        private void nhàCungCấpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlNCC.Instance);
            ctrlNCC.Instance.Dock = DockStyle.Fill;
            ctrlNCC.Instance.BringToFront();
        }

        private void frmTongquan_MouseHover(object sender, EventArgs e)
        {

        }

        private void thôngTinCôngTyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLogin.chucvu == "Quản lý")
            {
                frmThongTinCongTy t = new frmThongTinCongTy();
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Quản lý mới có quyền truy cập mục này");
            }
            
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bánLẻToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlHDLe.Instance);
            ctrlHDLe.Instance.Dock = DockStyle.Fill;
            ctrlHDLe.Instance.BringToFront();
        }

        private void nhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlNhapHang.Instance);
            ctrlNhapHang.Instance.Dock = DockStyle.Fill;
            ctrlNhapHang.Instance.BringToFront();
        }

        private void cấpTàiKhoảnChoNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmLogin.chucvu == "Quản lý")
            {
                frmCapTaiKhoanNhanVien t = new frmCapTaiKhoanNhanVien();
                t.ShowDialog();
            }
            else
            {
                MessageBox.Show("Quản lý mới có quyền truy cập mục này");
            }
        }

        private void nhómHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhomHangHoa test = new frmNhomHangHoa();
            test.ShowDialog();
        }

        private void nhómKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhomKhachHang test = new frmNhomKhachHang();
            test.ShowDialog();
        }

        private void báoCáoChiTiếtHàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlBaoCaoChiTietHangHoa.Instance);
            ctrlBaoCaoChiTietHangHoa.Instance.Dock = DockStyle.Fill;
            ctrlBaoCaoChiTietHangHoa.Instance.BringToFront();
        }

        private void côngNợPhảiThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlKhachHang.Instance);
            ctrlKhachHang.Instance.Dock = DockStyle.Fill;
            ctrlKhachHang.Instance.BringToFront();
        }

        private void côngNợPhảiTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlNCC.Instance);
            ctrlNCC.Instance.Dock = DockStyle.Fill;
            ctrlNCC.Instance.BringToFront();
        }

        private void doanhThuTheoNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlDoanhSoTheoNhanVien.Instance);
            ctrlDoanhSoTheoNhanVien.Instance.Dock = DockStyle.Fill;
            ctrlDoanhSoTheoNhanVien.Instance.BringToFront();
        }

        private void topMặtHàngBánChạyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlTopMatHangBanChay.Instance);
            ctrlTopMatHangBanChay.Instance.Dock = DockStyle.Fill;
            ctrlTopMatHangBanChay.Instance.BringToFront();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void topDoanhSốKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Add(ctrlTopDoanhSoKhachHang.Instance);
            ctrlTopDoanhSoKhachHang.Instance.Dock = DockStyle.Fill;
            ctrlTopDoanhSoKhachHang.Instance.BringToFront();
        }
    }
}
