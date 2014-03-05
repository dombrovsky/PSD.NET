namespace Psd.Net.Sections
{
    /// <summary>
    /// Contains the image pixel data. Image data is stored in planar order: first all the red data, then all the green data, etc. Each plane is stored in scan-line order, with no pad bytes.
    /// </summary>
    public sealed class ImageDataSection : IDataSection
    {
        /// <summary>
        /// Gets or sets the compression method.
        /// </summary>
        public CompressionMethod CompressionMethod { get; set; }

        /// <summary>
        /// Gets the offset.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public long Length { get; set; }
    }
}
