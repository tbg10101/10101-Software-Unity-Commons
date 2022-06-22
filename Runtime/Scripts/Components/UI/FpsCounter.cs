using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Software10101.Components.UI {
    [RequireComponent(typeof(Text))]
    public class FpsCounter : MonoBehaviour {
        private Text _text = null;
        private static int _frameCount = 0;

        private void Start () {
            _text = GetComponent<Text>();
            StartCoroutine(CountFps());
        }

        private void Update () {
            _frameCount++;
        }

        private IEnumerator CountFps () {
            while (this) {
                yield return new WaitForSecondsRealtime(1.0f);
                _text.text = _frameCount.ToString();
                _frameCount = 0;
            }
        }
    }
}
