#if UNITY_EDITOR && SORTIFY
using UnityEditor;
using UnityEngine;

namespace Sortify
{
    [InitializeOnLoad]
    public static class SortifyInitializer
    {
        private static bool _labelDrawn = false;

        static SortifyInitializer()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;

            EditorApplication.hierarchyChanged += ResetLabelDrawn;
            EditorApplication.update += ResetLabelDrawn;
        }

        public static void Refresh() => EditorApplication.RepaintHierarchyWindow();
        private static void ResetLabelDrawn() => _labelDrawn = false;
        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            if (EditorApplication.isPlaying)
            {
                if (!_labelDrawn && Event.current.type == EventType.Repaint)
                {
                    float hierarchyWidth = EditorGUIUtility.currentViewWidth;
                    GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
                    {
                        normal = { textColor = Color.white },
                        fontStyle = FontStyle.Normal,
                        alignment = TextAnchor.MiddleCenter
                    };

                    Rect labelRect = new Rect(selectionRect.x, -3, hierarchyWidth, 20);
                    GUI.Label(labelRect, "Play Mode Active - Simplified View", labelStyle);
                    _labelDrawn = true;
                }
            }
            else
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                if (obj == null)
                    return;

                SortifyDrawer.Draw(instanceID, selectionRect, obj);
            }
        }
    }
}
#endif
