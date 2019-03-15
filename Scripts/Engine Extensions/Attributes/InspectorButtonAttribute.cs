using System;
using UnityEngine;

namespace Software10101.EngineExtensions.Attributes {
	/// <summary>
	/// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187#file-testbuttonattribute-cs
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class InspectorButtonAttribute : PropertyAttribute {
		public readonly string ButtonLabel;
		public readonly string MethodName;

		public readonly bool EnabledInPlayMode;
		public readonly bool EnabledOutsidePlayMode;

		public InspectorButtonAttribute (string buttonLabel,
		                                 string methodName,
		                                 int order = 1,
		                                 bool enabledInPlayMode = true,
		                                 bool enabledOutsidePlayMode = true) {
			ButtonLabel = buttonLabel;
			MethodName = methodName;
			EnabledInPlayMode = enabledInPlayMode;
			EnabledOutsidePlayMode = enabledOutsidePlayMode;

			this.order = order; // Default the order to 1 so this can draw under header attributes
		}
	}
}
