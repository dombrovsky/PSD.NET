using System.Collections.Generic;
using System.IO;
using Psd.Net.Sections;

namespace Psd.Net
{
    public class ImageResourcesReader
    {
        public ImageResourcesSection Read(Stream stream)
        {
            var imageResourcesSection = new ImageResourcesSection();
            var reader = new BigEndianBinaryReader(stream);
            imageResourcesSection.Length = reader.ReadInt32();
            imageResourcesSection.Offset = reader.BaseStream.Position;

            stream.Position += imageResourcesSection.Length;

            return imageResourcesSection;
        }
    }

    public class ImageResourceBlocksReader
    {
        public List<ImageResourceBlock> Read(Stream stream, ImageResourcesSection imageResourcesSection)
        {
            var list = new List<ImageResourceBlock>();

            var reader = new BigEndianBinaryReader(stream);
            stream.Position = imageResourcesSection.Offset;

            while (reader.BaseStream.Position < imageResourcesSection.Offset + imageResourcesSection.Length)
            {
                var imageResource = new ImageResourceBlock();

                imageResource.Signature = new string(reader.ReadChars(4));
                imageResource.Id = (ImageResourceId)reader.ReadInt16();
                imageResource.Name = reader.ReadPascalString();

                imageResource.Length = reader.ReadInt32();

                if ((imageResource.Length % 2) != 0)
                {
                    imageResource.Length++;
                }

                imageResource.Offset = stream.Position;
                stream.Position += imageResource.Length;

                list.Add(imageResource);
            }

            return list;
        }
    }
}