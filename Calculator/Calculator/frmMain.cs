using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Calculator
{
    public partial class frmMain : Form
    {
        bool isTypingNumber = true;
        bool isThapPhan = true;
        bool isBegin = true;
        enum PhepToan { Cong, Tru, Nhan, Chia };
        PhepToan pheptoan;
        double nho =0.0;

        public frmMain()
        {
            InitializeComponent();
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        
        
//Cac phep toan
        private void btnCong_Click(object sender, EventArgs e)
        {
            isTypingNumber = false;
            pheptoan = PhepToan.Cong;
            this.btnBang_Click(sender, e);
            nho = double.Parse(lblDisplay.Text);
        }

        private void btnTru_Click(object sender, EventArgs e)
        {
            isTypingNumber = false;
            pheptoan = PhepToan.Tru;
            this.btnBang_Click(sender, e);
            nho = double.Parse(lblDisplay.Text);
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            isTypingNumber = false;
            pheptoan = PhepToan.Nhan;
            this.btnBang_Click(sender, e);
            nho = double.Parse(lblDisplay.Text);
        }

        private void btnChia_Click(object sender, EventArgs e)
        {
            isTypingNumber = false;
            pheptoan = PhepToan.Chia;
            this.btnBang_Click(sender, e);
            nho = double.Parse(lblDisplay.Text);
        }

        private void btnThapPhan_Click(object sender, EventArgs e)
        {
            if (isThapPhan)
            { lblDisplay.Text = lblDisplay.Text + btnThapPhan.Text; isThapPhan = false; }
        }

        private double TinhKetQua()
        {
            
            double ketqua = 0.0;
            switch (pheptoan)
            {
                case PhepToan.Cong:
                    ketqua = nho + double.Parse(lblDisplay.Text); break;
                case PhepToan.Tru:
                    ketqua = nho - double.Parse(lblDisplay.Text); break;
                case PhepToan.Nhan:
                    ketqua = nho * double.Parse(lblDisplay.Text); break;
                case PhepToan.Chia:
                    ketqua = nho / double.Parse(lblDisplay.Text); break;
            }
    
            lblDisplay.Text = Convert.ToString(ketqua);
            return ketqua; 
        }

        private void btnBang_Click(object sender, EventArgs e)
        {

            isTypingNumber = false;
            TinhKetQua();
        }

        private void NhapSo(object sender, EventArgs e)
        {

            if (isTypingNumber)
            {
                if (isBegin)
                {
                    lblDisplay.Text = "";
                    lblDisplay.Text = lblDisplay.Text + ((Button)sender).Text;
                    isBegin = false;
                }
                else lblDisplay.Text = lblDisplay.Text + ((Button)sender).Text;
            }
            else
            {
                lblDisplay.Text = ((Button)sender).Text;
                isTypingNumber = true;
            }
        }

        private void NhapPhepToan(object sender, EventArgs e)
        {
            isThapPhan = true;
            isTypingNumber = false;
            switch (((Button)sender).Text)
            {
                case "+":
                    pheptoan=PhepToan.Cong;break;
                case "-":
                    pheptoan = PhepToan.Tru;break;
                case "*":
                    pheptoan = PhepToan.Nhan;break;
                case "/":
                    pheptoan = PhepToan.Chia;break;               
            }

            //TinhKetQua();
            nho = double.Parse(lblDisplay.Text, CultureInfo.InvariantCulture);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "0.";
            isTypingNumber = true;
            isThapPhan = true;
            isBegin = true;
        }

        private void btnPhanTram_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = (nho * double.Parse(lblDisplay.Text) / 100).ToString();

        }
   
    }
}
