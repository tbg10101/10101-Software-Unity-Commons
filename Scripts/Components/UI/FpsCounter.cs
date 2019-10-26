using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Software10101.Components.UI {
	[RequireComponent(typeof(Text))]
	public class FpsCounter : MonoBehaviour {
		private Text _text = null;
		private static int FrameCount = 0;

		private void Start () {
			_text = GetComponent<Text>();
			StartCoroutine(CountFps());
		}

		private void Update () {
			FrameCount++;
		}

		private IEnumerator CountFps () {
			while (true) {
				yield return new WaitForSecondsRealtime(1.0f);
				_text.text = FrameCount.ToString();
				FrameCount = 0;
			}
		}
	}
}
