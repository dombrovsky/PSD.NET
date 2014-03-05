using Psd.Net.Sections;
using System.Collections.Generic;
using System.IO;

namespace Psd.Net
{
    public class LayerRecordsReader
    {
        public List<LayerRecord> Read(Stream stream, LayersInformation layersInformation, FileVersion fileVersion)
        {
            var list = new List<LayerRecord>();

            var reader = new BigEndianBinaryReader(stream);
            stream.Position = layersInformation.Offset;

            for (int i = 0; i < layersInformation.LayerCount; i++)
            {
                var layerRecord = new LayerRecord();

                layerRecord.RectangleTop = reader.ReadInt32();
                layerRecord.RectangleLeft = reader.ReadInt32();
                layerRecord.RectangleBottom = reader.ReadInt32();
                layerRecord.RectangleRight = reader.ReadInt32();

                layerRecord.ChannelCount = reader.ReadInt16();
                layerRecord.Channels = new ChannelInformation[layerRecord.ChannelCount];
                for (int j = 0; j < layerRecord.ChannelCount; j++)
                {
                    layerRecord.Channels[j] = new ChannelInformation();
                    layerRecord.Channels[j].Id = reader.ReadInt16();
                    layerRecord.Channels[j].Length = fileVersion == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();
                }

                layerRecord.BlendModeSignature = new string(reader.ReadChars(4));
                layerRecord.BlendModeKey = new string(reader.ReadChars(4));
                layerRecord.Opacity = reader.ReadByte();
                layerRecord.Clipping = (Clipping)reader.ReadByte();

                var flags = reader.ReadByte();
                layerRecord.IsTransparencyProtected = flags.GetBit(0);
                layerRecord.IsVisible = flags.GetBit(1);
                layerRecord.IsObsolete = flags.GetBit(2);
                if (flags.GetBit(3))
                {
                    layerRecord.IsPixelIrrelevantToAppearance = flags.GetBit(4);
                }

                reader.ReadByte(); // filler

                layerRecord.Length = reader.ReadInt32();
                layerRecord.Offset = stream.Position;

                // TODO: Layer mask / adjustment layer data
                var maskLength = reader.ReadInt32();
                stream.Position += maskLength;

                // TODO: Layer blending ranges data
                var blendingLength = reader.ReadInt32();
                stream.Position += blendingLength;

                layerRecord.Name = reader.ReadPascalString();

                reader.ReadByte(); //???

                layerRecord.AdditionalLayerInformation = new AdditionalLayerInformation();
                layerRecord.AdditionalLayerInformation.Offset = stream.Position;
                layerRecord.AdditionalLayerInformation.Length = (layerRecord.Offset + layerRecord.Length) - layerRecord.AdditionalLayerInformation.Offset;

                stream.Position = layerRecord.Offset + layerRecord.Length;

                list.Add(layerRecord);
            }

            return list;
        }

    }
}
