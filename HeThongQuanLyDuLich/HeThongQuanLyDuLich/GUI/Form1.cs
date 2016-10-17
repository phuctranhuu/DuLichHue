using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HeThongQuanLyDuLich.ServiceFull;
namespace HeThongQuanLyDuLich
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThemLanguage_Click(object sender, EventArgs e)
        {
            string name = txtTenNgonNgu.Text.ToString();
            ServiceFull.Service1 sv = new ServiceFull.Service1();
            sv.insertLanguage(name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceFull.Service1 sv = new ServiceFull.Service1();
            bool kt = sv.insertCity();
            if (kt == true)
            {
                MessageBox.Show("Thêm city thành công");
            }
            else
            {
                MessageBox.Show("Thêm city thất bại");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceFull.Service1 sv = new ServiceFull.Service1();
            string idCity = "51389905-19c5-47c2-871d-7b70b4e20881";
            string idLang = "88b7f1fd-f667-482f-8890-9c6909f9c9c0";
            Guid city = new Guid(idCity);
            Guid lang = new Guid(idLang);
            bool kt = sv.insertCityLang(city,lang,"Huế");
            if (kt == true)
            {
                MessageBox.Show("Thêm city lang thành công");
            }
            else
            {
                MessageBox.Show("Thêm city lang thất bại");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
