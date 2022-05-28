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

namespace formLogin_Register
{
    public partial class frmChangePass : Form
    {
        DataAccess dtBase = new DataAccess();

        Caesar caesar = new Caesar();


        public string username;
        public string old_password;
        public string new1_password;
        public string new2_password;

        public frmChangePass()
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            txtUser.Text = frmLogin.tendangnhap;
            txtOldPass.PasswordChar = '*';
            txtNewPass.PasswordChar = '*';
            txtNewPassAgain.PasswordChar = '*';

            btnXacNhan.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            if(txtNewPass.Text != txtNewPassAgain.Text)
            {
                labelNote.Text = "Chưa khớp";
                btnXacNhan.Enabled = false;
            }
            else
            {
                btnXacNhan.Enabled = true;
                labelNote.Text = "";
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            labelNote.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string user = txtUser.Text.Trim();
            DataTable temp = dtBase.DataReader("Select * from tblTaiKhoan2 where TenDangNhap = N'" + user + "'");

            if(temp.Rows.Count < 1)
            {
                MessageBox.Show("Tài khoản không tồn tại!");
            }
            else
            {
                string pass = temp.Rows[0].ItemArray[1].ToString().Trim();

                string destr = caesar.decrypt(pass, 13);

                string enstr = caesar.encrypt(txtNewPass.Text, 13);
                
                if(txtNewPass.Text == "" || txtNewPassAgain.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!");
                }

                else if (destr.Trim().Equals(txtOldPass.Text))
                {
                    dtBase.DataReader("Update tblTaiKhoan2 Set MatKhau = N'" + enstr + "' where TenDangNhap = N'" + user + "'");
                    MessageBox.Show("Đổi mật khẩu thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác!");
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            //frmLogin form1 = new frmLogin();
            //form1.ShowDialog();
            //this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmChangePass_Leave(object sender, EventArgs e)
        {
            
        }

        private void frmChangePass_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (MessageBox.Show("BẠN có muốn thoát? ","Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            //{
            //    this.Close();
            //}
            //else
            //{
            //    //frmLogin form1 = new frmLogin();
            //    //form1.ShowDialog();
            //}
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //frmLogin form1 = new frmLogin();
            //form1.ShowDialog();
        }
    }
}
