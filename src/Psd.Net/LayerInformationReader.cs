using System;
using System.IO;

namespace Psd.Net
{
    public class LayerInformationReader
    {
        public void Read(Stream stream, FileVersion version)
        {
            var reader = new BigEndianBinaryReader(stream);

            long length = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
            var startPosition = stream.Position;

            long layersInfoLength = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
            short layersCount = Math.Abs(reader.ReadInt16());

            stream.Position = startPosition + length;
        }
    }
}