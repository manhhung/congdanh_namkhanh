using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PhotoAlbum
{
    public class PhotoAlbum: Collection<Photo>,IDisposable
    {
        private bool hasChanged = false;
        public bool HasChanged
        {
            get {
                if (hasChanged) return true;
                foreach (Photo p in this)
                    if (p.HasChanged) return true;
                return false;
            }

            set {
                hasChanged = value;
                if (value == false)
                    foreach (Photo p in this)
                        p.HasChanged = false;
            }
        }
        public void Dispose()
        {
            foreach (Photo p in this)
                p.Dispose();
        }
        public Photo Add(string fileName)
        {
            Photo p = new Photo(fileName);
            base.Add(p);
            return p;
        }

        protected override void ClearItems()
        {
            if (Count > 0)
            {
                Dispose();
                base.ClearItems();
                HasChanged = true;
            }
        }

        protected override void InsertItem(int index, Photo item)
        {
            HasChanged = true;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Items[index].Dispose();
            hasChanged = true;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, Photo item)
        {
            HasChanged = true;
            base.SetItem(index, item);
        }
    }
}
