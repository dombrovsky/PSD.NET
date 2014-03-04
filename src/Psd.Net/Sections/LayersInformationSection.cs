namespace Psd.Net.Sections
{
    /// <summary>
    /// Layers information section.
    /// </summary>
    public sealed class LayersInformationSection : IDataSection
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
        /// Gets or sets the layer count.
        /// </summary>
        public short LayerCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the first alpha channel contains the transparency data for the merged result.
        /// </summary>
        public bool IsFirstAlphaChannel { get; set; }
    }
}
