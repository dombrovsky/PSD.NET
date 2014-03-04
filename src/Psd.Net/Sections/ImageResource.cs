namespace Psd.Net.Sections
{
    /// <summary>
    /// Image resources are used to store non-pixel data associated with images, such as pen tool paths.
    /// </summary>
    public class ImageResource
    {
        /// <summary>
        /// Gets or sets the signature. Should be '8BIM'.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the resource.
        /// </summary>
        public ImageResourceId Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the length of the data.
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
