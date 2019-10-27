using UnityEngine;

namespace Software10101.Serialization.Json {
    public static class Extensions {
        public static JsonDictionary Deserialize(this string input) {
            return GenericJson.Deserialize(input);
        }

        public static JsonDictionary PackageForJson(this Vector2 v) {
            return new JsonDictionary {
                ["x"] = v.x,
                ["y"] = v.y
            };
        }

        public static bool TryParse(this JsonDictionary input, out Vector2 v) {
            if (!input.TryGetValue("x", out float x) || !input.TryGetValue("y", out float y)) {
                v = default;
                return false;
            }

            v = new Vector2(x, y);
            return true;
        }

        public static JsonDictionary PackageForJson(this Vector3 v) {
            return new JsonDictionary {
                ["x"] = v.x,
                ["y"] = v.y,
                ["z"] = v.z
            };
        }

        public static bool TryParse(this JsonDictionary input, out Vector3 v) {
            if (!input.TryGetValue("x", out float x) || !input.TryGetValue("y", out float y) || !input.TryGetValue("z", out float z)) {
                v = default;
                return false;
            }

            v = new Vector3(x, y, z);
            return true;
        }
    }
}
