
namespace formLogin_Register
{
    partial class frmSuaHD_Nhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuaHD_Nhap));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtBotTien = new System.Windows.Forms.TextBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtVAT = new System.Windows.Forms.TextBox();
            this.txtTienVAT = new System.Windows.Forms.TextBox();
            this.txtTienHang = new System.Windows.Forms.TextBox();
            this.txtTiGia = new System.Windows.Forms.TextBox();
            this.txtTongNo = new System.Windows.Forms.TextBox();
            this.txtNoCu = new System.Windows.Forms.TextBox();
            this.txtNoLai = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTraNgay = new System.Windows.Forms.TextBox();
            this.txtPhaiThu = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxKhachHang = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxNhanVien = new System.Windows.Forms.ComboBox();
            this.cbxTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLuuVaExport = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.dgvDSHang = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDSHangDaChon = new System.Windows.Forms.DataGridView();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSHang)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSHangDaChon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuu.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuu.Location = new System.Drawing.Point(564, 645);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(92, 46);
            this.btnLuu.TabIndex = 65;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtBotTien
            // 
            this.txtBotTien.Location = new System.Drawing.Point(96, 163);
            this.txtBotTien.Name = "txtBotTien";
            this.txtBotTien.Size = new System.Drawing.Size(127, 26);
            this.txtBotTien.TabIndex = 11;
            this.txtBotTien.TextChanged += new System.EventHandler(this.txtBotTien_TextChanged);
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(96, 122);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(127, 26);
            this.txtTongTien.TabIndex = 10;
            // 
            // txtVAT
            // 
            this.txtVAT.Location = new System.Drawing.Point(96, 83);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.Size = new System.Drawing.Size(36, 26);
            this.txtVAT.TabIndex = 9;
            this.txtVAT.TextChanged += new System.EventHandler(this.txtVAT_TextChanged);
            // 
            // txtTienVAT
            // 
            this.txtTienVAT.Location = new System.Drawing.Point(138, 83);
            this.txtTienVAT.Name = "txtTienVAT";
            this.txtTienVAT.ReadOnly = true;
            this.txtTienVAT.Size = new System.Drawing.Size(85, 26);
            this.txtTienVAT.TabIndex = 8;
            // 
            // txtTienHang
            // 
            this.txtTienHang.Location = new System.Drawing.Point(96, 47);
            this.txtTienHang.Name = "txtTienHang";
            this.txtTienHang.ReadOnly = true;
            this.txtTienHang.Size = new System.Drawing.Size(127, 26);
            this.txtTienHang.TabIndex = 7;
            // 
            // txtTiGia
            // 
            this.txtTiGia.Location = new System.Drawing.Point(96, 15);
            this.txtTiGia.Name = "txtTiGia";
            this.txtTiGia.Size = new System.Drawing.Size(127, 26);
            this.txtTiGia.TabIndex = 6;
            this.txtTiGia.TextChanged += new System.EventHandler(this.txtTiGia_TextChanged);
            // 
            // txtTongNo
            // 
            this.txtTongNo.Location = new System.Drawing.Point(101, 163);
            this.txtTongNo.Name = "txtTongNo";
            this.txtTongNo.ReadOnly = true;
            this.txtTongNo.Size = new System.Drawing.Size(127, 26);
            this.txtTongNo.TabIndex = 11;
            // 
            // txtNoCu
            // 
            this.txtNoCu.Location = new System.Drawing.Point(101, 122);
            this.txtNoCu.Name = "txtNoCu";
            this.txtNoCu.ReadOnly = true;
            this.txtNoCu.Size = new System.Drawing.Size(127, 26);
            this.txtNoCu.TabIndex = 10;
            // 
            // txtNoLai
            // 
            this.txtNoLai.Location = new System.Drawing.Point(101, 83);
            this.txtNoLai.Name = "txtNoLai";
            this.txtNoLai.ReadOnly = true;
            this.txtNoLai.Size = new System.Drawing.Size(127, 26);
            this.txtNoLai.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTongNo);
            this.groupBox4.Controls.Add(this.txtNoCu);
            this.groupBox4.Controls.Add(this.txtNoLai);
            this.groupBox4.Controls.Add(this.txtTraNgay);
            this.groupBox4.Controls.Add(this.txtPhaiThu);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox4.Location = new System.Drawing.Point(248, 184);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(234, 203);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            // 
            // txtTraNgay
            // 
            this.txtTraNgay.Location = new System.Drawing.Point(101, 51);
            this.txtTraNgay.Name = "txtTraNgay";
            this.txtTraNgay.Size = new System.Drawing.Size(127, 26);
            this.txtTraNgay.TabIndex = 8;
            this.txtTraNgay.TextChanged += new System.EventHandler(this.txtTraNgay_TextChanged);
            // 
            // txtPhaiThu
            // 
            this.txtPhaiThu.Location = new System.Drawing.Point(101, 15);
            this.txtPhaiThu.Name = "txtPhaiThu";
            this.txtPhaiThu.ReadOnly = true;
            this.txtPhaiThu.Size = new System.Drawing.Size(127, 26);
            this.txtPhaiThu.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 166);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 18);
            this.label15.TabIndex = 6;
            this.label15.Text = "Tổng nợ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 127);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 18);
            this.label14.TabIndex = 5;
            this.label14.Text = "Nợ cũ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 18);
            this.label13.TabIndex = 4;
            this.label13.Text = "Nợ lại";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 18);
            this.label12.TabIndex = 3;
            this.label12.Text = "Trả trước";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 18);
            this.label11.TabIndex = 2;
            this.label11.Text = "Phải trả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhân viên";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxKhachHang);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxNhanVien);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(371, 159);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            // 
            // cbxKhachHang
            // 
            this.cbxKhachHang.FormattingEnabled = true;
            this.cbxKhachHang.Location = new System.Drawing.Point(114, 113);
            this.cbxKhachHang.Name = "cbxKhachHang";
            this.cbxKhachHang.Size = new System.Drawing.Size(251, 26);
            this.cbxKhachHang.TabIndex = 5;
            this.cbxKhachHang.SelectedIndexChanged += new System.EventHandler(this.cbxKhachHang_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Đối tác";
            // 
            // cbxNhanVien
            // 
            this.cbxNhanVien.FormattingEnabled = true;
            this.cbxNhanVien.Location = new System.Drawing.Point(114, 63);
            this.cbxNhanVien.Name = "cbxNhanVien";
            this.cbxNhanVien.Size = new System.Drawing.Size(251, 26);
            this.cbxNhanVien.TabIndex = 3;
            // 
            // cbxTime
            // 
            this.cbxTime.Location = new System.Drawing.Point(114, 18);
            this.cbxTime.Name = "cbxTime";
            this.cbxTime.Size = new System.Drawing.Size(251, 26);
            this.cbxTime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 18);
            this.label10.TabIndex = 5;
            this.label10.Text = "Bớt tiền";
            // 
            // btnLuuVaExport
            // 
            this.btnLuuVaExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuuVaExport.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnLuuVaExport.Image = ((System.Drawing.Image)(resources.GetObject("btnLuuVaExport.Image")));
            this.btnLuuVaExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuuVaExport.Location = new System.Drawing.Point(707, 645);
            this.btnLuuVaExport.Name = "btnLuuVaExport";
            this.btnLuuVaExport.Size = new System.Drawing.Size(180, 46);
            this.btnLuuVaExport.TabIndex = 64;
            this.btnLuuVaExport.Text = "Lưu và Export";
            this.btnLuuVaExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLuuVaExport.UseVisualStyleBackColor = true;
            this.btnLuuVaExport.Click += new System.EventHandler(this.btnLuuVaExport_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label16.Location = new System.Drawing.Point(562, 373);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(178, 17);
            this.label16.TabIndex = 63;
            this.label16.Text = "Click đúp chuột để trả hàng";
            // 
            // btnHuyPhieu
            // 
            this.btnHuyPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuyPhieu.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnHuyPhieu.Image = ((System.Drawing.Image)(resources.GetObject("btnHuyPhieu.Image")));
            this.btnHuyPhieu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHuyPhieu.Location = new System.Drawing.Point(925, 645);
            this.btnHuyPhieu.Name = "btnHuyPhieu";
            this.btnHuyPhieu.Size = new System.Drawing.Size(166, 46);
            this.btnHuyPhieu.TabIndex = 66;
            this.btnHuyPhieu.Text = "Hủy thay đổi";
            this.btnHuyPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHuyPhieu.UseVisualStyleBackColor = true;
            this.btnHuyPhieu.Click += new System.EventHandler(this.btnHuyPhieu_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTimKiem);
            this.groupBox2.Controls.Add(this.btnThem);
            this.groupBox2.Controls.Add(this.txtSoLuong);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtTenHang);
            this.groupBox2.Controls.Add(this.dgvDSHang);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox2.Location = new System.Drawing.Point(482, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(642, 334);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(6, 48);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(520, 26);
            this.txtTimKiem.TabIndex = 6;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            // 
            // btnThem
            // 
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThem.Location = new System.Drawing.Point(545, 23);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(92, 37);
            this.btnThem.TabIndex = 5;
            this.btnThem.Text = "Thêm";
            this.btnThem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(433, 15);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(84, 26);
            this.txtSoLuong.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(316, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "Số lượng nhập";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tên hàng";
            // 
            // txtTenHang
            // 
            this.txtTenHang.Location = new System.Drawing.Point(83, 16);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.Size = new System.Drawing.Size(228, 26);
            this.txtTenHang.TabIndex = 1;
            // 
            // dgvDSHang
            // 
            this.dgvDSHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSHang.Location = new System.Drawing.Point(6, 79);
            this.dgvDSHang.Name = "dgvDSHang";
            this.dgvDSHang.RowHeadersWidth = 51;
            this.dgvDSHang.RowTemplate.Height = 24;
            this.dgvDSHang.Size = new System.Drawing.Size(630, 249);
            this.dgvDSHang.TabIndex = 0;
            this.dgvDSHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSHang_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBotTien);
            this.groupBox3.Controls.Add(this.txtTongTien);
            this.groupBox3.Controls.Add(this.txtVAT);
            this.groupBox3.Controls.Add(this.txtTienVAT);
            this.groupBox3.Controls.Add(this.txtTienHang);
            this.groupBox3.Controls.Add(this.txtTiGia);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox3.Location = new System.Drawing.Point(13, 184);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 203);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 18);
            this.label9.TabIndex = 4;
            this.label9.Text = "Tổng";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 18);
            this.label8.TabIndex = 3;
            this.label8.Text = "VAT%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "Tiền hàng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "Tỷ giá";
            // 
            // dgvDSHangDaChon
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSHangDaChon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSHangDaChon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSHangDaChon.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDSHangDaChon.Location = new System.Drawing.Point(41, 393);
            this.dgvDSHangDaChon.Name = "dgvDSHangDaChon";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSHangDaChon.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDSHangDaChon.RowHeadersWidth = 51;
            this.dgvDSHangDaChon.RowTemplate.Height = 24;
            this.dgvDSHangDaChon.Size = new System.Drawing.Size(1050, 235);
            this.dgvDSHangDaChon.TabIndex = 60;
            this.dgvDSHangDaChon.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSHangDaChon_CellDoubleClick);
            // 
            // frmSuaHD_Nhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 726);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLuuVaExport);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnHuyPhieu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvDSHangDaChon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSuaHD_Nhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa Phiếu nhập";
            this.Load += new System.EventHandler(this.frmSuaHD_Nhap_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSHang)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSHangDaChon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.TextBox txtBotTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtVAT;
        private System.Windows.Forms.TextBox txtTienVAT;
        private System.Windows.Forms.TextBox txtTienHang;
        private System.Windows.Forms.TextBox txtTiGia;
        private System.Windows.Forms.TextBox txtTongNo;
        private System.Windows.Forms.TextBox txtNoCu;
        private System.Windows.Forms.TextBox txtNoLai;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTraNgay;
        private System.Windows.Forms.TextBox txtPhaiThu;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxKhachHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxNhanVien;
        private System.Windows.Forms.DateTimePicker cbxTime;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnLuuVaExport;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnHuyPhieu;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.DataGridView dgvDSHang;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvDSHangDaChon;
    }
}