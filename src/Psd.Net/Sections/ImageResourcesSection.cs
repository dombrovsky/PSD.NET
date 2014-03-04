namespace Psd.Net.Sections
{
    /// <summary>
    /// Contains image resources.
    /// </summary>
    public sealed class ImageResourcesSection : IDataSection
    {
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
