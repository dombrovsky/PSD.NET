using System;
using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public class LayerAndMaskInformationReader
    {
        public LayerAndMaskInformationSection Read(Stream stream, FileVersion version)
        {
            var layerAndMaskInformationSection = new LayerAndMaskInformationSection();
            var reader = new BigEndianBinaryReader(stream);

            layerAndMaskInformationSection.Length = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
            layerAndMaskInformationSection.Offset = stream.Position;

            layerAndMaskInformationSection.LayersInformation = new LayersInformationSection();
            layerAndMaskInformationSection.LayersInformation.Length = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
            layerAndMaskInformationSection.LayersInformation.LayerCount = reader.ReadInt16();
            if (layerAndMaskInformationSection.LayersInformation.LayerCount < 0)
            {
                layerAndMaskInformationSection.LayersInformation.LayerCount *= -1;
                layerAndMaskInformationSection.LayersInformation.IsFirstAlphaChannel = true;
            }

            layerAndMaskInformationSection.LayersInformation.Offset = stream.Position;
            stream.Position = layerAndMaskInformationSection.Offset + layerAndMaskInformationSection.Length;

            return layerAndMaskInformationSection;
        }
    }
}