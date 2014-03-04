using System.Collections.Generic;
using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
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
                imageResource.Name = reader.ReadPascalString();

                imageResource.DataLength = reader.ReadInt32();

                if ((imageResource.DataLength % 2) != 0)
                {
                    imageResource.DataLength++;   
                }

                imageResource.Data = reader.ReadBytes(imageResource.DataLength);

                list.Add(imageResource);
            }

            return list;
        }
    }
}