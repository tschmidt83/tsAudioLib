using System;
using System.Collections.Generic;
using System.Text;

namespace tsAudioLib.Categories
{
    /// <summary>
    /// Enumeration of possible audio types
    /// </summary>
    public enum CategoryType
    {
        /// <summary>
        /// Marks a raw, unprocessed audio sample
        /// </summary>
        Sample,

        /// <summary>
        /// Marks a processed audio effect
        /// </summary>
        Effect,

        /// <summary>
        /// Marks ambient/atmospheric recordings
        /// </summary>
        Atmo,

        /// <summary>
        /// This track is a whole song or a piece of actual music
        /// </summary>
        Music
    }
}
