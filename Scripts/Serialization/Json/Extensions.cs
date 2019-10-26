namespace Software10101.Serialization.Json {
    public static class Extensions {
        public static JsonDictionary Deserialize(this string input) {
            return GenericJson.Deserialize(input);
        }
    }
}
