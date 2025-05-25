using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform[] spawnPoints; // ✅ 새로 추가된 부분

    [System.Serializable]
    private struct WayPointData
    {
        public GameObject[] wayPoints;
    }

    [SerializeField]
    private WayPointData[] wayPointData;

    private void Awake()
    {
        // ✅ 각 spawnPoint마다 1개의 enemy 생성
        for (int i = 0; i < spawnPoints.Length; ++i)
        {
            // 안전 체크: waypoint 배열이 부족하지 않은지
            if (i >= wayPointData.Length)
            {
                Debug.LogWarning($"wayPointData가 부족합니다. SpawnPoint {i}에 대해 설정된 WayPoint가 없습니다.");
                continue;
            }

            Vector3 spawnPosition = spawnPoints[i].position;

            GameObject clone = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);

            EnemyFSM fsm = clone.GetComponent<EnemyFSM>();
            if (fsm != null)
            {
                fsm.Setup(target, wayPointData[i].wayPoints);
            }
            else
            {
                Debug.LogWarning("EnemyPrefab에 EnemyFSM 컴포넌트가 없습니다.");
            }
        }
    }
}
