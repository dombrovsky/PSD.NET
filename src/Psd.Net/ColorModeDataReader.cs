using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public class ColorModeDataReader
    {
        public ColorModeData Read(Stream stream)
        {
            var colorModeData = new ColorModeData();
            var reader = new BigEndianBinaryReader(stream);
            
            colorModeData.Length = reader.ReadInt32();
            colorModeData.Offset = stream.Position;

            stream.Position += colorModeData.Length;

            return colorModeData;
        }
    }
}