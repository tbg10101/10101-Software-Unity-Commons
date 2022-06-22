using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Software10101.EngineExtensions {
    /// <summary>
    /// Adds a checkbox to ScriptableObjects which, when ticker, will add the ScriptableObject to the list of pre-loaded assets.
    /// </summary>
    public abstract class PreLoadableScriptableObject : ScriptableObject {
#if UNITY_EDITOR
        [SerializeField]
        private bool _preLoad = false;
#endif

        protected void OnEnable() {
#if UNITY_EDITOR
            _preLoad = IsInPreloadedAssets(this);
#endif
        }

        protected void OnValidate() {
#if UNITY_EDITOR
            SetPreload(_preLoad, this);
            _preLoad = IsInPreloadedAssets(this);
#endif
        }

#if UNITY_EDITOR
        public static bool IsInPreloadedAssets(ScriptableObject obj) {
            return PlayerSettings.GetPreloadedAssets().Contains(obj);
        }

        public static void SetPreload(bool shouldPreLoad, ScriptableObject obj) {
            // this is intentionally a list to preserve order
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();

            if (shouldPreLoad) {
                if (!preloadedAssets.Contains(obj)) {
                    preloadedAssets.Add(obj);
                }
            } else {
                while (preloadedAssets.Contains(obj)) {
                    preloadedAssets.Remove(obj);
                }
            }

            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
        }

        [InitializeOnLoadMethod]
        private static void LoadInEditor() {
            var preloadedAssets = PlayerSettings.GetPreloadedAssets();
            foreach (Object preloadedAsset in preloadedAssets) {
                if (preloadedAsset) {
                    string _ = preloadedAsset.name;
                }
            }
        }
#endif
    }
}
