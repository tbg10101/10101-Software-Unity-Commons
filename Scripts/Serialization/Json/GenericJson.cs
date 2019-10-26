using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Software10101.Serialization.Json {
	public static class GenericJson {
		public static Dictionary<string, object> Deserialize (string input) {
			// find the first non-escaped opening brace in the string
			int startIndex;
			bool escaped = false;

			for (startIndex = 0; startIndex < input.Length; startIndex++) {
				if (escaped) {
					escaped = false;
				} else {
					char currentChar = input[startIndex];

					if (currentChar == '{') {
						break;
					}

					if (currentChar == '\\') {
						escaped = true;
					}
				}
			}

			int endIndex;

			return DoDeserializeMap(input, startIndex, out endIndex);
		}

		private static Dictionary<string, object> DoDeserializeMap (string input, int startIndex, out int endIndex) {
			Dictionary<string, object> output = new Dictionary<string, object>();

			bool escaped = false;
			bool pastKeyValueSeparator = false;
			string key = null;
			int valueStart = -1;
			object value = null;

			for (endIndex = startIndex + 1; endIndex < input.Length; endIndex++) {
				if (escaped) {
					escaped = false;
				} else {
					char currentChar = input[endIndex];

					if (currentChar == '\\') {
						escaped = true;
					} else if (!pastKeyValueSeparator) {
						if (currentChar == '"') {
							int newEndIndex;
							key = DoDeserializeString(input, endIndex, out newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == ':' && key != null) {
							pastKeyValueSeparator = true;
							valueStart = endIndex + 1;
						} else if (currentChar == '}') {
							return output;
						}
					} else {
						if (currentChar == '"') {
							int newEndIndex;
							value = DoDeserializeString(input, endIndex, out newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == '{') {
							int newEndIndex;
							value = DoDeserializeMap(input, endIndex, out newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == '[') {
							int newEndIndex;
							value = DoDeserializeList(input, endIndex, out newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == ',') {
							if (value == null) {
								value = DoDeserializeOther(input.Substring(valueStart, endIndex - valueStart));
							}

							output[key] = value;

							pastKeyValueSeparator = false;
							key = null;
							valueStart = -1;
							value = null;
						} else if (currentChar == '}') {
							if (value == null) {
								value = DoDeserializeOther(input.Substring(valueStart, endIndex - valueStart));
							}

							output[key] = value;

							return output;
						}
					}
				}
			}

			throw new Exception("Something went wrong while deserializing:\n" + input.Substring(startIndex));
		}

		private static List<object> DoDeserializeList (string input, int startIndex, out int endIndex) {
			List<object> output = new List<object>();

			bool escaped = false;
			int valueStart = startIndex + 1;
			object value = null;

			for (endIndex = startIndex + 1; endIndex < input.Length; endIndex++) {
				if (escaped) {
					escaped = false;
				} else {
					char currentChar = input[endIndex];

					if (currentChar == '\\') {
						escaped = true;
					} else if (currentChar == '"') {
						int newEndIndex;
						value = DoDeserializeString(input, endIndex, out newEndIndex);
						endIndex = newEndIndex;
					} else if (currentChar == '{') {
						int newEndIndex;
						value = DoDeserializeMap(input, endIndex, out newEndIndex);
						endIndex = newEndIndex;
					} else if (currentChar == '[') {
						int newEndIndex;
						value = DoDeserializeList(input, endIndex, out newEndIndex);
						endIndex = newEndIndex;
					} else if (currentChar == ',') {
						if (value == null) {
							value = DoDeserializeOther(input.Substring(valueStart, endIndex - valueStart));
						}

						output.Add(value);

						valueStart = endIndex + 1;
						value = null;
					} else if (currentChar == ']') {
						if (endIndex > startIndex + 1) {
							if (value == null) {
								value = DoDeserializeOther(input.Substring(valueStart, endIndex - valueStart));
							}

							output.Add(value);
						}

						return output;
					}
				}
			}

			throw new Exception("Something went wrong while deserializing:\n" + input.Substring(startIndex));
		}

		private static string DoDeserializeString (string input, int startIndex, out int endIndex) {
			bool escaped = false;

			for (endIndex = startIndex + 1; endIndex < input.Length; endIndex++) {
				if (escaped) {
					escaped = false;
				} else {
					char currentChar = input[endIndex];

					if (currentChar == '\\') {
						escaped = true;
					} else if (currentChar == '"') {
						if (endIndex == startIndex + 1) {
							return "";
						}

						StringBuilder unescapedBuilder = new StringBuilder();

						string substring = input.Substring(startIndex + 1, endIndex - startIndex - 1);

						escaped = false;
						foreach (char c in substring) {
							if (escaped) {
								escaped = false;
							} else if (c == '\\') {
								escaped = true;
								continue;
							}

							unescapedBuilder.Append(c);
						}

						return unescapedBuilder.ToString();
					}
				}
			}

			throw new Exception("Something went wrong while deserializing:\n" + input.Substring(startIndex));
		}

		private static object DoDeserializeOther (string input) {
			if (input.Contains("null")) {
				return null;
			}

			if (input.Contains("false")) {
				return false;
			}

			if (input.Contains("true")) {
				return true;
			}

			try {
				return double.Parse(input);
			} catch (FormatException) {}

			throw new Exception("Something went wrong while deserializing:\n" + input);
		}

		public static string Serialize<K, V> (IDictionary<K, V> input) {
			IDictionary<string, object> tmp = new Dictionary<string, object>();

			input.ForEach(entry => { tmp[entry.Key.ToString()] = entry.Value; });

            return Serialize((IDictionary)tmp);
		}

		public static string Serialize (IDictionary input) {
			return DoSerializeMap(input);
		}

		private static string DoSerialize (object input) {
			if (input == null) {
				return "null";
			}

			if (true.Equals(input)) {
				return "true";
			}

			if (false.Equals(input)) {
				return "false";
			}

			string s = input as string;
			if (s != null) {
				return DoSerializeString(s);
			}

			Object l = input as IList;
			if (l != null) {
				return DoSerializeList(input as IList);
			}

            Object m2 = input as IDictionary;
            if (m2 != null)
            {
                return DoSerializeMap(m2 as IDictionary);
            }

            try {
				double d = double.Parse(input.ToString());
				return d.ToString(CultureInfo.InvariantCulture);
			} catch (InvalidCastException) {
			} catch(FormatException) {}

			throw new Exception("Could not serialize: " + input);
		}

        private static string DoSerializeMap(IDictionary input) {
            StringJoiner output = new StringJoiner(",", "{", "}");

            ICollection keys = input.Keys;

            foreach (Object key in keys) {
                output.Add(DoSerializeString(key.ToString()) + ":" + DoSerialize(input[key]));
            }

            return output.ToString();
        }

        private static string DoSerializeList (IList input) {
			StringJoiner output = new StringJoiner(",", "[", "]");

			for (int i = 0; i < input.Count; i++) {
				output.Add(DoSerialize(input[i]));
			}

			return output.ToString();
		}

		private static string DoSerializeString (string input) {
			StringBuilder escapedStringBuilder = new StringBuilder();

			foreach (char c in input) {
				if (c == '"' || c == '\\') {
					escapedStringBuilder.Append("\\");
				}

				escapedStringBuilder.Append(c);
			}

			return "\"" + escapedStringBuilder + "\"";
		}
	}
}
