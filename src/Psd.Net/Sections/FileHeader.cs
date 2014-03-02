namespace Psd.Net.Sections
{
    /// <summary>
    /// The file header contains the basic properties of the image.
    /// </summary>
    internal sealed class FileHeader
    {
        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <remarks>Signature: always equal to '8BPS'. Do not try to read the file if the signature does not match this value.</remarks>
        public byte[] Signature { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <remarks>Version: always equal to 1. Do not try to read the file if the version does not match this value. (**PSB** version is 2.)</remarks>
        public short Version { get; set; }

        /// <summary>
        /// Gets or sets the number of channels in the image, including any alpha channels. Supported range is 1 to 56.
        /// </summary>
        public short ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the height of the image in pixels. Supported range is 1 to 30,000. (**PSB** max of 300,000.).
        /// </summary>
        public int PixelHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of the image in pixels. Supported range is 1 to 30,000. (**PSB** max of 300,000.).
        /// </summary>
        public int PixelWidth { get; set; }

        /// <summary>
        /// Gets or sets the bits per channel. Supported values are 1, 8, 16 and 32.
        /// </summary>
        public short BitsPerChannel { get; set; }

        /// <summary>
        /// Gets or sets the color mode of the file.
        /// </summary>
        public ColorMode ColorMode { get; set; }
    }
}
