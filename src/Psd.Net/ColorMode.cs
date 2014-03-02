namespace Psd.Net
{
    /// <summary>
    /// The color mode of the file.
    /// </summary>
    public enum ColorMode : short
    {
        /// <summary>
        /// The bitmap.
        /// </summary>
        Bitmap = 0,

        /// <summary>
        /// The grayscale.
        /// </summary>
        Grayscale = 1,

        /// <summary>
        /// The indexed.
        /// </summary>
        Indexed = 2,

        /// <summary>
        /// The RGB.
        /// </summary>
        RGB = 3,

        /// <summary>
        /// The cmyk.
        /// </summary>
        CMYK = 4,

        /// <summary>
        /// The multichannel.
        /// </summary>
        Multichannel = 7,

        /// <summary>
        /// The duotone.
        /// </summary>
        Duotone = 8,

        /// <summary>
        /// The lab.
        /// </summary>
        Lab = 9
    }
}
