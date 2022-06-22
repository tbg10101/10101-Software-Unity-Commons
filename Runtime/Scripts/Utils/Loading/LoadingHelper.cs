using System.Collections.Generic;
using Software10101.Logging;
using UnityEngine;

namespace Software10101.Utils.Loading {
    [ExecuteInEditMode]
    public sealed class LoadingHelper : MonoBehaviour {
        public List<Material> MaterialInputs;
        public List<ColorObject> ColorsInputs;
        public List<Texture2D> TextureInputs;
        public List<Sprite> SpriteInputs;
        public List<GameObject> PrefabInputs;

        public static readonly Dictionary<string, Material> Materials = new Dictionary<string, Material>();
        public static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>();
        public static readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public static readonly Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

#if UNITY_EDITOR
        private void Update () {
            if (!Application.isPlaying) {
                LinkedHashSet<string> materialNameSet = new LinkedHashSet<string>();

                MaterialInputs.ForEach(element => materialNameSet.Add(element.name));

                if (materialNameSet.Count != MaterialInputs.Count) {
                    Debug.LogError("An item with the same name already exists in this list.");
                    UnityEditor.Undo.PerformUndo();
                }
            }
        }
#endif

        private void Awake () {
            Materials.Clear();
            Colors.Clear();
            Textures.Clear();
            Prefabs.Clear();

            foreach (Material m in MaterialInputs) {
                Log.Trace("Loading Material '" + m.name + "'...");

                Materials[m.name] = m;

                if (m.HasProperty("_Color")) {
                    Colors[m.name] = m.color;

                    Texture2D t = new Texture2D(1, 1);
                    t.SetPixel(0, 0, m.color);
                    t.Apply();
                    Textures[m.name] = t;
                }
            }

            foreach (ColorObject c in ColorsInputs) {
                Log.Trace("Loading Color '" + c.name + "'...");

                Colors[c.name] = c.Color;
            }

            foreach (Texture2D t in TextureInputs) {
                Log.Trace("Loading Texture '" + t.name + "'...");

                Textures[t.name] = t;
            }

            foreach (Sprite s in SpriteInputs) {
                Log.Trace("Loading Sprite '" + s.name + "'...");

                Sprites[s.name] = s;
            }

            foreach (GameObject go in PrefabInputs) {
                Log.Trace("Loading Prefab '" + go.name + "'...");

                Prefabs[go.name] = go;
            }
        }
    }
}
