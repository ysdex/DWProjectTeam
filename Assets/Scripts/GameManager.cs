using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string nextMapName;
    public Vector3 nextSpawnPosisition;
    public GameObject healthBarPrefab; // 체력바 프리팹

    private GameObject healthBarInstance;

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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. 체력바 생성/제거
        HandleHealthBar(scene);

        // 2. 플레이어 위치 설정
        if (Character_Move.Instance != null)
        {
            Character_Move.Instance.gameObject.SetActive(true);
            Character_Move.Instance.enabled = true;
            Character_Move.Instance.transform.position = nextSpawnPosisition;
        }

        // 3. 카메라 및 맵 경계 설정
        CameraMove cam = FindFirstObjectByType<CameraMove>();
        MapManager mapManager = FindFirstObjectByType<MapManager>();

        if (cam != null && Character_Move.Instance != null)
        {
            Debug.Log("SetTarget 호출됨");
            cam.SetTarget(Character_Move.Instance.transform);

            if (mapManager != null)
            {
                cam.SetCameraBounds(mapManager.minCameraBounds, mapManager.maxCameraBounds);
            }
        }
    }

    private void HandleHealthBar(Scene scene)
    {
        // 기존 체력바 제거
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
            healthBarInstance = null;
        }

        // 게임 맵인 경우 새 체력바 생성
        if (scene.name == "Library_1st" || scene.name == "Wonhyo_1st" || scene.name == "Jayeon_1st")
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas != null && healthBarPrefab != null)
            {
                healthBarInstance = Instantiate(healthBarPrefab, canvas.transform);
            }
            else
            {
                Debug.LogWarning("체력바 생성 실패: Canvas 또는 healthBarPrefab 없음");
            }
        }
    }
}
