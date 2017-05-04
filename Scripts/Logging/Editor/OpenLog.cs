using UnityEngine;
using UnityEditor;
using System;

namespace Software10101.Logging {
	public class OpenLog : MonoBehaviour {
		[MenuItem("Tools/Open Log")]
		private static void OpenLogFile () {
			System.Diagnostics.Process.Start(Log.FilePath);
		}

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_WIN
		[MenuItem("Help/Open Editor Log")]
		private static void OpenEditorLogFile () {
#if UNITY_EDITOR_OSX
			string logFile = "~/Library/Logs/Unity/Editor.log";
#elif UNITY_STANDALONE_WIN
			string logFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Unity\\Editor\\Editor.log";
#endif

			Debug.Log("Opening Unity Editor log file: " + logFile);
			System.Diagnostics.Process.Start(logFile);
		}
#endif
	}
}
