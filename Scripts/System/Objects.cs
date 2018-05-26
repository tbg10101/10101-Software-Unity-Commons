namespace System {
	public static class Objects {
		public static T RequireNonNull<T> (T param) {
			if (param == null) {
				throw new ArgumentException();
			}

			return param;
		}
	}
}
