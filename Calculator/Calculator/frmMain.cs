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
        bool isTypingNumber = false;
        enum PhepToan { Cong, Tru, Nhan, Chia,CanBacHai };
        PhepToan pheptoan;
        double nho =0.0;

        public frmMain()
        {
            InitializeComponent();
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

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
                case PhepToan.CanBacHai:
                    ketqua=nho; break;
                
            }
            lblDisplay.Text = ketqua.ToString();
           
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
                    lblDisplay.Text = lblDisplay.Text + ((Button)sender).Text;                 
            }
            else
            {
                lblDisplay.Text = ((Button)sender).Text;
                isTypingNumber = true;
            }
        }

        private void NhapPhepToan(object sender, EventArgs e)
        {
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
                case "√":
                    pheptoan = PhepToan.CanBacHai;
                    lblDisplay.Text=Math.Sqrt(double.Parse(lblDisplay.Text)).ToString(); break;
                                       
            }

            nho = double.Parse(lblDisplay.Text, CultureInfo.InvariantCulture);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "0.";
            isTypingNumber = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text.Length != 1 && lblDisplay.Text !="0.")
            {
                lblDisplay.Text = lblDisplay.Text.Remove(lblDisplay.Text.Length - 1);
            }
            else lblDisplay.Text = "0.";
        }

        private bool CheckThapPhan(string str)
        {
            if (str.Contains(".")) return true;
            return false;
        }
        private void btnThapPhan_Click(object sender, EventArgs e)
        {
            if (CheckThapPhan(lblDisplay.Text)==false)
            { lblDisplay.Text = lblDisplay.Text + btnThapPhan.Text; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = (nho * double.Parse(lblDisplay.Text) / 100).ToString();
        }

        private bool CheckAmDuong(string str)
        {
            if (str.Contains("-")) return false;
            return true;
        }

        private void btnAmDuong_Click(object sender, EventArgs e)
        {
            if (CheckAmDuong(lblDisplay.Text)) lblDisplay.Text = "-"+ lblDisplay.Text;
            else lblDisplay.Text=lblDisplay.Text.Remove(0, 1);
        }
       
    }
}
