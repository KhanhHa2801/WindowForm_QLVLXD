using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formLogin_Register
{
    public partial class ctrlHeThong : UserControl
    {
        private static ctrlHeThong _instance;

        public static ctrlHeThong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ctrlHeThong();
                return _instance;
            }
        }
        public ctrlHeThong()
        {
            InitializeComponent();
        }

        private void UserControl_test_Load(object sender, EventArgs e)
        {

        }
    }
}
