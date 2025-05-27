using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    public bool IsAttacking { get; set; } = false;
    public Transform player; // Inspector에서 할당하거나 런타임에 찾아서 할당

    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    void Awake()
    {
        // Enemy 하위에 "Renderer" 오브젝트가 있고, 그 아래 SpriteRenderer가 있다면:
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (spriteRenderer == null) return;

        if (IsAttacking && player != null)
        {
            // 공격 중엔 항상 플레이어를 바라보게
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
        else
        {
            // 이동 방향 기반 flip
            Vector3 moveDirection = transform.position - lastPosition;
            if (moveDirection.x < -0.01f)
                spriteRenderer.flipX = false;
            else if (moveDirection.x > 0.01f)
                spriteRenderer.flipX = true;

            lastPosition = transform.position;
        }
    }
}
