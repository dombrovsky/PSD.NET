namespace Psd.Net.Sections
{
    /// <summary>
    /// Color mode data.
    /// </summary>
    public sealed class ColorModeData
    {
        /// <summary>
        /// Gets or sets the length of the following color data.
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// Gets or sets the color data.
        /// </summary>
        public byte[] ColorData { get; set; }
    }
}
