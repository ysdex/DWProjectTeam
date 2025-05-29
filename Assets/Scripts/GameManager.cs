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

    // 씬 로드 후 처리
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {

        // 1. 캐릭터 위치를 스폰 위치로 옮기기
        if (Character_Move.Instance != null)
        {
            Character_Move.Instance.gameObject.SetActive(true);
            Character_Move.Instance.enabled = true;
            Character_Move.Instance.transform.position = nextSpawnPosisition;
        }
        // 2. 카메라 설정 업데이트
        CameraMove cam = FindFirstObjectByType<CameraMove>();
        MapManager mapManager = FindFirstObjectByType<MapManager>();

        if (cam != null && Character_Move.Instance != null)
        {
            Debug.Log("SetTarget 호출함");
            // 캐릭터를 따라가게 다시 설정
            cam.SetTarget(Character_Move.Instance.transform);

            if (mapManager != null)
            {
                // 맵마다 다른 카메라 경계 설정
                cam.SetCameraBounds(mapManager.minCameraBounds, mapManager.maxCameraBounds);
            }
        }
    }
}