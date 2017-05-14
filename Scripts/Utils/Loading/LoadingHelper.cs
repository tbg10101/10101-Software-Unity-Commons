using System.Collections.Generic;
using UnityEngine;

namespace Software10101.Utils.Loading {
	[ExecuteInEditMode]
	public sealed class LoadingHelper : MonoBehaviour {
		public List<Material> MaterialInputs;
		public List<ColorObject> ColorsInputs;
		public List<Texture2D> TextureInputs;
		public List<Sprite> SpriteInputs;
		public List<GameObject> PrefabInputs;

		public static Dictionary<string, Material> Materials = new Dictionary<string, Material>();
		public static Dictionary<string, Color> Colors = new Dictionary<string, Color>();
		public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
		public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
		public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

#if UNITY_EDITOR
		private void Update () {
			LinkedHashSet<string> materialNameSet = new LinkedHashSet<string>();

			MaterialInputs.ForEach(element => materialNameSet.Add(element.name));

			if (materialNameSet.Count != MaterialInputs.Count) {
				Debug.LogError("An item with the same name already exists in this list.");
				UnityEditor.Undo.PerformUndo();
			}
		}
//#else
		private void Awake () {
			Materials.Clear();
			Colors.Clear();
			Textures.Clear();
			Prefabs.Clear();

			foreach (Material m in MaterialInputs) {
				Materials[m.name] = m;

				Colors[m.name] = m.color;

				Texture2D t = new Texture2D(1, 1);
				t.SetPixel(0, 0, m.color);
				t.Apply();
				Textures[m.name] = t;
			}

			foreach (ColorObject c in ColorsInputs) {
				Colors[c.name] = c.Color;
			}

			foreach (Texture2D t in TextureInputs) {
				Textures[t.name] = t;
			}

			foreach (Sprite s in SpriteInputs) {
				Sprites[s.name] = s;
			}

			foreach (GameObject go in PrefabInputs) {
				Prefabs[go.name] = go;
			}
		}
#endif
	}
}
