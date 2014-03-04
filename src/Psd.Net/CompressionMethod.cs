namespace Psd.Net
{
    /// <summary>
    /// Compression method.
    /// </summary>
    public enum CompressionMethod : short
    {
        /// <summary>
        /// Raw image data.
        /// </summary>
        Raw = 0,

        /// <summary>
        /// RLE compressed the image data starts with the byte counts for all the scan lines (rows * channels), with each count stored as a two-byte value.
        /// </summary>
        Rle = 1,

        /// <summary>
        /// ZIP without prediction.
        /// </summary>
        Zip = 2,

        /// <summary>
        /// ZIP with prediction.
        /// </summary>
        ZipPrediction = 3
    }
}
