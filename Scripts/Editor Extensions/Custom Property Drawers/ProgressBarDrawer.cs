using Software10101.EngineExtensions.Attributes;
using UnityEditor;
using UnityEngine;

namespace Software10101.EditorExtensions.Attributes {
    /// <summary>
    /// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187#file-progressbardrawer-cs
    /// </summary>
    [CustomPropertyDrawer(typeof(ProgressBarAttribute))]
    public class ProgressBarDrawer : PropertyDrawer {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            ProgressBarAttribute progressBarAttribute = (ProgressBarAttribute) attribute;

            if (property.floatValue <= progressBarAttribute.Minimum)
                return;

            var dynamicLabel = property.serializedObject.FindProperty(((ProgressBarAttribute)attribute).LabelField);

            float fraction = (property.floatValue - progressBarAttribute.Minimum) / (progressBarAttribute.Maximum - progressBarAttribute.Minimum);

            EditorGUI.ProgressBar(position, fraction, dynamicLabel == null ? property.name : dynamicLabel.stringValue);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            if (property.floatValue <= ((ProgressBarAttribute)attribute).Minimum)
                return 0;

            return base.GetPropertyHeight(property, label);
        }
    }
}
