using System;
using UnityEngine;

public static class LayerMaskExtensions {
	private const int BitsPerInt = sizeof(int) * 8;
	private static readonly int[] LayerBuffer = new int[BitsPerInt];
	public static int[] GetLayers (this LayerMask layerMask) {
		int length = 0;

		int currentMask = layerMask;

		for (int i = 0; i < BitsPerInt; i++) {
			if (currentMask % 2 == 1) {
				LayerBuffer[length] = i;
				length++;
				currentMask--;
			}

			currentMask /= 2;
		}

		int[] result = new int[length];

		Array.Copy(LayerBuffer, 0, result, 0, length);

		return result;
	}
}
