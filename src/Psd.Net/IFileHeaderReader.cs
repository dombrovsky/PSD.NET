using System;
using System.Collections.Generic;
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
            var reader = new BigEndianBinaryReader(stream);
            
                header.Signature = new string(reader.ReadChars(4));
                header.Version = (FileVersion)reader.ReadInt16();
                header.Reserved = reader.ReadBytes(6);
                header.ChannelCount = reader.ReadInt16();
                header.PixelHeight = reader.ReadInt32();
                header.PixelWidth = reader.ReadInt32();
                header.BitsPerChannel = reader.ReadInt16();
                header.ColorMode = (ColorMode)reader.ReadInt16();
            

            return header;
        }
    }

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

    public class ImageResourcesReader
    {
        public List<ImageResource> Read(Stream stream)
        {
            var list = new List<ImageResource>();

            var reader = new BigEndianBinaryReader(stream);
            var length = reader.ReadInt32();
            var startPosition = reader.BaseStream.Position;

            while (reader.BaseStream.Position < startPosition + length)
            {
                var imageResource = new ImageResource();

                imageResource.Signature = new string(reader.ReadChars(4));
                imageResource.Id = (ImageResourceId)reader.ReadInt16();

                //read pascal string
                var stringLength = reader.ReadByte();
                if ((stringLength%2) != 0 || stringLength == 0)
                {
                    reader.ReadByte();
                }

                imageResource.Name = new string(reader.ReadChars(stringLength));

                imageResource.DataLength = reader.ReadInt32();

                if ((imageResource.DataLength%2) != 0)
                {
                    imageResource.DataLength++;   
                }

                imageResource.Data = reader.ReadBytes(imageResource.DataLength);

                list.Add(imageResource);
            }

            return list;
        }
    }

    public class LayerInformationReader
    {
        public void Read(Stream stream, FileVersion version)
        {
            var reader = new BigEndianBinaryReader(stream);

            long length = version == FileVersion.Psd ? reader.ReadInt32() : reader.ReadInt64();

            stream.Position += length;
        }
    }
}
