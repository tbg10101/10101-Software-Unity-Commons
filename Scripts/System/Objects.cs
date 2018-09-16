namespace System {
	public static class Objects {
        static bool IsNullable(Type type) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static T RequireNonNull<T> (T param) {
            if (IsNullable(typeof(T)) && param == null) {
				throw new ArgumentException();
			}

			return param;
		}

        public static int RoundUp (this double me) {
            return (int)Math.Ceiling(me);
        }

        public static int RoundDown (this double me) {
            return (int)Math.Floor(me);
        }

        public static int Round (this double me) {
            return (int)Math.Round(me);
        }
    }
}
