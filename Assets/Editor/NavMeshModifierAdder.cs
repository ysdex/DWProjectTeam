#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshModifierAdder
{
    [MenuItem("Tools/Add NavMeshModifier to Children with Collider2D")]
    public static void AddModifiers()
    {
        if (Selection.activeTransform == null)
        {
            Debug.LogWarning("오브젝트를 선택하세요!");
            return;
        }

        AddModifierRecursively(Selection.activeTransform);
    }

    static void AddModifierRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Collider2D>() && !child.GetComponent<NavMeshModifier>())
            {
                var modifier = Undo.AddComponent<NavMeshModifier>(child.gameObject); // Undo 지원
                modifier.overrideArea = true;
                modifier.area = 1; // Not Walkable
                Debug.Log($"Modifier 추가됨: {child.name}");
            }

            AddModifierRecursively(child);
        }
    }
}
#endif
