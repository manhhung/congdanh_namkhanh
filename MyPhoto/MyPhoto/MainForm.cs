using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PhotoAlbum;
using MyPhotoControls;

namespace MyPhoto
{
    public partial class MainForm : Form
    {
        private AlbumManager _manager;
        private AlbumManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public MainForm()
        {
            InitializeComponent();
            NewAlbum();
            mnuView.DropDown = ctxMenuPhoto;
            
        }

        private void DisplayAlbum()
        {
            pbxPhoto.Image = Manager.CurrentImage;
            SetStatusStrip();
            SetTitleBar();
            Point p = pbxPhoto.PointToClient(Form.MousePosition);
            UpdatePixelDialog(p.X, p.Y);
        }

        private void menuFileNew_Click
            (object sender, System.EventArgs e)
        {
            NewAlbum();
        }

        private void NewAlbum()
        {
            if (Manager == null || SaveAndCloseAlbum())
            {
                // Album closed, create a new one
                Manager = new AlbumManager();
                DisplayAlbum();
            }
        }
        

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void SetStatusStrip()
        {
            if (pbxPhoto.Image !=null)
            {
                statusInfo.Text = Manager.Current.Caption;
                statusImageSize.Text = String.Format("{0:#} x {1:#}", pbxPhoto.Image.Width, pbxPhoto.Image.Height);
                statusAlbumPos.Text = String.Format(" {0:0}/{1:0} ",
                Manager.Index + 1,
                Manager.Album.Count);
            }
            else
            {
                statusInfo.Text=null;
                statusImageSize.Text=null;
                statusAlbumPos.Text=null;

            }
        }
        private void ProcessImageOpening(ToolStripDropDownItem parent)
        {
            if (parent != null)
            {
                string enumVal = pbxPhoto.SizeMode.ToString();
                foreach (ToolStripMenuItem item in parent.DropDownItems)
                {
                    item.Enabled= (pbxPhoto.Image != null);
                    item.Checked = item.Tag.Equals(enumVal);
                }
            }
        }

        private void mnuImage_DropDownOpening(object sender, EventArgs e)
        {
            ProcessImageOpening(sender as ToolStripDropDownItem);
        }

        private void mnuImage_Click(object sender, EventArgs e)
        {

        }
        private void ProcessImageClick(ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            string enumVal = item.Tag as string;
            if (enumVal !=null)
            {
                pbxPhoto.SizeMode=(PictureBoxSizeMode) Enum.Parse(typeof(PictureBoxSizeMode),enumVal);
            }
        }

        private void mnuImage_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProcessImageClick(e);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Album";
            dlg.Filter = "Album files (*.abm)|*.abm|" + "All files (*.*)|*.*";
            dlg.InitialDirectory = AlbumManager.DefaultPath;
            dlg.RestoreDirectory = true;
           
            if (dlg.ShowDialog() == DialogResult.OK)
               {
                   string path = dlg.FileName;
                   // Close any existing album
                   if (!SaveAndCloseAlbum())
                       return; // Close canceled
                   try
                   {
                       
                       // Open the new album                  
                       Manager = new AlbumManager(path);
                   }
                   catch (AlbumStorageException aex)
                   {
                       string msg = String.Format("Unableto open album file {0}\n({1})",path, aex.Message);
                       MessageBox.Show(msg, "Unable to Open");
                       Manager = new AlbumManager();
                       
                   }
                     DisplayAlbum();
               
               }
             dlg.Dispose();
        
        }

