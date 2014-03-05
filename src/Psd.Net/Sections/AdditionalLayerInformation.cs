namespace Psd.Net.Sections
{
    /// <summary>
    /// Additional layer information.
    /// </summary>
    public sealed class AdditionalLayerInformation : IDataSection
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
