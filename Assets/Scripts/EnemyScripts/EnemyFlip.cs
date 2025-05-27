using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform rendererTransform;
    private Vector3 lastPosition;

    void Awake()
    {
        // "Renderer"라는 이름의 자식 오브젝트에서 SpriteRenderer 컴포넌트 찾기
        rendererTransform = transform.Find("Renderer");
        if (rendererTransform != null)
            spriteRenderer = rendererTransform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (spriteRenderer == null)
            return;

        Vector3 moveDirection = transform.position - lastPosition;

        // 이동 방향에 따라 좌우 반전
        if (moveDirection.x < -0.01f)
            spriteRenderer.flipX = false;
        else if (moveDirection.x > 0.01f)
            spriteRenderer.flipX = true;

        lastPosition = transform.position;
    }
}
