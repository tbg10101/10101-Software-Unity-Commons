using System.Collections.Generic;
using UnityEditor;

namespace Software10101.EditorExtensions.Attributes {
    public static class EditorExtensions {
        /// <summary>
        /// Gets the direct children of a SerializedProperty. (it does not recurse into children)
        ///
        /// Source: https://forum.unity.com/threads/loop-through-serializedproperty-children.435119/#post-2814895
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IEnumerable<SerializedProperty> GetChildren (this SerializedProperty property) {
            property = property.Copy();
            SerializedProperty nextElement = property.Copy();

            bool hasNextElement = nextElement.NextVisible(false);
            if (!hasNextElement) {
                nextElement = null;
            }

            property.NextVisible(true);
            while (true) {
                if ((SerializedProperty.EqualContents(property, nextElement))) {
                    yield break;
                }

                yield return property;

                bool hasNext = property.NextVisible(false);
                if (!hasNext) {
                    break;
                }
            }
        }
    }
}
