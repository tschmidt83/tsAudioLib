using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tsAudioLib.Database
{
    /// <summary>
    ///  Simple XML-based library
    /// </summary>
    public class XmlDatabase
    {
        /// <summary>
        /// Default (empty) constructor
        /// </summary>
        public XmlDatabase()
        {
        }

        /// <summary>
        /// Save database to xml file
        /// </summary>
        /// <param name="entries">IEnumerable of entries to save</param>
        /// <returns>True if successul</returns>
        public bool Save(IEnumerable<AudioEntry> entries)
        {
            string dbName = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\tsAudioData\AudioLib.xml";

            FileInfo fi = new FileInfo(dbName);

            // Check if application directory exists. If not, create.
            if (!Directory.Exists(fi.DirectoryName))
            {
                try
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                catch
                {
                    return false;
                }
            }

            // Save database file
            try
            {
                if (entries != null)
                {
                    // Save as List<AudioEntry>
                    List<AudioEntry> e = new List<AudioEntry>(entries);
                    XmlSerializer ser = new XmlSerializer(typeof(List<AudioEntry>));

                    using (StreamWriter writer = new StreamWriter(dbName, false))
                    {
                        ser.Serialize(writer, e);
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Load library entries from XML file
        /// </summary>
        /// <param name="entries">(out) IEnumerable of loaded entries</param>
        /// <returns>True if loaded successful</returns>
        public bool Load(out IEnumerable<AudioEntry> entries)
        {
            entries = null;
            string dbName = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\tsAudioData\AudioLib.xml";

            FileInfo fi = new FileInfo(dbName);

            // Check if application directory exists. If not, create.
            if (Directory.Exists(fi.DirectoryName))
            {
                try
                {
                    // Load as List<AudioEntry>
                    List<AudioEntry> e;
                    XmlSerializer ser = new XmlSerializer(typeof(List<AudioEntry>));

                    using (StreamReader reader = new StreamReader(dbName))
                    {
                        e = (List<AudioEntry>)ser.Deserialize(reader);
                    }

                    entries = e;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
