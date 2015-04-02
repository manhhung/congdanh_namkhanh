using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPhoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mnuView.DropDown = ctxMenuPhoto;
            SetStatusStrip(null);
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Photo";
            dlg.Filter = "jpg files (*.jpg)|*.jpg|All Files (*.*) | *.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbxPhoto.Image = new Bitmap(dlg.OpenFile());
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Unable to load files" + ex.Message);
                    pbxPhoto.Image = null;
                }
                SetStatusStrip(dlg.FileName);
            }
            dlg.Dispose();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void SetStatusStrip(string path)
        {
            if (pbxPhoto.Image !=null)
            {
                statusInfo.Text = path;
                statusImageSize.Text = String.Format("{0:#} x {1:#}", pbxPhoto.Image.Width, pbxPhoto.Image.Height);
            }
            else
            {
                statusInfo.Text=null;
                statusImageSize.Text=null;
                statusAlbumPos.Text=null;

            }
        }
            
       
    }
}
