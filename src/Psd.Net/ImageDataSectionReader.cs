using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public class ImageDataSectionReader
    {
        public ImageDataSection Read(Stream stream)
        {
            var reader = new BigEndianBinaryReader(stream);
            var section = new ImageDataSection();

            section.CompressionMethod = (CompressionMethod)reader.ReadInt16();
            section.Offset = stream.Position;
            section.Length = stream.Length - section.Offset;

            return section;
        }
    }
}
