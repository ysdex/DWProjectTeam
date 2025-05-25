using UnityEngine;
using NavMeshPlus.Components;  // 필수 네임스페이스

public class AutoAddNavMeshModifier : MonoBehaviour
{
    public bool overrideArea = true;
    public int areaType = 1; // 1 = Not Walkable

    void Start()
    {
        AddModifierRecursively(transform);
    }

    void AddModifierRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Collider2D>() && !child.GetComponent<NavMeshModifier>())
            {
                var modifier = child.gameObject.AddComponent<NavMeshModifier>();
                modifier.overrideArea = overrideArea;
                modifier.area = areaType;
                Debug.Log($"Modifier 추가됨: {child.name}");
            }

            AddModifierRecursively(child);
        }
    }
}
