using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public class ImageDataReader
    {
        public void Read(Stream stream, FileHeader header)
        {
            var reader = new BigEndianBinaryReader(stream);

            var compression = (CompressionMethod)reader.ReadInt16();

           

            
        }
    }
}
