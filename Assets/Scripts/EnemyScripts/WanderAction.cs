using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wander", story: "[Self] Navigate To WanderPosition", category: "Action", id: "b9900e1eac530dfdbaf4b49235b80b3e")]
public partial class WanderAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private NavMeshAgent agent;
    private Vector3 wanderPosition;
    private float currentWanderTime = 0f;
    private float maxWanderTime = 5f;

    protected override Status OnStart()
    {
        agent = Self.Value.GetComponent<NavMeshAgent>();

        // 임의의 방향으로 목적지 계산
        float wanderRadius = UnityEngine.Random.Range(2.5f, 6f);
        int wanderJitter = UnityEngine.Random.Range(0, 360);
        Vector3 rawWanderPosition = Self.Value.transform.position + Utils.GetPositionFromAngle(wanderRadius, wanderJitter);

        // NavMesh 위의 유효한 위치로 보정
        NavMeshHit hit;
        float sampleMaxDistance = 1.0f;
        if (NavMesh.SamplePosition(rawWanderPosition, out hit, sampleMaxDistance, NavMesh.AllAreas))
        {
            wanderPosition = hit.position;
        }
        else
        {
            // NavMesh 위를 못 찾으면 현재 위치 유지(즉시 Success 반환)
            wanderPosition = Self.Value.transform.position;
            return Status.Success;
        }

        agent.SetDestination(wanderPosition);
        currentWanderTime = Time.time;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 목적지 도달 또는 시간 초과 시 Success 반환
        if ((wanderPosition - Self.Value.transform.position).sqrMagnitude < 0.1f
            || Time.time - currentWanderTime > maxWanderTime)
        {
            return Status.Success;
        }

        // NavMeshAgent가 경로를 찾지 못하면 즉시 Success 반환 (트리가 다음 행동으로 전환)
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            return Status.Success;
        }

        return Status.Running;
    }
}
