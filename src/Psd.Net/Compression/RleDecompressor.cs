using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Psd.Net.Sections;

namespace Psd.Net.Compression
{
    public interface IDecompressor
    {
        byte[] Decompress(Stream rawDataStream);
    }

    public sealed class RleDecompressor : IDecompressor
    {
        private readonly FileHeader _fileHeader;

        public RleDecompressor(FileHeader fileHeader)
        {
            _fileHeader = fileHeader;
        }

        public byte[] Decompress(Stream rawDataStream)
        {
            var reader = new BigEndianBinaryReader(rawDataStream);

            int totalRleLength = 0;
            for (int j = 0; j < _fileHeader.PixelHeight; j++)
            {
                totalRleLength += reader.ReadInt16();
            }

            var imageData = new byte[_fileHeader.PixelWidth * _fileHeader.PixelHeight];
            var bytesPerRow = BytesPerRow(_fileHeader.PixelWidth, _fileHeader.BitsPerChannel);
            for (int i = 0; i < _fileHeader.PixelHeight; i++)
            {
                int rowIndex = i * bytesPerRow;
                RleHelper.DecodedRow(rawDataStream, imageData, rowIndex, bytesPerRow);
            }

            return imageData;
        }

        public static int BytesPerRow(int pixelWidth, int depth)
        {
            switch (depth)
            {
                case 1:
                    return (pixelWidth + 7) / 8;
                default:
                    return pixelWidth * BytesFromBitDepth(depth);
            }
        }

        public static int BytesFromBitDepth(int depth)
        {
            switch (depth)
            {
                case 1:
                case 8:
                    return 1;
                case 16:
                    return 2;
                case 32:
                    return 4;
                default:
                    throw new ArgumentException("Invalid bit depth.");
            }
        }

        class RleHelper
        {
            ////////////////////////////////////////////////////////////////////////

            private class RlePacketStateMachine
            {
                private bool m_rlePacket = false;
                private byte lastValue;
                private int idxPacketData;
                private int packetLength;
                private int maxPacketLength = 128;
                private Stream m_stream;
                private byte[] data;

                internal void Flush()
                {
                    byte header;
                    if (m_rlePacket)
                    {
                        header = (byte)(-(packetLength - 1));
                        m_stream.WriteByte(header);
                        m_stream.WriteByte(lastValue);
                    }
                    else
                    {
                        header = (byte)(packetLength - 1);
                        m_stream.WriteByte(header);
                        m_stream.Write(data, idxPacketData, packetLength);
                    }

                    packetLength = 0;
                }

                internal void PushRow(byte[] imgData, int startIdx, int endIdx)
                {
                    data = imgData;
                    for (int i = startIdx; i < endIdx; i++)
                    {
                        byte color = imgData[i];
                        if (packetLength == 0)
                        {
                            // Starting a fresh packet.
                            m_rlePacket = false;
                            lastValue = color;
                            idxPacketData = i;
                            packetLength = 1;
                        }
                        else if (packetLength == 1)
                        {
                            // 2nd byte of this packet... decide RLE or non-RLE.
                            m_rlePacket = (color == lastValue);
                            lastValue = color;
                            packetLength = 2;
                        }
                        else if (packetLength == maxPacketLength)
                        {
                            // Packet is full. Start a new one.
                            Flush();
                            m_rlePacket = false;
                            lastValue = color;
                            idxPacketData = i;
                            packetLength = 1;
                        }
                        else if (packetLength >= 2 && m_rlePacket && color != lastValue)
                        {
                            // We were filling in an RLE packet, and we got a non-repeated color.
                            // Emit the current packet and start a new one.
                            Flush();
                            m_rlePacket = false;
                            lastValue = color;
                            idxPacketData = i;
                            packetLength = 1;
                        }
                        else if (packetLength >= 2 && m_rlePacket && color == lastValue)
                        {
                            // We are filling in an RLE packet, and we got another repeated color.
                            // Add the new color to the current packet.
                            ++packetLength;
                        }
                        else if (packetLength >= 2 && !m_rlePacket && color != lastValue)
                        {
                            // We are filling in a raw packet, and we got another random color.
                            // Add the new color to the current packet.
                            lastValue = color;
                            ++packetLength;
                        }
                        else if (packetLength >= 2 && !m_rlePacket && color == lastValue)
                        {
                            // We were filling in a raw packet, but we got a repeated color.
                            // Emit the current packet without its last color, and start a
                            // new RLE packet that starts with a length of 2.
                            --packetLength;
                            Flush();
                            m_rlePacket = true;
                            packetLength = 2;
                            lastValue = color;
                        }
                    }

                    Flush();
                }

                internal RlePacketStateMachine(Stream stream)
                {
                    m_stream = stream;
                }
            }

            ////////////////////////////////////////////////////////////////////////

            public static int EncodedRow(Stream stream, byte[] imgData, int startIdx, int columns)
            {
                long startPosition = stream.Position;

                RlePacketStateMachine machine = new RlePacketStateMachine(stream);
                machine.PushRow(imgData, startIdx, startIdx + columns);

                return (int)(stream.Position - startPosition);
            }

            ////////////////////////////////////////////////////////////////////////

            public static void DecodedRow(Stream stream, byte[] imgData, int startIdx, int columns)
            {
                int count = 0;
                while (count < columns)
                {
                    byte byteValue = (byte)stream.ReadByte();

                    int len = (int)byteValue;
                    if (len < 128)
                    {
                        len++;
                        while (len != 0 && (startIdx + count) < imgData.Length)
                        {
                            byteValue = (byte)stream.ReadByte();

                            imgData[startIdx + count] = byteValue;
                            count++;
                            len--;
                        }
                    }
                    else if (len > 128)
                    {
                        // Next -len+1 bytes in the dest are replicated from next source byte.
                        // (Interpret len as a negative 8-bit int.)
                        len ^= 0x0FF;
                        len += 2;
                        byteValue = (byte)stream.ReadByte();

                        while (len != 0 && (startIdx + count) < imgData.Length)
                        {
                            imgData[startIdx + count] = byteValue;
                            count++;
                            len--;
                        }
                    }
                    else if (128 == len)
                    {
                        // Do nothing
                    }
                }

            }
        }
    }
}
