using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform[] spawnPoints;

    [System.Serializable]
    private struct WayPointData
    {
        public GameObject[] wayPoints;
    }

    [SerializeField]
    private WayPointData[] wayPointData;

    private void Awake()
    {
        // ✅ 첫 번째 씬에서 할당하지 않은 경우 자동으로 Player 찾기
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player 오브젝트를 찾을 수 없습니다! Player 태그 확인 필요");
                return; // 더 이상 진행 불가
            }
        }

        // ✅ 각 spawnPoint마다 1개의 enemy 생성
        for (int i = 0; i < spawnPoints.Length; ++i)
        {
            if (i >= wayPointData.Length)
            {
                Debug.LogWarning($"wayPointData 부족: SpawnPoint {i}에 WayPoint 없음");
                continue;
            }

            Vector3 spawnPosition = spawnPoints[i].position;

            GameObject clone = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);

            if (clone.TryGetComponent(out EnemyFSM fsm))
            {
                fsm.Setup(target, wayPointData[i].wayPoints);
            }
            else
            {
                Debug.LogWarning("EnemyPrefab에 EnemyFSM 컴포넌트 없음");
            }
        }
    }
}
