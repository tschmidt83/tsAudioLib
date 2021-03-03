using System;
using System.Collections.Generic;
using System.Text;

namespace tsAudioLib
{
    /// <summary>
    /// This class represents an actual entry of the library
    /// </summary>
    public class AudioEntry
    {
        /// <summary>
        /// Audio category: sample, effect, atmo or music
        /// </summary>
        public Categories.CategoryType Category { get; set; }

        /// <summary>
        /// Date and time when the audio was recorded/created
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// Descriptive name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to the actual audio file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Collection of arbitrary keywords which are neither keyed nor categorized
        /// </summary>
        public List<string> Keywords { get; set; } = new List<string>();

        /// <summary>
        /// Retrieves a comma-separated enumeration of all keywords
        /// </summary>
        /// <returns>Comma-separated keyword enumeration</returns>
        public string GetKeywords()
        {
            string s = string.Empty;

            foreach(string word in Keywords)
                s += word + ", ";

            s = s.Trim();
            if (s.EndsWith(","))
                s = s.Remove(s.Length - 1);

            return s;
        }
    }
}
