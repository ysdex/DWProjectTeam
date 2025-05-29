using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Move : MonoBehaviour
{
    public static Character_Move Instance;

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed = 0.1f;              // �⺻ �̵� �ӵ�
    public float runSpeed = 0.05f;          // �޸��� �߰� �ӵ�
    public int walkCount = 20;              // ���� ��

    private float applyRunSpeed;
    private bool applyRunFlag = false;
    private int currentWalkCount;
    private bool canMove = true;

    private Vector3 vector;
    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        // �����: �� Ȯ��
        Debug.Log("speed: " + speed + ", walkCount: " + walkCount);
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            applyRunFlag = Input.GetKey(KeyCode.LeftShift);
            applyRunSpeed = applyRunFlag ? runSpeed : 0;

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // �밢�� ����
            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // �浹 üũ
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);
            Debug.DrawLine(start, end, Color.red, 0.5f); // ȭ�鿡 �� ǥ��

            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
            {
                Debug.Log("�̵� �ߴ�: �浹 ���� - " + hit.transform.name);
                break;
            }

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), vector.y * (speed + applyRunSpeed), 0);
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;
        }

        animator.SetBool("Walking", false);
        canMove = true;
    }

    void Update()
    {
        if (canMove && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            canMove = false;
            StartCoroutine(MoveCoroutine());
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance != null)
        {
            transform.position = GameManager.Instance.nextSpawnPosisition;
            Debug.Log("�� ��ȯ �� ��ġ: " + GameManager.Instance.nextSpawnPosisition);
        }
        else
        {
            Debug.LogWarning("GameManager.Instance�� null�Դϴ�.");
        }
    }
}
