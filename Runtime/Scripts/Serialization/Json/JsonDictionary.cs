using System;
using System.Collections.Generic;

namespace Software10101.Serialization.Json {
    public class JsonDictionary : Dictionary<string, object> {
        public string Serialize() {
            return GenericJson.Serialize((IDictionary<string, object>)this);
        }

        public T Get<T>(string key) {
            return (T)base[key];
        }

        public bool TryGetValue<T>(string key, out T element) {
            if (base.TryGetValue(key, out object obj)) {
                try {
                    element = (T)obj;
                    return true;
                } catch (InvalidCastException) { }
            }

            element = default;
            return false;
        }

        public double? GetNumber(string key) {
            return (double?)base[key];
        }

        public string GetString(string key) {
            return (string)base[key];
        }

        public bool? GetBoolean(string key) {
            return (bool?)base[key];
        }

        public JsonList GetList(string key) {
            return (JsonList)base[key];
        }

        public JsonDictionary GetDictionary(string key) {
            return (JsonDictionary)base[key];
        }
    }
}
