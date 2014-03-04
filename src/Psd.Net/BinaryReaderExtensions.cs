using System.IO;

namespace Psd.Net
{
    /// <summary>
    /// <see cref="BinaryReader"/> extensions.
    /// </summary>
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

            if ((stringLength % 2) != 0 || stringLength == 0)
            {
                binaryReader.ReadByte();
            }

            return resultString;
        }
    }
}
