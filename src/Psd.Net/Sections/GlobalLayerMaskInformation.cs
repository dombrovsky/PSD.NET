namespace Psd.Net.Sections
{
    /// <summary>
    /// Global layer mask info kind.
    /// </summary>
    public enum GlobalLayerMaskInfoKind : byte
    {
        /// <summary>
        /// The color selected i.e. inverted.
        /// </summary>
        ColorSelected = 0,

        /// <summary>
        /// The color protected.
        /// </summary>
        ColorProtected = 1,

        /// <summary>
        /// Use value stored per layer. This value is preferred. The others are for backward compatibility with beta versions.
        /// </summary>
        PerLayer = 128
    }

    public sealed class GlobalLayerMaskInformation : IDataSection
    {
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets the overlay color space (undocumented).
        /// </summary>
        public short OverlayColorSpace { get; set; }

        /// <summary>
        /// Gets or sets 4 * 2 byte color components.
        /// </summary>
        public short[] ColorComponents { get; set; }

        /// <summary>
        /// Gets or sets the opacity. 0 = transparent, 100 = opaque.
        /// </summary>
        public short Opacity { get; set; }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        public GlobalLayerMaskInfoKind Kind { get; set; }
    }
}
