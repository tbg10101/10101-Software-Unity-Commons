using System;
using UnityEngine;

namespace Software10101.EngineExtensions {
	public static class LayerMaskExtensions {
		private const int Size = sizeof(int) * 8;
		private static readonly int[] LayerBuffer = new int[Size];
		public static int[] GetLayers (this LayerMask layerMask) {
			int length = 0;

			for (int i = 0; i < Size; i++) {
				if (layerMask.IsEnabled(i)) {
					LayerBuffer[length] = i;
					length++;
				}
			}

			int[] result = new int[length];

			Array.Copy(LayerBuffer, 0, result, 0, length);

			return result;
		}

		public static bool IsEnabled (this LayerMask layerMask, int layer) {
			return layer < Size && (layerMask.value & (1 << layer)) != 0;
		}
	}
}
