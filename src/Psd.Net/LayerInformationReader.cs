using System;
using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public class LayerAndMaskInformationReader
    {
        public LayerAndMaskInformationSection Read(Stream stream, FileVersion version)
        {
            var section = new LayerAndMaskInformationSection();
            var reader = new BigEndianBinaryReader(stream);

            section.Length = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
            section.Offset = stream.Position;

            // Layers information.
            section.LayersInformation = new LayersInformation();
            section.LayersInformation.Length = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
            section.LayersInformation.LayerCount = reader.ReadInt16();
            if (section.LayersInformation.LayerCount < 0)
            {
                section.LayersInformation.LayerCount *= -1;
                section.LayersInformation.IsFirstAlphaChannel = true;
            }

            section.LayersInformation.Offset = stream.Position;
            section.LayersInformation.Length -= 2; // subtract layer count bytes.

            // GlobalLayerMaskInformation
            stream.Position = section.LayersInformation.Offset + section.LayersInformation.Length;
            var globalLayerMaskInformationLength = reader.ReadInt32();
            if (globalLayerMaskInformationLength > 0)
            {
                section.GlobalLayerMaskInformation = new GlobalLayerMaskInformation();
                section.GlobalLayerMaskInformation.Length = globalLayerMaskInformationLength;
                section.GlobalLayerMaskInformation.OverlayColorSpace = reader.ReadInt16();
                section.GlobalLayerMaskInformation.ColorComponents = new short[4];
                for (int i = 0; i < section.GlobalLayerMaskInformation.ColorComponents.Length; i++)
                {
                    section.GlobalLayerMaskInformation.ColorComponents[i] = reader.ReadInt16();
                }

                section.GlobalLayerMaskInformation.Opacity = reader.ReadInt16();
                section.GlobalLayerMaskInformation.Kind = (GlobalLayerMaskInfoKind)reader.ReadByte();
                section.GlobalLayerMaskInformation.Offset = stream.Position;
            }

            // AdditionalLayerInformation
            stream.Position += globalLayerMaskInformationLength;
            if (stream.Position != section.Offset + section.Length) // if we did not reach the end of section, than there is additional data.
            {
                section.AdditionalLayerInformation = new AdditionalLayerInformation();
                section.AdditionalLayerInformation.Offset = stream.Position;
                section.AdditionalLayerInformation.Length = (section.Offset + section.Length) - section.AdditionalLayerInformation.Offset;
            }

            stream.Position = section.Offset + section.Length;

            return section;
        }
    }
}