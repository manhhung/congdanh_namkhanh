using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PhotoAlbum
{
    public class Photo: IDisposable
    {
        private string fileName;
        public string FileName
        {
            get { return this.fileName; }
        }
        private Bitmap bitmap;
        public Bitmap Image
        {
            get
            {
                if (bitmap == null)
                    bitmap = new Bitmap(fileName);
                return bitmap;
            }
        }
        private string caption = "";
        public string Caption
        {
            get { return caption; }
            set
            {
                if (caption != value)
                {
                    caption = value;
                    HasChanged = true;
                }
            }
        }
        private string photographer = "";
        public string Photographer
        {
            get { return photographer; }
            set
            {
                if (photographer != value)
                {
                    photographer = value;
                    HasChanged = true;
                }
            }
        }
        private DateTime dateTaken = DateTime.Now;
        public DateTime DateTaken
        {
            get
            {
                return dateTaken;}
            set 
            {
                if (dateTaken !=value)
                {dateTaken=value;
                HasChanged=true;
            }
        }
        }
        private string notes = "";
        public string Notes
        {
            get { return notes; }
            set
            {
                if (notes != value)
                {
                    notes = value;
                    HasChanged = true;
                }
            }
        }
        public void ReleaseImage()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
        }
        public void Dispose()
        {
            ReleaseImage();
        }
        private bool hasChanged = true;
        
        public Photo(string fileName)
        {
            this.fileName = fileName;
            bitmap = null;
            caption = System.IO.Path.GetFileNameWithoutExtension(fileName);

        }
        public bool HasChanged
        {
            get { return hasChanged; }
            set { hasChanged = value; }
        }

        public override string ToString()
        {
            return FileName;
        }

        public override int GetHashCode()
        {
        return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            
            return GetHashCode() == obj.GetHashCode();
        }


    }
}
