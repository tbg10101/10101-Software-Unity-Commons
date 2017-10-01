using System;
using System.IO;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace Software10101.Logging {
	/// <summary>
	/// Small logging utility for Unity.
	///
	/// Log lines may contain string formatting parameters:
	///		Log.info("message with param: {0}", "parameter value");
	///
	/// The parameters can be any object.
	///
	/// Additionally, if the last parameter is an Exception the message and stack trace will be printed after the log line.
	///
	/// Configure logging level and file by setting the fields at the top of the class.
	///
	/// Messages may be logged at the following levels:
	/// 	- TRACE
	/// 	- DEBUG
	/// 	- INFO
	/// 	- WARN
	/// 	- ERROR
	/// 	- FATAL
	/// So, for example, setting the log level to INFO will hide all TRACE and DEBUG messages.
	///
	/// How to start the logging system: Log.Start();
	/// How to stop the logging system: Log.Stop();
	/// </summary>
	public static class Log {
		// configuration
		/// <summary>
		/// Messages logged at or above this level will be saved to disk.
		/// </summary>
		public static Level LogLevel = Level.INFO;

		/// <summary>
		/// The file to which messages will be written.
		/// </summary>
		public static string FilePath = UnityEngine.Application.persistentDataPath + "/log.txt";

		/// <summary>
		/// Whether or not the method name and line number is logged with messages.
		/// </summary>
		public static bool ShowMethodDetail = true;

		/// <summary>
		/// Whether or not the log line is written to the Unity log.
		/// </summary>
		public static bool WriteToUnityLog = true;

		/// <summary>
		/// The various levels at which messages may be logged.
		/// </summary>
		public enum Level { ALL, TRACE, DEBUG, INFO, WARN, ERROR, FATAL, OFF }

		// used to write the log lines to disk
		private static StreamWriter _writer;

		// used in place of a null array of parameters
		private static readonly object[] NullArrayReplacement = { "null" };

		/// <summary>
		/// Opens a stream writer to the configured log file.
		/// </summary>
		public static void Start () {
			if (_writer != null) {
				Stop();
			}

			_writer = new StreamWriter(FilePath, true, Encoding.Unicode);

			UnityEngine.Debug.Log("Logging started: " + FilePath);
		}

		/// <summary>
		/// Closes the stream writer to the configured log file.
		/// </summary>
		public static void Stop () {
			_writer.Flush();
			_writer.Close();

			_writer = null;
		}

		// internal method for processing messages and parameters, then writing the result to disk
		[StringFormatMethod("message")]
		private static string LogInternal (Level level, string message, params object[] parameters) {
			string processedMessage = string.Format(message, parameters ?? NullArrayReplacement);

			if (_writer == null) {
				return processedMessage;
			}

			if (parameters != null && parameters.Length >= 1) {
				Exception e = parameters[parameters.Length - 1] as Exception;

				if (e != null) {
					StringJoiner joiner = new StringJoiner(Environment.NewLine);

					joiner.Add(processedMessage);
					joiner.Add(e.Message);
					joiner.Add(e.StackTrace);

					processedMessage = joiner.ToString();
				}
			}

			DateTime dateTime = DateTime.Now;

			if (ShowMethodDetail) {
				System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame(2, true);
				MethodBase method = stackFrame.GetMethod();

				_writer.WriteLine("{0:yyyy-dd-MM HH:mm:ss.fff} {1}.{2}():{3} {4} {5}",
					dateTime,
					method.DeclaringType,
					method.Name,
					stackFrame.GetFileLineNumber(),
					level,
					processedMessage);
			} else {
				_writer.WriteLine("{0:yyyy-dd-MM HH:mm:ss.fff} {1} {2}",
					dateTime,
					level,
					processedMessage);
			}

#if UNITY_EDITOR
			_writer.Flush();
#endif

			return processedMessage;
		}

		/// <summary>
		/// Log at the TRACE level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="parameters">Parameters to use when formatting the message.</param>
		[StringFormatMethod("message")]
		public static void Trace (string message, params object[] parameters) {
			const Level thisLevel = Level.TRACE;

			if (LogLevel <= thisLevel) {
				string s = LogInternal(thisLevel, message, parameters);

				if (WriteToUnityLog) {
					UnityEngine.Debug.Log(s);
				}
			}
		}

		/// <summary>
		/// Log at the DEBUG level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="parameters">Parameters to use when formatting the message.</param>
		[StringFormatMethod("message")]
		public static void Debug (string message, params object[] parameters) {
			const Level thisLevel = Level.DEBUG;

			if (LogLevel <= thisLevel) {
				string s = LogInternal(thisLevel, message, parameters);

				if (WriteToUnityLog) {
					UnityEngine.Debug.Log(s);
				}
			}
		}

		/// <summary>
		/// Log at the INFO level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="parameters">Parameters to use when formatting the message.</param>
		[StringFormatMethod("message")]
		public static void Info (string message, params object[] parameters) {
			const Level thisLevel = Level.INFO;

			if (LogLevel <= thisLevel) {
				string s = LogInternal(thisLevel, message, parameters);

				if (WriteToUnityLog) {
					UnityEngine.Debug.Log(s);
				}
			}
		}

		/// <summary>
		/// Log at the WARN level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="parameters">Parameters to use when formatting the message.</param>
		[StringFormatMethod("message")]
		public static void Warn (string message, params object[] parameters) {
			const Level thisLevel = Level.WARN;

			if (LogLevel <= thisLevel) {
				string s = LogInternal(thisLevel, message, parameters);

				if (WriteToUnityLog) {
					UnityEngine.Debug.LogWarning(s);
				}
			}
		}

		/// <summary>
		/// Log at the ERROR level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="parameters">Parameters to use when formatting the message.</param>
		[StringFormatMethod("message")]
		public static void Error (string message, params object[] parameters) {
			const Level thisLevel = Level.ERROR;

			if (LogLevel <= thisLevel) {
				string s = LogInternal(thisLevel, message, parameters);

				if (WriteToUnityLog) {
					UnityEngine.Debug.LogError(s);
				}
			}
		}

		/// <summary>
		/// Log at the FATAL level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="parameters">Parameters to use when formatting the message.</param>
		[StringFormatMethod("message")]
		public static void Fatal (string message, params object[] parameters) {
			const Level thisLevel = Level.FATAL;

			if (LogLevel <= thisLevel) {
				string s = LogInternal(thisLevel, message, parameters);

				if (WriteToUnityLog) {
					UnityEngine.Debug.LogError(s);
				}
			}
		}
	}
}
