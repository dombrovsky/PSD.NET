namespace Psd.Net.Sections
{
    /// <summary>
    /// Represents offset and data length within the stream.
    /// </summary>
    public interface IDataSection
    {
        /// <summary>
        /// Gets the offset.
        /// </summary>
        long Offset { get; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        long Length { get; }
    }
}
