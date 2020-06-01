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
            if (!input.TryGetValue("x", out double x) || !input.TryGetValue("y", out double y)) {
                v = default;
                return false;
            }

            v = new Vector2((float)x, (float)y);
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
            if (!input.TryGetValue("x", out double x) || !input.TryGetValue("y", out double y) || !input.TryGetValue("z", out double z)) {
                v = default;
                return false;
            }

            v = new Vector3((float)x, (float)y, (float)z);
            return true;
        }
    }
}
