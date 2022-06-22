using System.Collections.Generic;
using System.Text;

namespace System {
    public static class Strings {
        public static byte[] GetBytes(this string input, Encoding encoding) {
            return encoding.GetBytes(input);
        }

        public static string ToString(this byte[] input, Encoding encoding) {
            return encoding.GetString(input);
        }

        public static byte[] ToHexStringBytes(this byte[] input) {
            byte[] result = new byte[input.Length * 2];

            for (int i = 0; i < input.Length; i++) {
                byte b = input[i];
                string byteString = b.ToString("X2");
                result[2 * i + 0] = Convert.ToByte(byteString[0]);
                result[2 * i + 1] = Convert.ToByte(byteString[1]);
            }

            return result;
        }

        public static string ToHexString(this byte[] input, Encoding encoding) {
            return encoding.GetString(input.ToHexStringBytes());
        }

        public static byte[] FromHexStringBytes(this byte[] input) {
            byte[] result = new byte[input.Length / 2];

            char[] tmp = new char[2];

            for (int i = 0; i < result.Length; i++) {
                tmp[0] = Convert.ToChar(input[2 * i + 0]);
                tmp[1] = Convert.ToChar(input[2 * i + 1]);

                byte b = Convert.ToByte(new string(tmp), 16);

                result[i] = b;
            }

            return result;
        }

        public static string ToString(this ICollection<byte> input, Encoding encoding) {
            byte[] arrayBytes = new byte[input.Count];
            input.CopyTo(arrayBytes, 0);
            return arrayBytes.ToString(encoding);
        }

        public static Guid ToGuid(this string input) {
            return Guid.Parse(input);
        }
    }
}
