﻿namespace Psd.Net.Sections
{
    /// <summary>
    /// Layer and mask information section.
    /// </summary>
    public sealed class LayerAndMaskInformationSection : IDataSection
    {
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets the layers information.
        /// </summary>
        public LayersInformationSection LayersInformation { get; set; }
    }
}