namespace Software10101.Utils {
	public static class MathHelper {
		/// <summary>
		/// From Dot Net Pearls: https://www.dotnetperls.com/fibonacci
		/// </summary>
		public static int Fibonacci (int n) {
			int a = 0;
			int b = 1;

			// In N steps compute Fibonacci sequence iteratively.
			for (int i = 0; i < n; i++) {
				int temp = a;
				a = b;
				b = temp + b;
			}

			return a;
		}
	}
}
