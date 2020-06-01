using UnityEngine;

namespace Software10101.Logging {
    public sealed class LogCloser : MonoBehaviour {
        private void Start () {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit () {
            Log.Stop();
        }
    }
}
