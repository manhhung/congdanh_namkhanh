using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace PhotoAlbum
{
    public class AlbumManager
    {
        static private string defaultPath;
        static public string DefaultPath
        {
            get { return defaultPath; }
            set { defaultPath = value; }
        }

        static AlbumManager()
        {
            defaultPath = Environment.GetFolderPath(
            Environment.SpecialFolder.Personal)
            + @"\Albums";
        }

        private int pos = -1;
        private string name = String.Empty;
        private PhotoAlbum album;

        public AlbumManager()
        {
            album = new PhotoAlbum();
        }

        public AlbumManager(string name)
            : this()
        {
            name = name;
            // TODO: load the album
            throw new NotImplementedException();
        }

        public PhotoAlbum Album
        {
            get { return album; }
        }
        public string FullName
        {
            get { return name; }
            private set { name = value; }
        }

        public string ShortName
        {
            get
            {
                if (String.IsNullOrEmpty(FullName))
                    return null;
                else
                    return Path.
                     GetFileNameWithoutExtension(FullName);
            }
        }

        public Photo Current
        {
            get
            {
                if (Index < 0 || Index >= Album.Count)
                    return null;
                return Album[pos];
            }
        }
    }
}
