namespace Psd.Net
{
    /// <summary>
    /// <see cref="byte"/> extensions.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Gets the bit.
        /// </summary>
        /// <param name="b">The byte.</param>
        /// <param name="bitNumber">The bit number.</param>
        /// <returns>True if bit is equal to 1, otherwise False.</returns>
        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
    }
}
