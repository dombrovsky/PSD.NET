namespace Psd.Net.Sections
{
    /// <summary>
    /// Color mode data.
    /// </summary>
    public sealed class ColorModeData : IDataSection
    {
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the length of the following color data.
        /// </summary>
        public long Length { get; set; }
    }
}
