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
    public partial class ctrlBaoCaoChiTietHangHoa : UserControl
    {
        private static ctrlBaoCaoChiTietHangHoa _instance;

        DataAccess dtBase = new DataAccess();

        public static ctrlBaoCaoChiTietHangHoa Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlBaoCaoChiTietHangHoa();
                return _instance;
            }
        }
        public ctrlBaoCaoChiTietHangHoa()
        {
            InitializeComponent();
            load_();
            ToanKy();
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
        private void ctrlBaoCaoChiTietHangHoa_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.textBox1_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.textBox1_Enter);

            cbx1.Items.Add("Toàn kỳ");
            cbx1.Items.Add("Năm nay");
            cbx1.Items.Add("Tháng này");

            cbx1.Text = "Toàn kỳ";
            ToanKy();
            
            load_();
        }

        private void ToanKy()
        {
            DataTable dtHang = dtBase.DataReader("select tblHang.MaHang, tblHang.TenHang, SoLuongTon as SoluongCon, a.tongnhap as SLnhap, d.banbuon as SLBanBuon, isnull(c.banle,0) as SLBanLe, (d.banbuon + isnull(c.banle,0)) * (tblHang.GiaBan - tblhang.GiaNhap) as LoiNhuan, (a.tongnhap * tblHang.GiaNhap) as TongNhap, (d.banbuon + isnull(c.banle,0)) * tblHang.GiaBan as TongBan from tblHang join (select tblChiTietNhapKho.MaHang, TenHang, sum(soluong) as tongnhap from tblChiTietNhapKho join tblHang on tblChiTietNhapKho.MaHang = tblhang.MaHang group by tblChiTietNhapKho.MaHang, TenHang) a on tblHang.MaHang = a.MaHang left join(select tblChiTietHDLe.MaHang, TenHang, isnull(sum(soluong), 0) as banle from tblChiTietHDLe join tblHang on tblChiTietHDLe.MaHang = tblhang.MaHang group by tblChiTietHDLe.MaHang, TenHang) c on tblHang.MaHang = c.MaHang join(select tblChiTietDatHang.MaHang, TenHang, sum(convert(int, tblChiTietDatHang.soluong)) as banbuon from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang = tblhang.MaHang group by tblChiTietDatHang.MaHang, TenHang) d on tblHang.MaHang = d.MaHang");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[1].Width = 150;

            dgvHang.Columns[2].Width = 50;
            dgvHang.Columns[3].Width = 50;
            dgvHang.Columns[4].Width = 50;
            dgvHang.Columns[5].Width = 50;

            dgvHang.Columns[6].Width = 150;
            dgvHang.Columns[7].Width = 150;
            dgvHang.Columns[8].Width = 150;


            dgvHang.Columns[0].HeaderText = "Mã Hàng";
            dgvHang.Columns[1].HeaderText = "Tên Hàng";
            dgvHang.Columns[2].HeaderText = "SL còn";
            dgvHang.Columns[3].HeaderText = "SL Nhập";
            dgvHang.Columns[4].HeaderText = "SL Bán buôn";
            dgvHang.Columns[5].HeaderText = "SL Bán lẻ";
            dgvHang.Columns[6].HeaderText = "Lợi nhuận";
            dgvHang.Columns[7].HeaderText = "Tổng tiền nhập";
            dgvHang.Columns[8].HeaderText = "Tổng tiền bán";

            int loinhuan = 0;
            int tongnhap = 0;
            int tongban = 0;

            for (int i = 0; i < dgvHang.RowCount-1; i++)
            {
                loinhuan += int.Parse(dgvHang.Rows[i].Cells[6].Value.ToString().Trim());
                tongnhap += int.Parse(dgvHang.Rows[i].Cells[7].Value.ToString().Trim());
                tongban += int.Parse(dgvHang.Rows[i].Cells[8].Value.ToString().Trim());
            }

            btnLoiNhuan.Text = loinhuan.ToString();
            btnTongTienNhap.Text = tongnhap.ToString();
            btnTongTienBan.Text = tongban.ToString();
        }

        private void NamNay()
        {
            DataTable dtHang = dtBase.DataReader("select tblHang.MaHang, tblHang.TenHang, SoLuongTon as SoluongCon, a.tongnhap as SLnhap, d.banbuon as SLBanBuon, isnull(c.banle,0) as SLBanLe, (d.banbuon + isnull(c.banle,0)) * (tblHang.GiaBan - tblhang.GiaNhap) as LoiNhuan, (a.tongnhap * tblHang.GiaNhap) as TongNhap, (d.banbuon + isnull(c.banle,0)) * tblHang.GiaBan as TongBan from tblHang join (select tblChiTietNhapKho.MaHang, TenHang, sum(soluong) as tongnhap from tblChiTietNhapKho join tblHang on tblChiTietNhapKho.MaHang = tblhang.MaHang join tblNhapKho on tblChiTietNhapKho.MaHDN = tblNhapKho.MaHDN where year(NgayNhap) = year(getdate()) group by tblChiTietNhapKho.MaHang, TenHang) a on tblHang.MaHang = a.MaHang left join(select tblChiTietHDLe.MaHang, TenHang, isnull(sum(soluong), 0) as banle from tblChiTietHDLe join tblHang on tblChiTietHDLe.MaHang = tblhang.MaHang join tblHDLe on tblChiTietHDLe.MaHDL = tblHDLe.MaHDL where year(NgayDat) = year(getdate()) group by tblChiTietHDLe.MaHang, TenHang) c on tblHang.MaHang = c.MaHang join(select tblChiTietDatHang.MaHang, TenHang, sum(convert(int, tblChiTietDatHang.soluong)) as banbuon from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang = tblhang.MaHang join tblDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD where year(NgayDatHang) = year(getdate()) group by tblChiTietDatHang.MaHang, TenHang) d on tblHang.MaHang = d.MaHang");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[1].Width = 150;

            dgvHang.Columns[2].Width = 50;
            dgvHang.Columns[3].Width = 50;
            dgvHang.Columns[4].Width = 50;
            dgvHang.Columns[5].Width = 50;

            dgvHang.Columns[6].Width = 150;
            dgvHang.Columns[7].Width = 150;
            dgvHang.Columns[8].Width = 150;


            dgvHang.Columns[0].HeaderText = "Mã Hàng";
            dgvHang.Columns[1].HeaderText = "Tên Hàng";
            dgvHang.Columns[2].HeaderText = "SL còn";
            dgvHang.Columns[3].HeaderText = "SL Nhập";
            dgvHang.Columns[4].HeaderText = "SL Bán buôn";
            dgvHang.Columns[5].HeaderText = "SL Bán lẻ";
            dgvHang.Columns[6].HeaderText = "Lợi nhuận";
            dgvHang.Columns[7].HeaderText = "Tổng tiền nhập";
            dgvHang.Columns[8].HeaderText = "Tổng tiền bán";

            int loinhuan = 0;
            int tongnhap = 0;
            int tongban = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                loinhuan += int.Parse(dgvHang.Rows[i].Cells[6].Value.ToString().Trim());
                tongnhap += int.Parse(dgvHang.Rows[i].Cells[7].Value.ToString().Trim());
                tongban += int.Parse(dgvHang.Rows[i].Cells[8].Value.ToString().Trim());
            }

            btnLoiNhuan.Text = loinhuan.ToString();
            btnTongTienNhap.Text = tongnhap.ToString();
            btnTongTienBan.Text = tongban.ToString();
        }

        private void ThangNay()
        {
            DataTable dtHang = dtBase.DataReader("select tblHang.MaHang, tblHang.TenHang, SoLuongTon as SoluongCon, a.tongnhap as SLnhap, d.banbuon as SLBanBuon, isnull(c.banle,0) as SLBanLe, (d.banbuon + isnull(c.banle,0)) * (tblHang.GiaBan - tblhang.GiaNhap) as LoiNhuan, (a.tongnhap * tblHang.GiaNhap) as TongNhap, (d.banbuon + isnull(c.banle,0)) * tblHang.GiaBan as TongBan from tblHang join (select tblChiTietNhapKho.MaHang, TenHang, sum(soluong) as tongnhap from tblChiTietNhapKho join tblHang on tblChiTietNhapKho.MaHang = tblhang.MaHang join tblNhapKho on tblChiTietNhapKho.MaHDN = tblNhapKho.MaHDN where month(NgayNhap) = month(getdate()) group by tblChiTietNhapKho.MaHang, TenHang) a on tblHang.MaHang = a.MaHang left join(select tblChiTietHDLe.MaHang, TenHang, isnull(sum(soluong), 0) as banle from tblChiTietHDLe join tblHang on tblChiTietHDLe.MaHang = tblhang.MaHang join tblHDLe on tblChiTietHDLe.MaHDL = tblHDLe.MaHDL where month(NgayDat) = month(getdate()) group by tblChiTietHDLe.MaHang, TenHang) c on tblHang.MaHang = c.MaHang join(select tblChiTietDatHang.MaHang, TenHang, sum(convert(int, tblChiTietDatHang.soluong)) as banbuon from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang = tblhang.MaHang join tblDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD where month(NgayDatHang) = month(getdate()) group by tblChiTietDatHang.MaHang, TenHang) d on tblHang.MaHang = d.MaHang");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvHang.Columns[1].Width = 150;

            dgvHang.Columns[2].Width = 50;
            dgvHang.Columns[3].Width = 50;
            dgvHang.Columns[4].Width = 50;
            dgvHang.Columns[5].Width = 50;

            dgvHang.Columns[6].Width = 150;
            dgvHang.Columns[7].Width = 150;
            dgvHang.Columns[8].Width = 150;


            dgvHang.Columns[0].HeaderText = "Mã Hàng";
            dgvHang.Columns[1].HeaderText = "Tên Hàng";
            dgvHang.Columns[2].HeaderText = "SL còn";
            dgvHang.Columns[3].HeaderText = "SL Nhập";
            dgvHang.Columns[4].HeaderText = "SL Bán buôn";
            dgvHang.Columns[5].HeaderText = "SL Bán lẻ";
            dgvHang.Columns[6].HeaderText = "Lợi nhuận";
            dgvHang.Columns[7].HeaderText = "Tổng tiền nhập";
            dgvHang.Columns[8].HeaderText = "Tổng tiền bán";

            int loinhuan = 0;
            int tongnhap = 0;
            int tongban = 0;

            for (int i = 0; i < dgvHang.RowCount - 1; i++)
            {
                loinhuan += int.Parse(dgvHang.Rows[i].Cells[6].Value.ToString().Trim());
                tongnhap += int.Parse(dgvHang.Rows[i].Cells[7].Value.ToString().Trim());
                tongban += int.Parse(dgvHang.Rows[i].Cells[8].Value.ToString().Trim());
            }

            btnLoiNhuan.Text = loinhuan.ToString();
            btnTongTienNhap.Text = tongnhap.ToString();
            btnTongTienBan.Text = tongban.ToString();
        }

        private void load_()
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

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Tìm kiếm")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Tìm kiếm")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
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
                if (cbx1.Text == "Toàn kỳ")
                {
                    DataTable dtHang = dtBase.DataReader("select tblHang.MaHang, tblHang.TenHang, SoLuongTon as SoluongCon, a.tongnhap as SLnhap, d.banbuon as SLBanBuon, isnull(c.banle,0) as SLBanLe, (d.banbuon + isnull(c.banle,0)) * (tblHang.GiaBan - tblhang.GiaNhap) as LoiNhuan, (a.tongnhap * tblHang.GiaNhap) as TongNhap, (d.banbuon + isnull(c.banle,0)) * tblHang.GiaBan as TongBan from tblHang join (select tblChiTietNhapKho.MaHang, TenHang, sum(soluong) as tongnhap from tblChiTietNhapKho join tblHang on tblChiTietNhapKho.MaHang = tblhang.MaHang group by tblChiTietNhapKho.MaHang, TenHang) a on tblHang.MaHang = a.MaHang left join(select tblChiTietHDLe.MaHang, TenHang, isnull(sum(soluong), 0) as banle from tblChiTietHDLe join tblHang on tblChiTietHDLe.MaHang = tblhang.MaHang group by tblChiTietHDLe.MaHang, TenHang) c on tblHang.MaHang = c.MaHang join(select tblChiTietDatHang.MaHang, TenHang, sum(convert(int, tblChiTietDatHang.soluong)) as banbuon from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang = tblhang.MaHang group by tblChiTietDatHang.MaHang, TenHang) d on tblHang.MaHang = d.MaHang where tblHang.MaHang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%'");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[1].Width = 150;

                    dgvHang.Columns[2].Width = 50;
                    dgvHang.Columns[3].Width = 50;
                    dgvHang.Columns[4].Width = 50;
                    dgvHang.Columns[5].Width = 50;

                    dgvHang.Columns[6].Width = 150;
                    dgvHang.Columns[7].Width = 150;
                    dgvHang.Columns[8].Width = 150;


                    dgvHang.Columns[0].HeaderText = "Mã Hàng";
                    dgvHang.Columns[1].HeaderText = "Tên Hàng";
                    dgvHang.Columns[2].HeaderText = "SL còn";
                    dgvHang.Columns[3].HeaderText = "SL Nhập";
                    dgvHang.Columns[4].HeaderText = "SL Bán buôn";
                    dgvHang.Columns[5].HeaderText = "SL Bán lẻ";
                    dgvHang.Columns[6].HeaderText = "Lợi nhuận";
                    dgvHang.Columns[7].HeaderText = "Tổng tiền nhập";
                    dgvHang.Columns[8].HeaderText = "Tổng tiền bán";

                    int loinhuan = 0;
                    int tongnhap = 0;
                    int tongban = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        loinhuan += int.Parse(dgvHang.Rows[i].Cells[6].Value.ToString().Trim());
                        tongnhap += int.Parse(dgvHang.Rows[i].Cells[7].Value.ToString().Trim());
                        tongban += int.Parse(dgvHang.Rows[i].Cells[8].Value.ToString().Trim());
                    }

                    btnLoiNhuan.Text = loinhuan.ToString();
                    btnTongTienNhap.Text = tongnhap.ToString();
                    btnTongTienBan.Text = tongban.ToString();
                }
                else if (cbx1.Text == "Năm nay")
                {
                    DataTable dtHang = dtBase.DataReader("select tblHang.MaHang, tblHang.TenHang, SoLuongTon as SoluongCon, a.tongnhap as SLnhap, d.banbuon as SLBanBuon, isnull(c.banle,0) as SLBanLe, (d.banbuon + isnull(c.banle,0)) * (tblHang.GiaBan - tblhang.GiaNhap) as LoiNhuan, (a.tongnhap * tblHang.GiaNhap) as TongNhap, (d.banbuon + isnull(c.banle,0)) * tblHang.GiaBan as TongBan from tblHang join (select tblChiTietNhapKho.MaHang, TenHang, sum(soluong) as tongnhap from tblChiTietNhapKho join tblHang on tblChiTietNhapKho.MaHang = tblhang.MaHang join tblNhapKho on tblChiTietNhapKho.MaHDN = tblNhapKho.MaHDN where year(NgayNhap) = year(getdate()) group by tblChiTietNhapKho.MaHang, TenHang) a on tblHang.MaHang = a.MaHang left join(select tblChiTietHDLe.MaHang, TenHang, isnull(sum(soluong), 0) as banle from tblChiTietHDLe join tblHang on tblChiTietHDLe.MaHang = tblhang.MaHang join tblHDLe on tblChiTietHDLe.MaHDL = tblHDLe.MaHDL where year(NgayDat) = year(getdate()) group by tblChiTietHDLe.MaHang, TenHang) c on tblHang.MaHang = c.MaHang join(select tblChiTietDatHang.MaHang, TenHang, sum(convert(int, tblChiTietDatHang.soluong)) as banbuon from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang = tblhang.MaHang join tblDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD where year(NgayDatHang) = year(getdate()) group by tblChiTietDatHang.MaHang, TenHang) d on tblHang.MaHang = d.MaHang where tblHang.MaHang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%'");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[1].Width = 150;

                    dgvHang.Columns[2].Width = 50;
                    dgvHang.Columns[3].Width = 50;
                    dgvHang.Columns[4].Width = 50;
                    dgvHang.Columns[5].Width = 50;

                    dgvHang.Columns[6].Width = 150;
                    dgvHang.Columns[7].Width = 150;
                    dgvHang.Columns[8].Width = 150;


                    dgvHang.Columns[0].HeaderText = "Mã Hàng";
                    dgvHang.Columns[1].HeaderText = "Tên Hàng";
                    dgvHang.Columns[2].HeaderText = "SL còn";
                    dgvHang.Columns[3].HeaderText = "SL Nhập";
                    dgvHang.Columns[4].HeaderText = "SL Bán buôn";
                    dgvHang.Columns[5].HeaderText = "SL Bán lẻ";
                    dgvHang.Columns[6].HeaderText = "Lợi nhuận";
                    dgvHang.Columns[7].HeaderText = "Tổng tiền nhập";
                    dgvHang.Columns[8].HeaderText = "Tổng tiền bán";

                    int loinhuan = 0;
                    int tongnhap = 0;
                    int tongban = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        loinhuan += int.Parse(dgvHang.Rows[i].Cells[6].Value.ToString().Trim());
                        tongnhap += int.Parse(dgvHang.Rows[i].Cells[7].Value.ToString().Trim());
                        tongban += int.Parse(dgvHang.Rows[i].Cells[8].Value.ToString().Trim());
                    }

                    btnLoiNhuan.Text = loinhuan.ToString();
                    btnTongTienNhap.Text = tongnhap.ToString();
                    btnTongTienBan.Text = tongban.ToString();
                }
                else
                {
                    DataTable dtHang = dtBase.DataReader("select tblHang.MaHang, tblHang.TenHang, SoLuongTon as SoluongCon, a.tongnhap as SLnhap, d.banbuon as SLBanBuon, isnull(c.banle,0) as SLBanLe, (d.banbuon + isnull(c.banle,0)) * (tblHang.GiaBan - tblhang.GiaNhap) as LoiNhuan, (a.tongnhap * tblHang.GiaNhap) as TongNhap, (d.banbuon + isnull(c.banle,0)) * tblHang.GiaBan as TongBan from tblHang join (select tblChiTietNhapKho.MaHang, TenHang, sum(soluong) as tongnhap from tblChiTietNhapKho join tblHang on tblChiTietNhapKho.MaHang = tblhang.MaHang join tblNhapKho on tblChiTietNhapKho.MaHDN = tblNhapKho.MaHDN where month(NgayNhap) = month(getdate()) group by tblChiTietNhapKho.MaHang, TenHang) a on tblHang.MaHang = a.MaHang left join(select tblChiTietHDLe.MaHang, TenHang, isnull(sum(soluong), 0) as banle from tblChiTietHDLe join tblHang on tblChiTietHDLe.MaHang = tblhang.MaHang join tblHDLe on tblChiTietHDLe.MaHDL = tblHDLe.MaHDL where month(NgayDat) = month(getdate()) group by tblChiTietHDLe.MaHang, TenHang) c on tblHang.MaHang = c.MaHang join(select tblChiTietDatHang.MaHang, TenHang, sum(convert(int, tblChiTietDatHang.soluong)) as banbuon from tblChiTietDatHang join tblHang on tblChiTietDatHang.MaHang = tblhang.MaHang join tblDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD where month(NgayDatHang) = month(getdate()) group by tblChiTietDatHang.MaHang, TenHang) d on tblHang.MaHang = d.MaHang where tblHang.MaHang like '%" + txtTimKiem.Text.Trim() + "%' or tblHang.TenHang like '%" + txtTimKiem.Text.Trim() + "%'");
                    dgvHang.DataSource = dtHang;
                    for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                    {
                        dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    }

                    dgvHang.Columns[1].Width = 150;

                    dgvHang.Columns[2].Width = 50;
                    dgvHang.Columns[3].Width = 50;
                    dgvHang.Columns[4].Width = 50;
                    dgvHang.Columns[5].Width = 50;

                    dgvHang.Columns[6].Width = 150;
                    dgvHang.Columns[7].Width = 150;
                    dgvHang.Columns[8].Width = 150;


                    dgvHang.Columns[0].HeaderText = "Mã Hàng";
                    dgvHang.Columns[1].HeaderText = "Tên Hàng";
                    dgvHang.Columns[2].HeaderText = "SL còn";
                    dgvHang.Columns[3].HeaderText = "SL Nhập";
                    dgvHang.Columns[4].HeaderText = "SL Bán buôn";
                    dgvHang.Columns[5].HeaderText = "SL Bán lẻ";
                    dgvHang.Columns[6].HeaderText = "Lợi nhuận";
                    dgvHang.Columns[7].HeaderText = "Tổng tiền nhập";
                    dgvHang.Columns[8].HeaderText = "Tổng tiền bán";

                    int loinhuan = 0;
                    int tongnhap = 0;
                    int tongban = 0;

                    for (int i = 0; i < dgvHang.RowCount - 1; i++)
                    {
                        loinhuan += int.Parse(dgvHang.Rows[i].Cells[6].Value.ToString().Trim());
                        tongnhap += int.Parse(dgvHang.Rows[i].Cells[7].Value.ToString().Trim());
                        tongban += int.Parse(dgvHang.Rows[i].Cells[8].Value.ToString().Trim());
                    }

                    btnLoiNhuan.Text = loinhuan.ToString();
                    btnTongTienNhap.Text = tongnhap.ToString();
                    btnTongTienBan.Text = tongban.ToString();
                }
            }
        }

        private void cbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbx1.Text == "Toàn kỳ")
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

        private void button1_Click(object sender, EventArgs e)
        {
            load_();
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
                header.Value = "Báo cáo chi tiết mặt hàng";

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
                exSheet.get_Range("B9").Value = "Mã hàng";
                exSheet.get_Range("C9").Value = "Tên hàng";
                exSheet.get_Range("C9").ColumnWidth = 30;

                exSheet.get_Range("D9").Value = "SL còn";
                exSheet.get_Range("E9").Value = "SL nhập";
                exSheet.get_Range("F9").Value = "Sl Bán buôn";
                exSheet.get_Range("G9").Value = "SL Bán lẻ";
                exSheet.get_Range("H9").Value = "Lợi nhuận";
                exSheet.get_Range("I9").Value = "Tồng tiền nhập";
                exSheet.get_Range("J9").Value = "Tổng tiền bán";

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
                loinhuan.Value = "Lợi nhuận: " + btnLoiNhuan.Text;

                Excel.Range tongnhap = (Excel.Range)exSheet.Cells[dgvHang.Rows.Count + 11, 2];
                exSheet.get_Range("B" + (dgvHang.Rows.Count + 11).ToString() + ":E" + (dgvHang.Rows.Count + 11).ToString()).Merge(true);
                tongnhap.Font.Bold = true;
                tongnhap.Value = "Tổng tiền nhập: " + btnTongTienNhap.Text;

                Excel.Range tongban = (Excel.Range)exSheet.Cells[dgvHang.Rows.Count + 12, 2];
                exSheet.get_Range("B" + (dgvHang.Rows.Count + 12).ToString() + ":E" + (dgvHang.Rows.Count + 12).ToString()).Merge(true);
                tongban.Font.Bold = true;
                tongban.Value = "Tổng tiền bán: " + btnTongTienBan.Text;

                exSheet.Name = "Hang";

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
