using System;
using System.Collections.Concurrent;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
using System.Diagnostics;
#endif

namespace Software10101.Components {
    public class MainThreadJobExecutor : MonoBehaviour {
        private static MainThreadJobExecutor _instance;

        private static readonly ConcurrentQueue<Action> CrossSceneActionsQueue = new ConcurrentQueue<Action>();
        private readonly ConcurrentQueue<Action> _thisSceneActionsQueue = new ConcurrentQueue<Action>();

#if UNITY_EDITOR
        public long LastExecutionDurationMs;

        private static ulong _crossSceneActionsExecutedBack;
        [Header("Execution Counters")]
        [SerializeField]
        private ulong _crossSceneActionsExecuted;

        [SerializeField]
        private ulong _thisSceneActionsExecuted;

        private readonly Stopwatch _executionStopwatch = new Stopwatch();
#endif

        private void Awake() {
            _instance = this;
        }

        private void Update() {
#if UNITY_EDITOR
            _executionStopwatch.Restart();
#endif
            Action[] queueSnapshot;

            if (!_thisSceneActionsQueue.IsEmpty) {
                queueSnapshot = _thisSceneActionsQueue.ToArray();

                foreach (Action action in queueSnapshot) {
                    try {
                        action.Invoke();

#if UNITY_EDITOR
                        _thisSceneActionsExecuted++;
#endif
                    } catch (Exception e) {
                        Debug.LogException(e);
                    }
                }

                for (var i = 0; i < queueSnapshot.Length; i++) {
                    _thisSceneActionsQueue.TryDequeue(out _);
                }
            }

            if (!CrossSceneActionsQueue.IsEmpty) {
                queueSnapshot = CrossSceneActionsQueue.ToArray();

                foreach (Action action in queueSnapshot) {
                    try {
                        action.Invoke();

#if UNITY_EDITOR
                        _crossSceneActionsExecutedBack++;
#endif
                    } catch (Exception e) {
                        Debug.LogException(e);
                    }
                }

                for (var i = 0; i < queueSnapshot.Length; i++) {
                    CrossSceneActionsQueue.TryDequeue(out _);
                }
            }

#if UNITY_EDITOR
            _crossSceneActionsExecuted = _crossSceneActionsExecutedBack;

            _executionStopwatch.Stop();

            LastExecutionDurationMs = _executionStopwatch.ElapsedMilliseconds;
#endif
        }

        public static void ExecuteOnMainThreadAcrossScenes(Action action) {
            if (action == null) {
                return;
            }

            CrossSceneActionsQueue.Enqueue(action);
        }

        public static void ExecuteOnMainThread(Action action) {
            if (!_instance) {
                Debug.LogError("Cannot schedule job for the main thread. The executor singleton is not instantiated.");
                return;
            }

            if (action == null) {
                return;
            }

            _instance._thisSceneActionsQueue.Enqueue(action);
        }
    }
}
