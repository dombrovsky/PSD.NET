namespace Psd.Net.Sections
{
    /// <summary>
    /// Layer clipping.
    /// </summary>
    public enum Clipping
    {
        /// <summary>
        /// The base.
        /// </summary>
        Base = 0,

        /// <summary>
        /// The non base.
        /// </summary>
        NonBase = 1
    }

    /// <summary>
    /// Layer record.
    /// </summary>
    public sealed class LayerRecord : IDataSection
    {
        /// <summary>
        /// Gets or sets the rectangle top.
        /// </summary>
        public int RectangleTop { get; set; }

        /// <summary>
        /// Gets or sets the rectangle left.
        /// </summary>
        public int RectangleLeft { get; set; }

        /// <summary>
        /// Gets or sets the rectangle bottom.
        /// </summary>
        public int RectangleBottom { get; set; }

        /// <summary>
        /// Gets or sets the rectangle right.
        /// </summary>
        public int RectangleRight { get; set; }

        /// <summary>
        /// Gets or sets the number of channels in the layer.
        /// </summary>
        public short ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the channels information.
        /// </summary>
        public ChannelInformation[] Channels { get; set; }

        /// <summary>
        /// Gets or sets the blend mode signature. Should be '8BIM'.
        /// </summary>
        public string BlendModeSignature { get; set; }

        /// <summary>
        /// Gets or sets the blend mode key.
        /// </summary>
        public string BlendModeKey { get; set; }

        /// <summary>
        /// Gets or sets the opacity. 0 = transparent ... 255 = opaque.
        /// </summary>
        public byte Opacity { get; set; }

        /// <summary>
        /// Gets or sets the clipping.
        /// </summary>
        public Clipping Clipping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether transparency protected.
        /// </summary>
        public bool IsTransparencyProtected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether layer is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether layer is obsolete.
        /// </summary>
        public bool IsObsolete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pixel data irrelevant to appearance of document.
        /// </summary>
        public bool? IsPixelIrrelevantToAppearance { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the offset.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets the additional layer information.
        /// </summary>
        public AdditionalLayerInformation AdditionalLayerInformation { get; set; }
    }
}
