using System;
using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public interface IFileHeaderReader
    {
        FileHeader Read(Stream stream);
    }

    public class FileHeaderReader : IFileHeaderReader
    {
        public FileHeader Read(Stream stream)
        {
            var header = new FileHeader();
            using (var reader = new BigEndianBinaryReader(stream))
            {
                header.Signature = new string(reader.ReadChars(4));
                header.Version = reader.ReadInt16();
                header.Reserved = reader.ReadBytes(6);
                header.ChannelCount = reader.ReadInt16();
                header.PixelHeight = reader.ReadInt32();
                header.PixelWidth = reader.ReadInt32();
                header.BitsPerChannel = reader.ReadInt16();
                header.ColorMode = (ColorMode)reader.ReadInt16();
            }

            return header;
        }

        
    }
}
