using System;
using UnityEngine;

namespace Software10101.EngineExtensions {
    public static class TransformExtensions {
        public static void ForEachChildBottomUp(this Transform parent, Action<Transform> action) {
            foreach (Transform child in parent) {
                ForEachChildBottomUp(child, action);
            }

            action?.Invoke(parent);
        }

        public static void ForEachChildTopDown(this Transform parent, Action<Transform> action) {
            action?.Invoke(parent);

            foreach (Transform child in parent) {
                ForEachChildTopDown(child, action);
            }
        }
    }
}
