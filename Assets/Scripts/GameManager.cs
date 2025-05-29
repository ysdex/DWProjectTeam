using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string nextMapName;
    public Vector3 nextSpawnPosisition;

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

    // �� �ε� �� ó��
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {

        // 1. ĳ���� ��ġ�� ���� ��ġ�� �ű��
        if (Character_Move.Instance != null)
        {
            Character_Move.Instance.gameObject.SetActive(true);
            Character_Move.Instance.enabled = true;
            Character_Move.Instance.transform.position = nextSpawnPosisition;
        }
        // 2. ī�޶� ���� ������Ʈ
        CameraMove cam = FindFirstObjectByType<CameraMove>();
        MapManager mapManager = FindFirstObjectByType<MapManager>();

        if (cam != null && Character_Move.Instance != null)
        {
            Debug.Log("SetTarget ȣ����");
            // ĳ���͸� ���󰡰� �ٽ� ����
            cam.SetTarget(Character_Move.Instance.transform);

            if (mapManager != null)
            {
                // �ʸ��� �ٸ� ī�޶� ��� ����
                cam.SetCameraBounds(mapManager.minCameraBounds, mapManager.maxCameraBounds);
            }
        }
    }
}