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
    public partial class frmThem_HD_Si : Form
    {

        DataAccess dtBase = new DataAccess();

        public frmThem_HD_Si()
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
        private void frmThem_HD_Si_Load(object sender, EventArgs e)
        {
            cbxTime.Format = DateTimePickerFormat.Short;

            txtTimKiem.Text = "Tìm kiếm";
            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);

            cbxKhachHang.DataSource = dtBase.DataReader("Select * from tblKhachHang");
            cbxKhachHang.DisplayMember = "TenKh";
            cbxKhachHang.ValueMember = "MaKH";
            cbxKhachHang.Text = "";

            cbxNhanVien.Text = frmLogin.tendangnhap;

            dtBase.UpdateData("delete from DsHangDaChon where exists(select * from dshangdachon)");

            txtTiGia.Text = "1";
            txtVAT.Text = "0";
            txtTienVAT.Text = "0";
            txtBotTien.Text = "0";
            txtTraNgay.Text = "0";
            txtNoCu.Text = "0";

            load_();
        }

        private void resetData()
        {
            cbxKhachHang.Text = "";
            cbxNhanVien.Text = "";

            txtTiGia.Text = "1";
            txtTienHang.Text = "0";
            txtVAT.Text = "0";
            txtTienVAT.Text = "0";
            txtTongTien.Text = "0";
            txtBotTien.Text = "0";

            txtPhaiThu.Text = "0";
            txtTraNgay.Text = "0";
            txtNoLai.Text = "0";
            txtNoCu.Text = "0";
            txtTongNo.Text = "0";
        }

        private void load_()
        {

            DataTable dtHang = dtBase.DataReader("select MaHang, TenHang, SoLuongTon, GiaBan, DonViTinh from tblHang");
            dgvDSHang.DataSource = dtHang;

            for (int i = 0; i < dgvDSHang.RowCount; i = i + 2)
            {
                dgvDSHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvDSHang.Columns[0].Width = 70;
            dgvDSHang.Columns[1].Width = 150;
            dgvDSHang.Columns[2].Width = 70;
            dgvDSHang.Columns[3].Width = 70;
            dgvDSHang.Columns[4].Width = 70;

            dgvDSHang.Columns[0].HeaderText = "Mã Hàng";
            dgvDSHang.Columns[1].HeaderText = "Tên Hàng";
            dgvDSHang.Columns[2].HeaderText = "Số lượng";
            dgvDSHang.Columns[3].HeaderText = "Giá bán";
            dgvDSHang.Columns[4].HeaderText = "Đơn vị";

            txtTenHang.Text = "";
            txtSoLuong.Text = "";

            DataTable dtDSHangDaChon = dtBase.DataReader("select * from DsHangDaChon");
            dgvDSHangDaChon.DataSource = dtDSHangDaChon;

            for (int i = 0; i < dgvDSHangDaChon.RowCount; i = i + 2)
            {
                dgvDSHangDaChon.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            dgvDSHangDaChon.Columns[0].HeaderText = "Mã Hàng";
            dgvDSHangDaChon.Columns[1].HeaderText = "Tên hàng";
            dgvDSHangDaChon.Columns[2].HeaderText = "Giá bán";
            dgvDSHangDaChon.Columns[3].HeaderText = "Số lượng";
            dgvDSHangDaChon.Columns[4].HeaderText = "Thành tiền";

            dgvDSHangDaChon.Columns[1].Width = 200;
            dgvDSHangDaChon.Columns[2].Width = 150;
            dgvDSHangDaChon.Columns[3].Width = 150;
            dgvDSHangDaChon.Columns[4].Width = 200;

            try
            {
                Double tien_hang = 0;
                for(int i=0; i<dgvDSHangDaChon.Rows.Count - 1; i++)
                {
                    tien_hang += Double.Parse(dgvDSHangDaChon.Rows[i].Cells[4].Value.ToString().Trim());
                }
                tien_hang = tien_hang * Double.Parse(txtTiGia.Text);
                txtTienHang.Text = tien_hang.ToString();
                txtTongTien.Text = (tien_hang + Int32.Parse(txtTienVAT.Text)).ToString();
                txtPhaiThu.Text = (Int32.Parse(txtTongTien.Text) - Int32.Parse(txtBotTien.Text)).ToString();
                txtNoLai.Text = (Int32.Parse(txtPhaiThu.Text) - Int32.Parse(txtTraNgay.Text)).ToString();
                txtTongNo.Text = (Int32.Parse(txtNoLai.Text) + Int32.Parse(txtNoCu.Text)).ToString();
            }
            catch
            {

            }
        }

        private void ReloadDSHang()
        {
            
        }

        private void dgvDSHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            txtTenHang.Text = dgvDSHang.Rows[numrow].Cells[1].Value.ToString().Trim();
            
        }

        private void txtTenHang_TextChanged(object sender, EventArgs e)
        {

            //DataTable dtHang = dtBase.DataReader("select MaHang, TenHang, SoLuongTon, GiaBan, DonViTinh from tblHang where TenHang like N'%" + txtTenHang.Text + "%' or MaHang like N'%" + txtTenHang.Text + "%'" );
            //dgvDSHang.DataSource = dtHang;

            //for (int i = 0; i < dgvDSHang.RowCount; i = i + 2)
            //{
            //    dgvDSHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            //}
            //dgvDSHang.Columns[0].Width = 70;
            //dgvDSHang.Columns[1].Width = 150;
            //dgvDSHang.Columns[2].Width = 70;
            //dgvDSHang.Columns[3].Width = 70;
            //dgvDSHang.Columns[4].Width = 70;

            //dgvDSHang.Columns[0].HeaderText = "Mã Hàng";
            //dgvDSHang.Columns[1].HeaderText = "Tên Hàng";
            //dgvDSHang.Columns[2].HeaderText = "Số lượng";
            //dgvDSHang.Columns[3].HeaderText = "Giá bán";
            //dgvDSHang.Columns[4].HeaderText = "Đơn vị";

            //cbxKhachHang.DataSource = dtBase.DataReader("Select * from tblKhachHang");
            //cbxKhachHang.DisplayMember = "TenKh";
            //cbxKhachHang.Text = "";

            //cbxNhanVien.DataSource = dtBase.DataReader("Select * from tblNhanVien");
            //cbxNhanVien.DisplayMember = "TenNV";
            //cbxNhanVien.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtTenHang.Text.Trim() == "" || txtSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng kiểm tra lại Tên hàng hoặc số lượng");
                return;
            }

            DataTable temp = dtBase.DataReader("Select Soluongton from tblHang where TenHang = N'" + txtTenHang.Text.Trim() + "'");

            if (txtSoLuong.Text.Trim() != "" && Int32.Parse(txtSoLuong.Text.Trim()) > Int32.Parse(temp.Rows[0].ItemArray[0].ToString().Trim()))
            {
                MessageBox.Show("Số lượng hiện tại vượt quá số lượng tồn trong kho");
            }
            else
            {
                temp = dtBase.DataReader("Select MaHang, GiaBan from tblHang where TenHang = N'" + txtTenHang.Text.Trim() + "'");
                int giaban = Int32.Parse(temp.Rows[0].ItemArray[1].ToString().Trim());
                int thanhtien = giaban * Int32.Parse(txtSoLuong.Text.Trim());
                try
                {
                    dtBase.UpdateData("insert into DSHangDaChon(MaHang, TenHang, GiaBan, SoLuong, ThanhTien) values('" + temp.Rows[0].ItemArray[0].ToString().Trim() + "', N'" + txtTenHang.Text.Trim() + "', '" + temp.Rows[0].ItemArray[1].ToString().Trim() + "', '" + txtSoLuong.Text.Trim() + "', '" + thanhtien + "')");
                }
                catch
                {
                    dtBase.UpdateData("update DSHangDaChon set SoLuong = SoLuong + " + Int32.Parse(txtSoLuong.Text.Trim()) + " where MaHang = '" + temp.Rows[0].ItemArray[0].ToString().Trim() + "'");
                    dtBase.UpdateData("update DSHangDaChon set ThanhTien = ThanhTien + " + thanhtien + " where MaHang = '" + temp.Rows[0].ItemArray[0].ToString().Trim() + "'");
                }

                dtBase.UpdateData("update tblHang set SoLuongTon = SoLuongTon - " + Int32.Parse(txtSoLuong.Text) + " where TenHang = N'" + txtTenHang.Text.Trim() + "'");

                load_();
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

        private void txtTimKiem_Enter(object sender, EventArgs e)
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
                load_();
            }
            else if (txtTimKiem.Text != "Tìm kiếm")
            {
                DataTable dtHang = dtBase.DataReader("select MaHang, TenHang, SoLuongTon, GiaBan, DonViTinh from tblHang where MaHang like N'%" + txtTimKiem.Text + "%' or TenHang like N'%" + txtTimKiem.Text + "%'");
                dgvDSHang.DataSource = dtHang;

                for (int i = 0; i < dgvDSHang.RowCount; i = i + 2)
                {
                    dgvDSHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                dgvDSHang.Columns[0].Width = 70;
                dgvDSHang.Columns[1].Width = 150;
                dgvDSHang.Columns[2].Width = 70;
                dgvDSHang.Columns[3].Width = 70;
                dgvDSHang.Columns[4].Width = 70;

                dgvDSHang.Columns[0].HeaderText = "Mã Hàng";
                dgvDSHang.Columns[1].HeaderText = "Tên Hàng";
                dgvDSHang.Columns[2].HeaderText = "Số lượng";
                dgvDSHang.Columns[3].HeaderText = "Giá bán";
                dgvDSHang.Columns[4].HeaderText = "Đơn vị";

                //cbxKhachHang.DataSource = dtBase.DataReader("Select * from tblKhachHang");
                //cbxKhachHang.DisplayMember = "TenKh";
                //cbxKhachHang.ValueMember = "MaKH";
                //cbxKhachHang.Text = "";

                //cbxNhanVien.DataSource = dtBase.DataReader("Select * from tblNhanVien");
                //cbxNhanVien.DisplayMember = "TenNV";
                //cbxNhanVien.ValueMember = "MaNV";
                //cbxNhanVien.Text = "";

                //DataTable dtDSHangDaChon = dtBase.DataReader("select * from DsHangDaChon");
                //dgvDSHangDaChon.DataSource = dtDSHangDaChon;

                //for (int i = 0; i < dgvDSHangDaChon.RowCount; i = i + 2)
                //{
                //    dgvDSHangDaChon.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                //}
                //dgvDSHangDaChon.Columns[0].HeaderText = "Mã Hàng";
                //dgvDSHangDaChon.Columns[1].HeaderText = "Tên hàng";
                //dgvDSHangDaChon.Columns[2].HeaderText = "Giá bán";
                //dgvDSHangDaChon.Columns[3].HeaderText = "Số lượng";
                //dgvDSHangDaChon.Columns[4].HeaderText = "Thành tiền";

                //dgvDSHangDaChon.Columns[1].Width = 200;
                //dgvDSHangDaChon.Columns[2].Width = 150;
                //dgvDSHangDaChon.Columns[3].Width = 150;
                //dgvDSHangDaChon.Columns[4].Width = 200;

                txtTenHang.Text = "";
                txtSoLuong.Text = "";
            }
        }

        private void cbxKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable temp = dtBase.DataReader("select isnull(TienNo, 0) from tblKhachHang where MaKH = '" + cbxKhachHang.SelectedValue.ToString() + "'");
            try
            {
                txtNoCu.Text = temp.Rows[0].ItemArray[0].ToString().Trim();
                txtTongNo.Text = (Int32.Parse(txtNoCu.Text) + Int32.Parse(txtNoLai.Text)).ToString();
            }
            catch
            {

            }
        }

        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            int tien_hang = Int32.Parse(txtTienHang.Text);

            int tienVAT = 0;

            try
            {
                tienVAT = tien_hang * Int32.Parse(txtVAT.Text.Trim()) / 100;
                txtTienVAT.Text = tienVAT.ToString();
                txtTongTien.Text = (tien_hang + tienVAT).ToString();
            }
            catch
            {

            }
            try
            {
                txtTienHang.Text = tien_hang.ToString();
                txtTongTien.Text = (tien_hang + Int32.Parse(txtTienVAT.Text)).ToString();
                txtPhaiThu.Text = (Int32.Parse(txtTongTien.Text) - Int32.Parse(txtBotTien.Text)).ToString();
                txtNoLai.Text = (Int32.Parse(txtPhaiThu.Text) - Int32.Parse(txtTraNgay.Text)).ToString();
                txtTongNo.Text = (Int32.Parse(txtNoLai.Text) + Int32.Parse(txtNoCu.Text)).ToString();
            }
            catch
            {

            }
        }

        private void txtBotTien_TextChanged(object sender, EventArgs e)
        {
            int bot_tien = 0;
            try
            {
                bot_tien = Int32.Parse(txtBotTien.Text);

                if(bot_tien > Int32.Parse(txtTongTien.Text))
                {
                    MessageBox.Show("Tiền bớt đang lớn hơn Tổng tiền phải thu. Vui lòng kiểm tra lại!");
                    return;
                }
            }
            catch
            {

            }
            txtPhaiThu.Text = (Int32.Parse(txtTongTien.Text) - bot_tien).ToString();

            try
            {
                Double tien_hang = Double.Parse(txtTienHang.Text);
                
                txtTongTien.Text = (tien_hang + Int32.Parse(txtTienVAT.Text)).ToString();
                txtPhaiThu.Text = (Int32.Parse(txtTongTien.Text) - Int32.Parse(txtBotTien.Text)).ToString();
                txtNoLai.Text = (Int32.Parse(txtPhaiThu.Text) - Int32.Parse(txtTraNgay.Text)).ToString();
                txtTongNo.Text = (Int32.Parse(txtNoLai.Text) + Int32.Parse(txtNoCu.Text)).ToString();
            }
            catch
            {

            }
        }

        private void txtTraNgay_TextChanged(object sender, EventArgs e)
        {
            int tra_ngay = 0;

            try
            {
                tra_ngay = Int32.Parse(txtTraNgay.Text);
                if (tra_ngay > Int32.Parse(txtTraNgay.Text))
                {
                    MessageBox.Show("Tiền trả đang lớn hơn phải thu. Vui lòng kiểm tra lại!");
                }
            }
            catch
            {

            }
            try
            {
                
                txtPhaiThu.Text = (Int32.Parse(txtTongTien.Text) - Int32.Parse(txtBotTien.Text)).ToString();
                txtNoLai.Text = (Int32.Parse(txtPhaiThu.Text) - Int32.Parse(txtTraNgay.Text)).ToString();
                txtTongNo.Text = (Int32.Parse(txtNoLai.Text) + Int32.Parse(txtNoCu.Text)).ToString();
            }
            catch
            {

            }
            //txtNoLai.Text = (Int32.Parse(txtPhaiThu.Text) - tra_ngay).ToString();
            //txtTongNo.Text = (Int32.Parse(txtTongNo.Text) - tra_ngay).ToString();
        }

        private void dgvDSHangDaChon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            string ma_hang = dgvDSHangDaChon.Rows[numrow].Cells[0].Value.ToString().Trim();

            dtBase.UpdateData("update tblHang set SoLuongTon = SoLuongTon + " + dgvDSHangDaChon.Rows[numrow].Cells[3].Value + " where MaHang = N'" + ma_hang + "'");
            dtBase.UpdateData("delete from DsHangDaChon where MaHang = '" + ma_hang + "'");

            load_();
        }

        private void txtTiGia_TextChanged(object sender, EventArgs e)
        {
            double ti_gia = 1;

            try
            {
                ti_gia = double.Parse(txtTiGia.Text);

            }
            catch
            {

            }
            Double tien_hang = 0;
            for (int i = 0; i < dgvDSHangDaChon.Rows.Count - 1; i++)
            {
                tien_hang += Double.Parse(dgvDSHangDaChon.Rows[i].Cells[4].Value.ToString().Trim());
            }
            tien_hang = tien_hang * ti_gia;
            txtTienHang.Text = tien_hang.ToString();
            try
            {
                txtTienHang.Text = tien_hang.ToString();
                txtTongTien.Text = (tien_hang + Int32.Parse(txtTienVAT.Text)).ToString();
                txtPhaiThu.Text = (Int32.Parse(txtTongTien.Text) - Int32.Parse(txtBotTien.Text)).ToString();
                txtNoLai.Text = (Int32.Parse(txtPhaiThu.Text) - Int32.Parse(txtTraNgay.Text)).ToString();
                txtTongNo.Text = (Int32.Parse(txtNoLai.Text) + Int32.Parse(txtNoCu.Text)).ToString();
            }
            catch
            {

            }
        }

        private int  Luu_HD()
        {
            DataTable temp = dtBase.DataReader("Select * from DSHangDaChon");
            if (cbxKhachHang.Text == "" || cbxNhanVien.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
                return -1;
            }
            else if (temp.Rows.Count < 1)
            {
                MessageBox.Show("Danh sách hàng đã chọn rỗng. Vui lòng chọn hàng trước khi lập hóa đơn");
                return -1;
            }
            else
            {
                //To-do: Sinh mã hóa đơn tiếp theo
                string Ma_HD;
                try
                {
                    DataTable mahd = dtBase.DataReader("select top 1 MaHDD from tblDatHang order by MaHDD desc");

                    string text = mahd.Rows[0].ItemArray[0].ToString().Trim();

                    int tong_dai = mahd.Rows[0].ItemArray[0].ToString().Trim().Length;

                    Ma_HD = "HDD";

                    string t = "";

                    for (int i = 3; i < tong_dai; i++)
                    {
                        t += text[i].ToString();
                    }

                    int numb = Int32.Parse(t) + 1;

                    for (int i = 0; i < tong_dai - 3 - numb.ToString().Length; i++)
                    {
                        Ma_HD += '0';
                    }
                    Ma_HD += numb.ToString();
                    //MessageBox.Show(Ma_HD);
                }
                catch
                {
                    Ma_HD = "HD0001";
                }



                //To-do: Lưu vào bảng đặt hàng
                string makh = cbxKhachHang.SelectedValue.ToString().Trim();

                string tennv = cbxNhanVien.Text.ToString().Trim();
                DataTable temp1 = dtBase.DataReader("select MaNV from tblNhanVien where TenNV = N'" + tennv + "'");
                string manv = temp1.Rows[0].ItemArray[0].ToString().Trim();

                DataTable diachi = dtBase.DataReader("select DiaChi from tblKhachHang where MaKH = '" + makh + "'");

                try
                {
                    //string ttt =      "insert into tblDatHang(MaHDD, MaKH, MaNV, NgayDatHang, NgayGiaoHang, NoiGiaoHang, TongTien, TraNgay, NoLai) values('" + Ma_HD + "', '" + makh + "', '" + manv + "', '" + cbxTime.Text + "', '" + cbxTime.Text + "', N'" + diachi.Rows[0].ItemArray[0].ToString() + "', " + Int32.Parse(txtPhaiThu.Text) + ", " + Int32.Parse(txtTraNgay.Text) + ", " + Int32.Parse(txtNoLai.Text) + ")";
                    //MessageBox.Show(ttt);
                    dtBase.UpdateData("insert into tblDatHang(MaHDD, MaKH, MaNV, NgayDatHang, NgayGiaoHang, NoiGiaoHang, TongTien, TraNgay, NoLai) values('" + Ma_HD + "', '" + makh + "', '" + manv + "', '" + cbxTime.Text + "', '" + cbxTime.Text + "', N'" + diachi.Rows[0].ItemArray[0].ToString() + "', " + Int32.Parse(txtPhaiThu.Text) + ", " + Int32.Parse(txtTraNgay.Text) + ", " + Int32.Parse(txtNoLai.Text) + ")");

                    //string ttt1 = "update tblKhachHang set TienNo = TienNo - " + 20 + " where MaKH = '" + cbxKhachHang.SelectedValue.ToString() + "'";
                    //MessageBox.Show(ttt1);
                    dtBase.UpdateData("update tblKhachHang set TienNo = " + Int32.Parse(txtTongNo.Text) + "where MaKH = '" + cbxKhachHang.SelectedValue.ToString() + "'");
                }
                catch
                {
                    MessageBox.Show("Lưu vào bảng đặt hàng không thành công!");
                    return -1;
                }

                //To-do: Lưu vào bảng chi tiết
                try
                {
                    for (int i = 0; i < dgvDSHangDaChon.Rows.Count - 1; i++)
                    {
                        string mahang = dgvDSHangDaChon.Rows[i].Cells[0].Value.ToString();
                        string soluong = dgvDSHangDaChon.Rows[i].Cells[3].Value.ToString();
                        string thanhtien = dgvDSHangDaChon.Rows[i].Cells[4].Value.ToString();

                        //string ttt2 = "insert into tblChiTietDatHang(MaHDD, MaHang, SoLuong, ThanhTien) values('" + Ma_HD + "', '" + mahang + "', '" + soluong + "', '" + thanhtien + "')";
                        //MessageBox.Show(ttt2);
                        dtBase.UpdateData("insert into tblChiTietDatHang(MaHDD, MaHang, SoLuong, ThanhTien) values('" + Ma_HD + "', '" + mahang + "', '" + soluong + "', '" + thanhtien + "')");
                    }
                }
                catch
                {
                    MessageBox.Show("Lưu vào bảng chi tiết đặt hàng không thành công!");
                    return -1;
                }

                try
                {
                    dtBase.UpdateData("delete from DsHangDaChon where exists(select * from dshangdachon)");
                }
                catch
                {
                    MessageBox.Show("Xóa danh sách đã chọn không thành công!");
                    return -1;
                }
                load_();
                return 1;
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if(Luu_HD() == 1)
                {
                    MessageBox.Show("Lưu hóa đơn thành công!");
                    load_();
                    resetData();
                    this.Close();
                }
            }
            catch
            {

            }

        }

        private void btnLuuVaExport_Click(object sender, EventArgs e)
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
                header.Value = "Hóa đơn bán hàng";

                exSheet.get_Range("A7:K7").Font.Bold = true;
                exSheet.get_Range("A7:K7").HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                exSheet.Rows.WrapText = true;

                Excel.Range tenkh = (Excel.Range)exSheet.Cells[6, 2];
                exSheet.get_Range("B6:G6").Merge(true);
                tenkh.Value = "Khách hàng: " + cbxKhachHang.Text.Trim();

                string makh = cbxKhachHang.SelectedValue.ToString().Trim();
                DataTable diachi = dtBase.DataReader("select DiaChi from tblKhachHang where MaKH = '" + makh + "'");

                Excel.Range tt = (Excel.Range)exSheet.Cells[7, 2];
                exSheet.get_Range("B7:G7").Merge(true);
                tt.Value = "Địa chỉ: " + diachi.Rows[0].ItemArray[0].ToString();

                exSheet.get_Range("A9").Value = "STT";
                exSheet.get_Range("B9").Value = "Mã hàng";
                exSheet.get_Range("C9").Value = "Tên hàng";
                exSheet.get_Range("C9").ColumnWidth = 30;

                exSheet.get_Range("D9").Value = "Giá bán";
                exSheet.get_Range("E9").Value = "Số Lượng";
                exSheet.get_Range("E9").ColumnWidth = 15;

                exSheet.get_Range("F9").Value = "Thành tiền";
                exSheet.get_Range("F9").ColumnWidth = 15;

                int i;
                int j;
                for (i = 0; i < dgvDSHangDaChon.Rows.Count - 1; i++)
                {
                    for (j = 0; j < dgvDSHangDaChon.Columns.Count; j++)
                    {
                        exSheet.Cells[i + 10, 1] = (i + 1).ToString();
                        exSheet.Cells[i + 10, j + 2] = dgvDSHangDaChon.Rows[i].Cells[j].Value.ToString();
                    }
                }

                Excel.Range stringTongTien = (Excel.Range)exSheet.Cells[dgvDSHangDaChon.Rows.Count + 11, dgvDSHangDaChon.Columns.Count];
                stringTongTien.Font.Size = 12;
                stringTongTien.Font.Color = Color.Black;
                stringTongTien.Value = "Tổng tiền: ";

                Excel.Range Tongtien = (Excel.Range)exSheet.Cells[dgvDSHangDaChon.Rows.Count + 11, dgvDSHangDaChon.Columns.Count + 1];
                Tongtien.Font.Size = 12;
                Tongtien.Font.Color = Color.Black;
                Tongtien.Value = txtPhaiThu.Text;

                Excel.Range ngay = (Excel.Range)exSheet.Cells[dgvDSHangDaChon.Rows.Count + 13, 4];
                string temp = "D" + (dgvDSHangDaChon.Rows.Count + 13).ToString() + ":F" + (dgvDSHangDaChon.Rows.Count + 13).ToString();
                exSheet.get_Range(temp).Merge(true);
                ngay.Font.Color = Color.Black;

                string[] dat = cbxTime.Text.Split('/');
                ngay.Value = "Thái Bình, Ngày " + dat[1] + " tháng " + dat[0] + " năm " + dat[2];

                Excel.Range stringNV = (Excel.Range)exSheet.Cells[dgvDSHangDaChon.Rows.Count + 14, 5];
                string temp1 = "E" + (dgvDSHangDaChon.Rows.Count + 14).ToString() + ":F" + (dgvDSHangDaChon.Rows.Count + 14).ToString();
                exSheet.get_Range(temp1).Merge(true);
                stringNV.Font.Color = Color.Black;
                stringNV.Value = "Nhân viên bán hàng";

                Excel.Range tennv = (Excel.Range)exSheet.Cells[dgvDSHangDaChon.Rows.Count + 16, 5];
                string temp2 = "E" + (dgvDSHangDaChon.Rows.Count + 16).ToString() + ":F" + (dgvDSHangDaChon.Rows.Count + 16).ToString();
                exSheet.get_Range(temp2).Merge(true);
                stringNV.Font.Color = Color.Black;
                tennv.Value = cbxNhanVien.Text;

                exSheet.Name = "HoaDon";

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

            try
            {
                if (Luu_HD() == 1)
                {
                    this.Close();
                }

            }
            catch
            {

            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {        
            if (MessageBox.Show("Bạn có thật sự muốn hủy hóa đơn? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                for (int i=0; i<dgvDSHangDaChon.Rows.Count-1; i++)
                {
                    dtBase.UpdateData("update tblHang set SoLuongTon = SoLuongTon + " + dgvDSHangDaChon.Rows[i].Cells[3].Value + " where MaHang = N'" + dgvDSHangDaChon.Rows[i].Cells[0].Value.ToString().Trim() + "'");
                }

                dtBase.UpdateData("delete from DsHangDaChon where exists(select * from dshangdachon)");
                this.Close();
            }
        }
    }
}
