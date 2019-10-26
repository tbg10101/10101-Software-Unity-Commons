using UnityEngine;
using UnityEngine.UI;

namespace Software10101.Components.UI {
    [ExecuteInEditMode]
    [RequireComponent(typeof(Text))]
    public sealed class VersionDisplay : MonoBehaviour {
        private void Awake () {
            GetComponent<Text>().text = Application.version;
        }

#if UNITY_EDITOR
        private void Update () {
            Awake();
        }
#endif
    }
}
