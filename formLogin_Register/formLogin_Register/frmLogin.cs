using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;

namespace formLogin_Register
{
    public partial class frmLogin : Form
    {
        DataAccess dtBase = new DataAccess();

        Caesar caesar = new Caesar();

        public static string chucvu;
        public static string tendangnhap;

        private string matkhau;

        public frmLogin()
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cbxTenDangNhap.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
            }
            else
            {
                string user = cbxTenDangNhap.Text.Trim();
                tendangnhap = user;

                string enstr = caesar.encrypt(txtPassword.Text.Trim(), 13);

                DataTable temp = dtBase.DataReader("Select * from tblTaiKhoan2 where TenDangNhap = N'" + user + "'");
                string pass = temp.Rows[0].ItemArray[1].ToString().Trim();

                chucvu = temp.Rows[0].ItemArray[2].ToString().Trim();

                //MessageBox.Show(chucvu);

                if (pass.Equals(enstr))
                {
                    //load form giao diện chính
                    //MessageBox.Show("Đăng nhập thành công");
                    this.Hide();
                    frmTongquan form3 = new frmTongquan();
                    form3.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //this.KeyDown += form;

            btnHienMatKhau.Visible = false;


            cbxTenDangNhap.DataSource = dtBase.DataReader("Select * from tblTaiKhoan2");
            cbxTenDangNhap.DisplayMember = "TenDangNhap";
            cbxTenDangNhap.ValueMember = "TenDangNhap";
            cbxTenDangNhap.Text = "";

            lklHoTro.Text = "Cần trợ giúp"; 
            lklHoTro.Links.Add(6, 4, "www.facebook.com/KhanhCuCuteAhihi");

            label4.AutoSize = false;
            label4.Height = 370;
            label4.Width = 2;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.ForeColor = Color.Blue;

            cbxTenDangNhap.ForeColor = Color.LightGray;
            cbxTenDangNhap.Text = "Tên đăng nhập";
            this.cbxTenDangNhap.Leave += new System.EventHandler(this.txtUser_Leave);
            this.cbxTenDangNhap.Enter += new System.EventHandler(this.txtUser_Enter);

            txtPassword.ForeColor = Color.LightGray;
            txtPassword.Text = "Mật khẩu";
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if(cbxTenDangNhap.Text == "")
            {
                cbxTenDangNhap.Text = "Tên đăng nhập";
                cbxTenDangNhap.ForeColor = Color.Gray;
            }
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (cbxTenDangNhap.Text == "Tên đăng nhập")
            {
                cbxTenDangNhap.Text = "";
                cbxTenDangNhap.ForeColor = Color.Black;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Mật khẩu";
                txtPassword.ForeColor = Color.Gray;
                //txtPassword.PasswordChar = '';
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Mật khẩu")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                //txtPassword.PasswordChar = '*';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if(txtPassword.Text.Trim() != "" && txtPassword.Text != "Mật khẩu")
            {
                matkhau = txtPassword.Text.Trim();
                txtPassword.PasswordChar = '*';
            }
        }

        private void lklHoTro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmChangePass form2 = new frmChangePass();
            form2.ShowDialog();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtPassword.Text = matkhau;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (cbxTenDangNhap.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!");
                }
                else
                {
                    string user = cbxTenDangNhap.Text.Trim();
                    tendangnhap = user;

                    string enstr = caesar.encrypt(txtPassword.Text.Trim(), 13);

                    DataTable temp = dtBase.DataReader("Select * from tblTaiKhoan2 where TenDangNhap = N'" + user + "'");
                    string pass = temp.Rows[0].ItemArray[1].ToString().Trim();

                    chucvu = temp.Rows[0].ItemArray[2].ToString().Trim();

                    //MessageBox.Show(chucvu);

                    if (pass.Equals(enstr))
                    {
                        //load form giao diện chính
                        //MessageBox.Show("Đăng nhập thành công");
                        this.Hide();
                        frmTongquan form3 = new frmTongquan();
                        form3.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                    }
                }
            }
        }

        private void cbxTenDangNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            tendangnhap = cbxTenDangNhap.Text;
        }
    }
}