        private void SaveAlbum(string name)
        {
            try
            {
                Manager.Save(name, true);
            }
            catch (AlbumStorageException aex)
            {
                string msg = String.Format("Unable to save album {0} ({1})\n\n"+ "Do you wish to save the album "
                    + "under an alternate name?",name, aex.Message);
                DialogResult result = MessageBox.Show(msg, "Unable to Save", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
                if (DialogResult == DialogResult.Yes)
                    SaveAsAlbum();
            }
        }
        private void SaveAlbum()
        {
            if (String.IsNullOrEmpty(Manager.FullName))
            SaveAsAlbum();
            else
                {
                    // Save the album under the existing name
                    SaveAlbum(Manager.FullName);
                }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveAlbum();
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveAsAlbum();
        }

        private void SaveAsAlbum()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Album";
            dlg.DefaultExt = "abm";
            dlg.Filter = "Album files (*.abm)|*.abm|"
            + "All files|*.*";
            dlg.InitialDirectory
            = AlbumManager.DefaultPath;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SaveAlbum(dlg.FileName);
                // Update title bar to include new name
        //chua lam thanh tieu de        
                SetTitleBar();
            }
            dlg.Dispose();
        }
        private void SetTitleBar()
        {
            Version ver
            = new Version(Application.ProductVersion);
            string name = Manager.FullName;
            Text = String.Format("{2} - MyPhotos {0:0}.{1:0}",
            ver.Major, ver.Minor,String.IsNullOrEmpty(name) ? "Untitled" : name);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuEditAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Add Photos";
            dlg.Multiselect = true;
            dlg.Filter
            = "Image Files (JPEG, GIF, BMP, etc.)|"
             + "*.jpg;*.jpeg;*.gif;*.bmp;"
             + "*.tif;*.tiff;*.png|"
            + " JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
            + "GIF files (*.gif)|*.gif|"
            + "BMP files (*.bmp)|*.bmp|"
            + " TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
            + "PNG files (*.png)|*.png|"
             + "All files (*.*)|*.*";
            dlg.InitialDirectory
            = Environment.CurrentDirectory;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = dlg.FileNames;
                int index = 0;
                foreach (string s in files)
                {
                    Photo photo = new Photo(s);
                    // Add the file (if not already present)
                    index = Manager.Album.IndexOf(photo);
                    if (index < 0)
                        Manager.Album.Add(photo);
                    else
                        photo.Dispose(); // photo already there
                }
            }

            Manager.Index = Manager.Album.Count - 1;
            dlg.Dispose();
            DisplayAlbum();
        }

        private void mnuEditRemove_Click(object sender, EventArgs e)
        {
            if (Manager.Album.Count > 0)
            {
                Manager.Album.RemoveAt(Manager.Index);
                DisplayAlbum();
            }
        }

        private void mnuNext_Click(object sender, EventArgs e)
        {
            if (Manager.Index < Manager.Album.Count - 1)
            {
                Manager.Index++;
                DisplayAlbum();
            }
        }

        private void mnuPrevious_Click(object sender, EventArgs e)
        {
            if (Manager.Index > 0)
            {
                Manager.Index--;
                DisplayAlbum();
            }
        }

        private void ctxMenuPhoto_Opening(object sender, CancelEventArgs e)
        {
            mnuNext.Enabled  = (Manager.Index < Manager.Album.Count - 1);
            mnuPrevious.Enabled = (Manager.Index > 0);
        }
        private bool SaveAndCloseAlbum()
        {
            if (Manager.Album.HasChanged)
            {
                string msg;
                if (String.IsNullOrEmpty(Manager.FullName))
                    msg = "Do you wish to save your changes?";
                else
                    msg = String.Format("Do you wish to "
                    + "save your changes to \n{0}?",
                    Manager.FullName);
                DialogResult result = MessageBox.Show(this, msg,"Save Changes?",MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    SaveAlbum();
                else if (result == DialogResult.Cancel)
                    return false; // do not close
                
            }
            // Close the album and return true
            if (Manager.Album != null)
                Manager.Album.Dispose();
            Manager = new AlbumManager();
            SetTitleBar();
            return true;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !SaveAndCloseAlbum();
            base.OnFormClosing(e);
        }
        private PixelDialog _dlgPixel = null;
        private PixelDialog PixelForm
        {
            get { return _dlgPixel; }
            set { _dlgPixel = value; }
        }

        private void mnuPixelData_Click(object sender, EventArgs e)
        {
            if (PixelForm == null || PixelForm.IsDisposed)
            {
                PixelForm = new PixelDialog();
                PixelForm.Owner = this;
            }
            PixelForm.Show();
            Point p = pbxPhoto.PointToClient(Form.MousePosition);
            UpdatePixelDialog(p.X, p.Y);
        }
        private void UpdatePixelDialog(int x, int y)
        {
            if (PixelForm != null && PixelForm.Visible)
            {
                Bitmap bmp = Manager.CurrentImage;
                PixelForm.Text = (Manager.Current == null ? "Pixel Data" : Manager.Current.Caption);
                if (bmp == null || !pbxPhoto.DisplayRectangle.Contains(x, y))
                    PixelForm.ClearPixelData();
                else
                    PixelForm.UpdatePixelData(x, y, bmp,
                    pbxPhoto.DisplayRectangle,
                    new Rectangle(0, 0,
                    bmp.Width, bmp.Height),
                    pbxPhoto.SizeMode);
            }
           
        }

        private void pbxPhoto_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePixelDialog(e.X, e.Y);
        }
    }
}
