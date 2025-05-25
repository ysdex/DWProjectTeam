using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Self] Navigate To [Target]", category: "Action", id: "d96d2f1d8bf2601c2bc57b68be0a89c0")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private NavMeshAgent agent;
    private const float speed = 1f;

    protected override Status OnStart()
    {
        if (Self?.Value == null || Target?.Value == null)
        {
            Debug.LogError("ChaseAction.OnStart: Self 또는 Target이 null입니다.");
            return Status.Failure;
        }

        agent = Self.Value.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("ChaseAction.OnStart: NavMeshAgent가 없습니다.");
            return Status.Failure;
        }

        agent.speed = speed;
        agent.enabled = true;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self?.Value == null || Target?.Value == null || agent == null)
            return Status.Failure;

        Vector3 targetPos = Target.Value.transform.position;
        Vector3 selfPos = Self.Value.transform.position;

        NavMeshHit hit;
        bool isTargetOnNavMesh = NavMesh.SamplePosition(targetPos, out hit, 1.0f, NavMesh.AllAreas);

        if (isTargetOnNavMesh)
        {
            agent.enabled = true;
            agent.SetDestination(targetPos);
        }
        else
        {
            // NavMesh 경로가 없을 경우 Transform 기반 추적
            agent.enabled = false;
            Vector3 direction = (targetPos - selfPos).normalized;
            Self.Value.transform.position += direction * speed * Time.deltaTime;
        }

        return Status.Running;
    }
}
