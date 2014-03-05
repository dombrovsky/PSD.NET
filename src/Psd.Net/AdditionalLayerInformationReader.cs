using Psd.Net.Sections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Psd.Net
{
    public class AdditionalLayerInformationReader
    {
        private static readonly string[] LongDataKeys = { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD" };

        public List<AdditionalLayerInformationBlock> Read(Stream stream, AdditionalLayerInformation additionalLayerInformation, FileVersion fileVersion)
        {
            var reader = new BigEndianBinaryReader(stream);
            stream.Position = additionalLayerInformation.Offset;
            var list = new List<AdditionalLayerInformationBlock>();

            while (stream.Position < additionalLayerInformation.Offset + additionalLayerInformation.Length)
            {
                var block = new AdditionalLayerInformationBlock();
                block.Signature = new string(reader.ReadChars(4));
                block.Key = new string(reader.ReadChars(4));
                
                if (fileVersion == FileVersion.Psb && LongDataKeys.Contains(block.Key))
                {
                    block.Length = reader.ReadInt64();
                }
                else
                {
                    block.Length = reader.ReadInt32();
                }

                if ((block.Length % 2) != 0)
                {
                    block.Length += 3; // should be 1, but 3 works there for unknown reason
                }

                block.Offset = stream.Position;
                stream.Position += block.Length;
                list.Add(block);
            }

            return list;
        }
    }
}
