namespace System {
	public static class Ints {
		public const int NoBitsSet = 0;
		public const int AllBitsSet = -1;

		private const byte IntBitCount = sizeof(int) * 8;

		public static int SetBit (this int i, int index) {
			TestIndex(index);
			return i | 1 << index;
		}

		public static int ResetBit (this int i, int index) {
			TestIndex(index);
			return i & ~(1 << index);
		}

		public static bool IsBitSet (this int i, int index) {
			TestIndex(index);
			return (i & (1 << index)) != 0;
		}

		public static string ToStringBinary(this int i) {
			return Convert.ToString(i, 2).PadLeft(IntBitCount, '0');
		}

		private static void TestIndex (int index) {
			if (index < 0 || index >= IntBitCount) {
				throw new ArgumentOutOfRangeException(nameof(index),
				                                      $"Index ({index}) must be greater than 0 and less than {IntBitCount}.");
			}
		}
	}
}
