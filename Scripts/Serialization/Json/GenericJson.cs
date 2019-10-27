using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Software10101.Serialization.Json {
	public static class GenericJson {
		// ReSharper disable once UnusedMember.Global
		// ReSharper disable once MemberCanBePrivate.Global
		public static JsonDictionary Deserialize (string input) {
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

			return DoDeserializeMap(input, startIndex, out _);
		}

		private static JsonDictionary DoDeserializeMap (string input, int startIndex, out int endIndex) {
			JsonDictionary output = new JsonDictionary();

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
							key = DoDeserializeString(input, endIndex, out var newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == ':' && key != null) {
							pastKeyValueSeparator = true;
							valueStart = endIndex + 1;
						} else if (currentChar == '}') {
							return output;
						}
					} else {
						if (currentChar == '"') {
							value = DoDeserializeString(input, endIndex, out var newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == '{') {
							value = DoDeserializeMap(input, endIndex, out var newEndIndex);
							endIndex = newEndIndex;
						} else if (currentChar == '[') {
							value = DoDeserializeList(input, endIndex, out var newEndIndex);
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

		private static JsonList DoDeserializeList (string input, int startIndex, out int endIndex) {
			JsonList output = new JsonList();

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
						value = DoDeserializeString(input, endIndex, out var newEndIndex);
						endIndex = newEndIndex;
					} else if (currentChar == '{') {
						value = DoDeserializeMap(input, endIndex, out var newEndIndex);
						endIndex = newEndIndex;
					} else if (currentChar == '[') {
						value = DoDeserializeList(input, endIndex, out var newEndIndex);
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

		// ReSharper disable once UnusedMember.Global
		// ReSharper disable once MemberCanBePrivate.Global
		public static string Serialize<TK, TV> (IDictionary<TK, TV> input) {
			IDictionary tmp = new Dictionary<string, object>();

			input.ForEach(entry => {
				var (key, value) = entry;
				tmp[key.ToString()] = value;
			});

            return Serialize(tmp);
		}

		// ReSharper disable once UnusedMember.Global
		// ReSharper disable once MemberCanBePrivate.Global
		public static string Serialize(JsonDictionary input) {
			IDictionary tmp = new Dictionary<string, object>();

			input.ForEach(entry => {
				var (key, value) = entry;
				tmp[key] = value;
			});

            return Serialize(tmp);
		}

		// ReSharper disable once UnusedMember.Global
		// ReSharper disable once MemberCanBePrivate.Global
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

			if (input is string s) {
				return DoSerializeString(s);
			}

			object l = input as IList;
			if (l != null) {
				return DoSerializeList((IList)input);
			}

            object m2 = input as IDictionary;
            if (m2 != null)
            {
                return DoSerializeMap((IDictionary)m2);
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

            foreach (object key in keys) {
                output.Add(DoSerializeString(key.ToString()) + ":" + DoSerialize(input[key]));
            }

            return output.ToString();
        }

        private static string DoSerializeList (IList input) {
			StringJoiner output = new StringJoiner(",", "[", "]");

			foreach (object element in input) {
				output.Add(DoSerialize(element));
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
