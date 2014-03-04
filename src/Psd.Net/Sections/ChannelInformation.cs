namespace Psd.Net.Sections
{
    /// <summary>
    /// Channel information.
    /// </summary>
    public sealed class ChannelInformation
    {
        /// <summary>
        /// Gets or sets the identifier. 0 = red, 1 = green, etc.; -1 = transparency mask; -2 = user supplied layer mask, -3 real user supplied layer mask (when both a user mask and a vector mask are present).
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        /// Gets or sets the length of corresponding channel data.
        /// </summary>
        public long Length { get; set; }
    }
}
