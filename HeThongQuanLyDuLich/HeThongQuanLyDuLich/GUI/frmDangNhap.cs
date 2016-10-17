using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeThongQuanLyDuLich.GUI
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ServiceFull.Service1 sv = new ServiceFull.Service1();
            if (sv.checkLogin(txtTaiKhoan.Text, txtMatKhau.Text) > 0)
            {
                GUI.frmMain frmmain = new frmMain();
                frmmain.ShowDialog();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
        }
    }
}
