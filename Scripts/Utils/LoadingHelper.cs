using System.Collections.Generic;
using UnityEngine;

namespace Software10101.Utils {
	public sealed class LoadingHelper : MonoBehaviour {
		public List<Material> MaterialInputs;
		public List<Texture> TextureInputs;
		public List<GameObject> PrefabInputs;

		public static Dictionary<string, Material> Materials = new Dictionary<string, Material>();
		public static Dictionary<string, Color> Colors = new Dictionary<string, Color>();
		public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
		public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();

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

			foreach (Texture2D t in TextureInputs) {
				Textures[t.name] = t;
			}

			foreach (GameObject go in PrefabInputs) {
				Prefabs[go.name] = go;
			}
		}
	}
}
