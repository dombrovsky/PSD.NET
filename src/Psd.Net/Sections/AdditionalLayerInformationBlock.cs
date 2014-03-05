namespace Psd.Net.Sections
{
    /// <summary>
    /// Additional layer information block.
    /// </summary>
    public sealed class AdditionalLayerInformationBlock : IDataSection
    {
        /// <summary>
        /// Gets or sets the signature. '8BIM' or '8B64'.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

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
