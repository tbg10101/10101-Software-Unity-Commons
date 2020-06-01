using System.Collections.Generic;

namespace Software10101.Serialization.Json {
    public class JsonList : List<object> {
        public T Get<T>(int index) {
            return (T)base[index];
        }

        public double? GetNumber(int index) {
            return (double?)base[index];
        }

        public string GetString(int index) {
            return (string)base[index];
        }

        public bool? GetBoolean(int index) {
            return (bool?)base[index];
        }

        public JsonList GetList(int index) {
            return (JsonList)base[index];
        }

        public JsonDictionary GetDictionary(int index) {
            return (JsonDictionary)base[index];
        }
    }
}
