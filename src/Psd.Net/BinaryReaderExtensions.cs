using System.IO;

namespace Psd.Net
{
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads the pascal string.
        /// </summary>
        /// <param name="binaryReader">The binary reader.</param>
        /// <returns>Pascal string.</returns>
        public static string ReadPascalString(this BinaryReader binaryReader)
        {
            var stringLength = binaryReader.ReadByte();
            var resultString = new string(binaryReader.ReadChars(stringLength));

            if ((stringLength % 2) != 0)
            {
                binaryReader.ReadByte();
            }

            return resultString;
        }
    }
}
