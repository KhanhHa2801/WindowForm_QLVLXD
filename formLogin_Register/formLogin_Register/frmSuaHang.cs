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
    public partial class frmSuaHang : Form
    {
        public frmSuaHang()
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
        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmSuaHang_Load(object sender, EventArgs e)
        {
            DateTime d = DateTime.Today;
            txtNgayTao.Text = d.ToString(" dd / MM / yyyy");

            btnRefesh.Enabled = false;
        }
    }
}
