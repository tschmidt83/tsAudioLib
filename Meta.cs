using System;
using System.Collections.Generic;
using System.Text;

namespace tsAudioLib
{
    /// <summary>
    /// This class represents a collection of uncategorized metadata.
    /// The actual metadata is represented as a dictionary, this class wraps some methods around it for easier access.
    /// </summary>
    public class Meta
    {
        // TODO: check if dictionary or list<keyvaluepair> is better for quick searching
        /// <summary>
        /// Actual metadata
        /// </summary>
        public Dictionary<string, string> Data { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Adds a metadata entry, if no entry with that key exists
        /// </summary>
        /// <param name="key">Metadata key</param>
        /// <param name="value">Metadata value</param>
        /// <returns>True if added successfully</returns>
        public bool Add(string key, string value)
        {
            if (Data.ContainsKey(key))
                return false;

            Data.Add(key, value);
            return true;
        }
    }
}
