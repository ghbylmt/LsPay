using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LsPay.Client.Util
{
    public static class Utilities
    {
        public static byte[] SubArray(this byte[] source, int startIndex, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = source[startIndex + i];
            }
            return result;
        }

        public static byte[] PinBlock(string pin, string pan)
        {
            RightBcdFormatter formatter = new RightBcdFormatter();

            byte[] first = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            first[0] = 0x06;
            formatter.GetBytes(pin).CopyTo(first, 1);
            byte[] second = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            formatter.GetBytes(pan).CopyTo(second, 2);
            for (int i = 0; i < 8; i++)
            {
                first[i] ^= second[i];
            }
            return first;
        }


        public static byte[] TrackBlock(string track)
        {
            LeftBcdFormatter formatter = new LeftBcdFormatter();

            byte[] panbytes = formatter.GetBytes(track);
            byte[] panLength =formatter.GetBytes(track.Length.ToString());
            int remainLength = 8 - (panbytes.Length+1) % 8;
            List<byte> listBytes = new List<byte>();
            listBytes.AddRange(panLength);
            listBytes.AddRange(panbytes);

            for (int i = 0; i < remainLength; i++)
            {
                listBytes.Add(0x00);
            }
            return listBytes.ToArray();
        }
    }

    public class RightBcdFormatter
    {
        #region IFormatter Members
        public byte[] GetBytes(string value)
        {
            if (value.Length % 2 == 1)
            {
                value = value.PadLeft(value.Length + 1, '0');
            }
            byte[] bs = Encoding.ASCII.GetBytes(value);
            int len = bs.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                byte high = (byte)(bs[i * 2] % 16);
                byte low = (byte)(bs[i * 2 + 1] % 16);
                bytes[i] = (byte)((byte)(high << 4) | low);
            }

            return bytes;
        }

        public string GetString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public int GetPackedLength(int unpackedLength)
        {
            double len = unpackedLength;
            return (int)Math.Ceiling(len / 2);
        }

        #endregion
    }


    public class LeftBcdFormatter
    {
        #region IFormatter Members

        public byte[] GetBytes(string value)
        {
            if (value.Length % 2 == 1)
            {
                value = value.PadRight(value.Length + 1, '0');
            }
            byte[] bs = Encoding.ASCII.GetBytes(value);
            int len = bs.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                byte high = (byte)(bs[i * 2] % 16);
                byte low = (byte)(bs[i * 2 + 1] % 16);
                bytes[i] = (byte)((byte)(high << 4) | low);
            }
            return bytes;
        }

        public static byte[] BCDToBytes(string value, bool isRight = true)
        {
            if (value.Length % 2 == 1)
            {
                value = isRight ? value.PadLeft(value.Length + 1, '0') : value.PadRight(value.Length + 1, '0');
            }
            byte[] bs = Encoding.ASCII.GetBytes(value);
            int len = bs.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                byte high = (byte)(bs[i * 2] % 16);
                byte low = (byte)(bs[i * 2 + 1] % 16);
                bytes[i] = (byte)((byte)(high << 4) | low);
            }
            return bytes;
        }

        public string GetString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public int GetPackedLength(int unpackedLength)
        {
            double len = unpackedLength;
            return (int)Math.Ceiling(len / 2);
        }

        #endregion
    }
}
