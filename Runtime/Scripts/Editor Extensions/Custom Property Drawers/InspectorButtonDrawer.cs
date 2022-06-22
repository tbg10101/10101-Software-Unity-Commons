using Software10101.EngineExtensions.Attributes;
using UnityEditor;
using UnityEngine;

namespace Software10101.EditorExtensions.Attributes {
    /// <summary>
    /// https://gist.github.com/LotteMakesStuff/dd785ff49b2a5048bb60333a6a125187#file-testbuttondrawer-cs
    /// </summary>
    [CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
    public class InspectorButtonDrawer : DecoratorDrawer {
        public override void OnGUI (Rect position) {
            // cast the attribute to make it easier to work with
            var buttonAttribute = (InspectorButtonAttribute) attribute;

            // check if the button is supposed to be enabled right now
            if (EditorApplication.isPlaying && !buttonAttribute.EnabledInPlayMode)
                GUI.enabled = false;
            if (!EditorApplication.isPlaying && !buttonAttribute.EnabledOutsidePlayMode)
                GUI.enabled = false;

            // figure out where were drawing the button
            var pos = new Rect(position.x, position.y, position.width, position.height - EditorGUIUtility.standardVerticalSpacing);
            // draw it and if its clicked...
            if (GUI.Button(pos, buttonAttribute.ButtonLabel)) {
                // tell the current game object to find and run the method we asked for!
                Selection.activeGameObject.BroadcastMessage(buttonAttribute.MethodName);
            }

            // make sure the GUI is enabled when were done!
            GUI.enabled = true;
        }

        public override float GetHeight () {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2;
        }
    }
}
