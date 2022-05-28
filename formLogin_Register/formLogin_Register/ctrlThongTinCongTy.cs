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
    public partial class ctrlThongTinCongTy : UserControl
    {
        private static ctrlThongTinCongTy _instance;

        public static ctrlThongTinCongTy Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlThongTinCongTy();
                return _instance;
            }
        }

        DataAccess dtBase = new DataAccess();

        public ctrlThongTinCongTy()
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
        private void ctrlThongTinCongTy_Load(object sender, EventArgs e)
        {

        }
    }
}
