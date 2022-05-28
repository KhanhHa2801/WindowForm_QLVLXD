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


    public partial class ctrlNCC : UserControl
    {
        private static ctrlNCC _instance;

        DataAccess dtBase = new DataAccess();

        public static ctrlNCC Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlNCC();
                return _instance;
            }
        }

        public ctrlNCC()
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
        private void ctrlNCC_Load(object sender, EventArgs e)
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
            DataTable dtNCC = dtBase.DataReader("select MaNCC, TenNCC, SDT, DiaChi, TienNo from tblNhaCungCap");
            dgvNCC.DataSource = dtNCC;
            for (int i = 0; i < dgvNCC.RowCount; i = i + 2)
            {
                dgvNCC.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }

            dgvNCC.Columns[1].Width = 300;
            dgvNCC.Columns[3].Width = 300;

            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns[2].HeaderText = "SĐT";
            dgvNCC.Columns[3].HeaderText = "Địa chỉ";
            dgvNCC.Columns[4].HeaderText = "Tiền nợ";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtTienNo.Text = "";
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            load_();
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtTienNo.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                load_();
            }
            else if (txtTimKiem.Text != "Tìm kiếm")
            {
                DataTable dtNCC = dtBase.DataReader("select MaNCC, TenNCC, SDT, DiaChi, TienNo from tblNhaCungCap where MaNCC like N'%" + txtTimKiem.Text + "%' or TenNCC like N'%" + txtTimKiem.Text + "%'");
                dgvNCC.DataSource = dtNCC;
                for (int i = 0; i < dgvNCC.RowCount; i = i + 2)
                {
                    dgvNCC.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                for (int i = 0; i < dgvNCC.RowCount; i = i + 2)
                {
                    dgvNCC.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

                dgvNCC.Columns[1].Width = 300;
                dgvNCC.Columns[3].Width = 300;

                dgvNCC.Columns[0].HeaderText = "Mã NCC";
                dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
                dgvNCC.Columns[2].HeaderText = "SĐT";
                dgvNCC.Columns[3].HeaderText = "Địa chỉ";
                dgvNCC.Columns[4].HeaderText = "Tiền nợ";

                btnSua.Enabled = false;
                btnXoa.Enabled = false;

                txtMaNCC.Text = "";
                txtTenNCC.Text = "";
                txtSDT.Text = "";
                txtDiaChi.Text = "";
                txtTienNo.Text = "";
            }
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;

            txtMaNCC.Text = dgvNCC.Rows[numrow].Cells[0].Value.ToString().Trim();
            txtTenNCC.Text = dgvNCC.Rows[numrow].Cells[1].Value.ToString().Trim();
            txtSDT.Text = dgvNCC.Rows[numrow].Cells[2].Value.ToString().Trim();
            txtTienNo.Text = dgvNCC.Rows[numrow].Cells[4].Value.ToString().Trim();
            txtDiaChi.Text = dgvNCC.Rows[numrow].Cells[3].Value.ToString().Trim();

            if (frmLogin.chucvu == "Quản lý")
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text == "" || txtTenNCC.Text == "" || txtSDT.Text == "" || txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đủ dữ liệu");
            }
            else
            {
                DataTable temp = dtBase.DataReader("select * from tblNhaCungCap where MaNCC = '" + txtMaNCC.Text + "'");

                if (temp.Rows.Count > 0)
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại");
                    txtMaNCC.Focus();
                }
                else
                {
                    dtBase.UpdateData("insert into tblNhaCungCap(MaNCC, TenNCC, SDT, DiaChi) values ('" + txtMaNCC.Text.Trim() + "', N'" + txtTenNCC.Text.Trim() + "', '" + txtSDT.Text.Trim() + "', N'" + txtDiaChi.Text + "')");
                    MessageBox.Show("Đã thêm thành công!");
                }
            }
            load_();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa " + txtTenNCC.Text, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                dtBase.UpdateData("Delete from tblNhaCungCap where MaNCC = '" + txtMaNCC.Text + "'");
                load_();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dtBase.UpdateData("update tblNhaCungCap set TenNCC = N'" + txtTenNCC.Text + "', SDT = '" + txtSDT.Text + "', DiaChi = N'" + txtDiaChi.Text + "', TienNo ='" + txtTienNo.Text + "' where MaNCC = '" + txtMaNCC.Text + "'");
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
                header.Value = "DANH SÁCH NHÀ CUNG CẤP";

                exSheet.get_Range("A7:H7").Font.Bold = true;
                exSheet.get_Range("A7:H7").HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                exSheet.Rows.WrapText = true;

                exSheet.get_Range("A7").Value = "STT";
                exSheet.get_Range("B7").Value = "Mã NCC";
                exSheet.get_Range("B7").ColumnWidth = 15;
                exSheet.get_Range("C7").Value = "Tên Nhà Cung Cấp";
                exSheet.get_Range("C7").ColumnWidth = 25;

                exSheet.get_Range("D7").Value = "SĐT";
                exSheet.get_Range("D7").ColumnWidth = 15;
                exSheet.get_Range("E7").Value = "Địa chỉ";
                exSheet.get_Range("E7").ColumnWidth = 35;

                for (int i = 0; i < dgvNCC.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        exSheet.Cells[i + 8, 1] = (i + 1).ToString();
                        exSheet.Cells[i + 8, j + 2] = dgvNCC.Rows[i].Cells[j].Value.ToString();
                    }
                }
                exSheet.Name = "NCC";

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
