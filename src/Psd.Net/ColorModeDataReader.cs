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
            
            colorModeData.DataLength = reader.ReadInt32();
            colorModeData.ColorData = reader.ReadBytes(colorModeData.DataLength);
            

            return colorModeData;
        }
    }
}