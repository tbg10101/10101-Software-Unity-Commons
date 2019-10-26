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
