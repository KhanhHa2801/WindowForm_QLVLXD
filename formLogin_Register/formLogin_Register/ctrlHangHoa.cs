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
    public partial class ctrlHangHoa : UserControl
    {
        private static ctrlHangHoa _instance;

        DataAccess dtBase = new DataAccess();

        public static ctrlHangHoa Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlHangHoa();
                return _instance;
            }
        }
        public ctrlHangHoa()
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
        private void ctrlDanhMuc_Load(object sender, EventArgs e)
        {

            load_();

            //DateTime d = DateTime.Today;
            //txtNgayTao.Text = d.ToString("dd/MM/yyyy");

            txtTimKiem.Text = "Tìm kiếm";

            txtTimKiem.ForeColor = Color.LightGray;
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);


            cbxTenNhomHang.DataSource = dtBase.DataReader("select * from tblNhomHang");
            cbxTenNhomHang.DisplayMember = "TenNhomHang";
            cbxTenNhomHang.ValueMember = "MaNhomHang";
            cbxTenNhomHang.Text = "";

            cbxTenDVT.Items.Add("Bao");
            cbxTenDVT.Items.Add("Cái");
            cbxTenDVT.Items.Add("Đôi");
            cbxTenDVT.Items.Add("Kg");
            cbxTenDVT.Items.Add("Tấm");
            cbxTenDVT.Items.Add("Tấn");
            cbxTenDVT.Items.Add("Viên");
            cbxTenDVT.Items.Add("Yến");

            cbxTenDVT.SelectedItem = "";
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            frmThemHang frmth = new frmThemHang();
            frmth.ShowDialog();
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                load_();
            }
            else if (txtTimKiem.Text != "Tìm kiếm")
            {
                DataTable dtHang = dtBase.DataReader("select MaHang, TenHang, Soluongton, GiaNhap, GiaBan, TenNhomHang, DonViTinh, MoTa from tblHang join tblNhomHang on tblhang.MaNhomHang=tblNhomHang.MaNhomHang where MaHang like N'%" + txtTimKiem.Text + "%' or TenHang like N'%" + txtTimKiem.Text + "%'");
                dgvHang.DataSource = dtHang;

                for (int i = 0; i < dgvHang.RowCount; i = i + 2)
                {
                    dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                dgvHang.Columns[1].Width = 200;
                dgvHang.Columns[7].Width = 200;
                dgvHang.Columns[5].Width = 150;

                dgvHang.Columns[0].HeaderText = "Mã Hàng";
                dgvHang.Columns[1].HeaderText = "Tên Hàng";
                dgvHang.Columns[2].HeaderText = "Số lượng tồn";
                dgvHang.Columns[3].HeaderText = "Giá nhập";
                dgvHang.Columns[4].HeaderText = "Giá bán";
                dgvHang.Columns[5].HeaderText = "Nhóm hàng";
                dgvHang.Columns[6].HeaderText = "Đơn vị tính";
                dgvHang.Columns[7].HeaderText = "Mô tả";

                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                txtMaHang.Text = "";
                txtTenHang.Text = "";
                cbxTenNhomHang.Text = "";
                cbxTenDVT.Text = "";
                txtMoTa.Text = "";
                txtGiaNhap.Text = "";
                txtGiaBan.Text = "";
                txtSoLuong.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DataTable mahang = dtBase.DataReader("select top 1 MaHang from tblHang order by MaHang desc");
            //string []text = mahang.Rows[0].ItemArray[0].ToString().Split('0');

            //string res = text[0];

            //for (int i=0; i< mahang.Rows[0].ItemArray[0].ToString().Trim().Length-text[0].Trim().Length- text[text.Length - 1].Trim().Length; i++)
            //{
            //    res += '0';
            //}
            //int duoi = Int32.Parse(text[text.Length - 1]) + 1;
            //res += duoi.ToString();
            //txtMaHang.Text = res;

            if (txtMaHang.Text == "" || txtTenHang.Text == "" || cbxTenNhomHang.Text == "" || cbxTenDVT.Text == "" || txtGiaNhap.Text == "" || txtGiaBan.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đủ dữ liệu");
            }
            else
            {
                DataTable temp = dtBase.DataReader("select * from tblHang where MaHang = '" + txtMaHang.Text + "'");

                if (temp.Rows.Count > 0)
                {
                    MessageBox.Show("Mã hàng đã tồn tại");
                    txtMaHang.Focus();
                }
                else
                {
                    if (float.Parse(txtGiaBan.Text) < float.Parse(txtGiaNhap.Text))
                    {
                        if (MessageBox.Show("Giá bán hiện đang nhỏ hơn giá nhập. Bạn chắc chứ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            txtGiaBan.Focus();
                        }
                        else
                        {
                            DataTable t = dtBase.DataReader("select MaNhomHang from tblNhomHang where TenNhomHang = N'" + cbxTenNhomHang.Text + "'");
                            dtBase.UpdateData("insert into tblHang(MaHang, TenHang, MaNhomHang, DonViTinh, MoTa) values('" + txtMaHang.Text + "', '" + txtTenHang.Text + "', '" + t.Rows[0].ItemArray[0].ToString() + "', '" + cbxTenDVT.Text + "', N'" + txtMoTa.Text + "')");
                            MessageBox.Show("Đã thêm thành công!");
                            load_();
                        }
                    }
                    else
                    {
                        DataTable t = dtBase.DataReader("select MaNhomHang from tblNhomHang where TenNhomHang = N'" + cbxTenNhomHang.Text + "'");
                        dtBase.UpdateData("insert into tblHang(MaHang, TenHang, MaNhomHang, DonViTinh, MoTa) values('" + txtMaHang.Text + "', '" + txtTenHang.Text + "', '" + t.Rows[0].ItemArray[0].ToString() + "', '" + cbxTenDVT.Text + "', N'" + txtMoTa.Text +  "')");
                        MessageBox.Show("Đã thêm thành công!");
                        load_();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable t = dtBase.DataReader("select MaNhomHang from tblNhomHang where TenNhomHang = N'" + cbxTenNhomHang.Text + "'");
            //string t2 = "update tblHang set TenHang = N'" + txtTenHang.Text + "', GiaNhap = '" + txtGiaNhap.Text + "', GiaBan = '" + txtGiaBan.Text + "', MaNhomHang = N'" + t.Rows[0].ItemArray[0].ToString().Trim() + "', DonViTinh = N'" + cbxTenDVT.Text + "', MoTa = N'" + txtMoTa.Text + "'";
            //MessageBox.Show(t2);
            dtBase.UpdateData("update tblHang set TenHang = N'" + txtTenHang.Text + "', GiaNhap = '" + txtGiaNhap.Text + "', GiaBan = '" + txtGiaBan.Text + "', MaNhomHang = N'" + t.Rows[0].ItemArray[0].ToString().Trim() + "', DonViTinh = N'" + cbxTenDVT.Text + "', MoTa = N'" + txtMoTa.Text + "', SoLuongTon = '" + txtSoLuong.Text + "' where mahang = '" + txtMaHang.Text +"'");
            load_();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            load_();
        }

        private void load_()
        {
            DataTable dtHang = dtBase.DataReader("select MaHang, TenHang, SoLuongTon, GiaNhap, GiaBan, TenNhomHang, DonViTinh, MoTa from tblHang join tblNhomHang on tblhang.MaNhomHang=tblNhomHang.MaNhomHang");
            dgvHang.DataSource = dtHang;
            for (int i = 0; i < dgvHang.RowCount; i = i + 2)
            {
                dgvHang.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            dgvHang.Columns[1].Width = 200;
            dgvHang.Columns[7].Width = 200;
            dgvHang.Columns[5].Width = 150;

            dgvHang.Columns[0].HeaderText = "Mã Hàng";
            dgvHang.Columns[1].HeaderText = "Tên Hàng";
            dgvHang.Columns[2].HeaderText = "Số lượng tồn";
            dgvHang.Columns[3].HeaderText = "Giá nhập";
            dgvHang.Columns[4].HeaderText = "Giá bán";
            dgvHang.Columns[5].HeaderText = "Nhóm hàng";
            dgvHang.Columns[6].HeaderText = "Đơn vị tính";
            dgvHang.Columns[7].HeaderText = "Mô tả";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbxTenNhomHang.Text = "";
            cbxTenDVT.Text = "";
            txtMoTa.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtSoLuong.Text = "";
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbxTenNhomHang.Text = "";
            cbxTenDVT.Text = "";
            txtMoTa.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtSoLuong.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            txtMaHang.Text = dgvHang.Rows[numrow].Cells[0].Value.ToString().Trim();
            txtTenHang.Text = dgvHang.Rows[numrow].Cells[1].Value.ToString().Trim();
            txtSoLuong.Text = dgvHang.Rows[numrow].Cells[2].Value.ToString().Trim();
            cbxTenNhomHang.Text = dgvHang.Rows[numrow].Cells[5].Value.ToString().Trim();
            cbxTenDVT.Text = dgvHang.Rows[numrow].Cells[6].Value.ToString().Trim();
            txtMoTa.Text = dgvHang.Rows[numrow].Cells[7].Value.ToString().Trim();
            txtGiaNhap.Text = dgvHang.Rows[numrow].Cells[3].Value.ToString().Trim();
            txtGiaBan.Text = dgvHang.Rows[numrow].Cells[4].Value.ToString().Trim();

            if(frmLogin.chucvu == "Quản lý")
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa " + txtTenHang.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                dtBase.UpdateData("Delete from tblHang where mahang = '" + txtMaHang.Text + "'");
                load_();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text != "" || txtTenHang.Text != "" || cbxTenNhomHang.Text != "" || cbxTenDVT.Text != "" || txtGiaNhap.Text != "" || txtGiaBan.Text != "")
            {
                if (MessageBox.Show("Dữ liệu đã thay đổi! Bạn có muốn thoát? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.Visible = false;
                }
                else
                {

                }
            }
            else
            {
                this.Visible = false;
            }
        }

        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {
            float a;
            if(txtGiaBan.Text != "")
            {
                if (float.TryParse(txtGiaBan.Text, out a) == false)
                {
                    MessageBox.Show("Giá bán phải là số!");
                }
            }
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {
            float a;
            if(txtGiaNhap.Text != "")
            {
                if (float.TryParse(txtGiaNhap.Text, out a) == false)
                {
                    MessageBox.Show("Giá nhập phải là số!");
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
                header.Value = "DANH SÁCH CÁC MẶT HÀNG";

                exSheet.get_Range("A7:K7").Font.Bold = true;
                exSheet.get_Range("A7:K7").HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                exSheet.Rows.WrapText = true;

                exSheet.get_Range("A7").Value = "STT";
                exSheet.get_Range("B7").Value = "Mã hàng";
                exSheet.get_Range("C7").Value = "Tên hàng";
                exSheet.get_Range("C7").ColumnWidth = 30;

                exSheet.get_Range("D7").Value = "Số Lượng";
                exSheet.get_Range("E7").Value = "Giá nhập";
                exSheet.get_Range("F7").Value = "Giá bán";
                exSheet.get_Range("G7").Value = "Nhóm hàng";
                exSheet.get_Range("G7").ColumnWidth = 20;
                exSheet.get_Range("H7").Value = "Đơn vị tính";
                exSheet.get_Range("H7").ColumnWidth = 10;
                exSheet.get_Range("I7").Value = "Mô tả";
                exSheet.get_Range("I7").ColumnWidth = 20;

                for (int i = 0; i < dgvHang.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvHang.Columns.Count; j++)
                    {
                        exSheet.Cells[i + 8, 1] = (i + 1).ToString();
                        exSheet.Cells[i + 8, j + 2] = dgvHang.Rows[i].Cells[j].Value.ToString();
                    }
                }
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

        private void txtMaHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            
        }
    }
}
