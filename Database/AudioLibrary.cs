using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tsAudioLib.Database
{
    /// <summary>
    /// This class contains the actual library and methods for adding/removing/searching
    /// </summary>
    public class AudioLibrary
    {
        /// <summary>
        /// Collection of library entries
        /// </summary>
        public ObservableCollection<AudioEntry> Entries { get; private set; } = new ObservableCollection<AudioEntry>();

        /// <summary>
        /// Default (empty) constructor
        /// </summary>
        public AudioLibrary()
        {
        }

        /// <summary>
        /// Adds an entry to the library
        /// </summary>
        /// <param name="entry">Entry to add</param>
        public void AddEntry(AudioEntry entry)
        {
            if (entry != null)
            {
                if (!Entries.Contains(entry))
                {
                    Entries.Add(entry);
                    SaveLibrary();
                }
            }
        }

        /// <summary>
        /// Remove one specific entry from the library
        /// </summary>
        /// <param name="entry">Entry to remove</param>
        public void RemoveEntry(AudioEntry entry)
        {
            if (entry != null)
            {
                if (Entries.Contains(entry))
                {
                    Entries.Remove(entry);
                    SaveLibrary();
                }
            }
        }

        /// <summary>
        /// Load library from default location
        /// </summary>
        /// <returns>True if loaded successful</returns>
        public bool LoadLibrary()
        {
            XmlDatabase db = new XmlDatabase();

            if(db.Load(out IEnumerable<AudioEntry> entries))
            {
                Entries.Clear();

                foreach (AudioEntry e in entries)
                    Entries.Add(e);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Save library to default location
        /// </summary>
        /// <returns>True if saved successful</returns>
        public bool SaveLibrary()
        {
            XmlDatabase db = new XmlDatabase();
            return db.Save(Entries);
        }
    }
}
