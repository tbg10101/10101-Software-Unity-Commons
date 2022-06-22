using UnityEngine;

namespace Software10101.EngineExtensions.Attributes {
    /// <summary>
    /// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187#file-progressbarattribute-cs
    /// </summary>
    public class ProgressBarAttribute : PropertyAttribute {
        public readonly float Minimum;
        public readonly float Maximum;
        public readonly string LabelField;

        public ProgressBarAttribute(float minimum = 0, float maximum = 1, string labelField = null) {
            Minimum = minimum;
            Maximum = maximum;
            LabelField = labelField;
        }
    }
}
