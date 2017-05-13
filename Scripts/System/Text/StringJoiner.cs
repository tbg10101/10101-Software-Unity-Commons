namespace System.Text {
	/// <summary>
	/// <see cref="StringJoiner"/> is used to construct a string separated by a delimiter and optionally starting with a supplied prefix and ending with a supplied suffix.
	/// 
	/// Prior to adding something to the <see cref="StringJoiner"/>, its sj.ToString () method will, by default, return prefix + suffix.However, if the SetEmptyValue() method is called, the emptyValue supplied will be returned instead. This can be used, for example, when creating a string using set notation to indicate an empty set, i.e. "{}", where the prefix is "{", the suffix is "}" and nothing has been added to the <see cref="StringJoiner"/>.
	/// </summary>
	public sealed class StringJoiner {
		private readonly string _delimiter;

		private readonly string _prefix;
		private readonly string _suffix;

		private readonly StringBuilder _value = new StringBuilder();

		private string _emptyValue = "";

		/// <summary>
		/// Constructs a <see cref="StringJoiner"/> with no characters in it, with no prefix or suffix, and a copy of the supplied delimiter. If no characters are added to the <see cref="StringJoiner"/> and methods accessing the value of it are invoked, it will not return a prefix or suffix (or properties thereof) in the result, unless SetEmptyValue() has first been called.
		/// </summary>
		/// <param name="delimiter">The <see cref="string"/> to be used between each element added to the <see cref="StringJoiner"/> value.</param>
		public StringJoiner (string delimiter) {
			if (delimiter == null) {
				throw new ArgumentNullException("delimiter", "Delimiter must not be null.");
			}

			_delimiter = delimiter;
			_prefix = _suffix = "";
		}

		/// <summary>
		/// Constructs a <see cref="StringJoiner"/> with no characters in it using copies of the supplied prefix, delimiter and suffix. If no characters are added to the <see cref="StringJoiner"/> and methods accessing the string value of it are invoked, it will return the prefix + suffix (or properties thereof) in the result, unless SetEmptyValue() has first been called.
		/// </summary>
		/// <param name="delimiter">The <see cref="string"/> to be used between each element added to the <see cref="StringJoiner"/>.</param>
		/// <param name="prefix">The <see cref="string"/> to be used at the beginning.</param>
		/// <param name="suffix">The <see cref="string"/> to be used at the end.</param>
		public StringJoiner (string delimiter, string prefix, string suffix) {
			if (delimiter == null) {
				throw new ArgumentNullException("delimiter", "Delimiter must not be null.");
			}

			if (prefix == null) {
				throw new ArgumentNullException("prefix", "Prefix must not be null.");
			}

			if (suffix == null) {
				throw new ArgumentNullException("suffix", "Suffix must not be null.");
			}

			_delimiter = delimiter;
			_prefix = prefix;
			_suffix = suffix;

			_emptyValue = prefix + suffix;
		}

		/// <summary>
		/// Sets <see cref="string"/> to be used when determining the <see cref="string"/> representation of this <see cref="StringJoiner"/> and no elements have been added yet, that is, when it is empty. A copy of the emptyValue parameter is made for this purpose. Note that once an add method has been called, the <see cref="StringJoiner"/> is no longer considered empty, even if the element(s) added correspond to the empty <see cref="string"/>.
		/// </summary>
		/// <param name="emptyValue"></param>
		/// <returns>This <see cref="StringJoiner"/> itself so the calls may be chained.</returns>
		public StringJoiner SetEmptyValue (string emptyValue) {
			_emptyValue = emptyValue ?? "null";

			return this;
		}

		/// <summary>
		/// Returns the current value, consisting of the prefix, the values added so far separated by the delimiter, and the suffix, unless no elements have been added in which case, the prefix + suffix or the emptyValue characters are returned.
		/// </summary>
		/// <returns>The string representation of this <see cref="StringJoiner"/>.</returns>
		override
		public string ToString () {
			if (_value.Length == 0) {
				return _emptyValue;
			}

			string ret = _value.ToString();

			if (_suffix.Length == 0) {
				return ret;
			}

			return ret + _suffix;
		}

		/// <summary>
		/// Adds a copy of the given <see cref="string"/> value as the next element of the <see cref="StringJoiner"/> value. If newElement is null, then "null" is added.
		/// </summary>
		/// <param name="newElement">The element to add.</param>
		/// <returns>A reference to this StringJoiner.</returns>
		public StringJoiner Add (string newElement) {
			if (_value.Length == 0) {
				if (_prefix.Length == 0) {
					_value.Append(newElement);
				} else {
					_value.Append(_prefix);
					_value.Append(newElement);
				}

				return this;
			}

			_value.Append(_delimiter);
			_value.Append(newElement);

			return this;
		}

		/// <summary>
		/// Returns the length of the <see cref="string"/> representation of this <see cref="StringJoiner"/>. Note that if no add methods have been called, then the length of the <see cref="string"/> representation (either prefix + suffix or emptyValue) will be returned. The value should be equivalent to ToString().Length.
		/// </summary>
		/// <returns>The length of the current value of <see cref="StringJoiner"/>.</returns>
		public int Length () {
			return _value.Length != 0 ? _value.Length + _suffix.Length : _emptyValue.Length;
		}
	}
}
