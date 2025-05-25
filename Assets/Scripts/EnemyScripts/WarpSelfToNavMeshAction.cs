using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WarpSelfToNavMeshAction", story: "Warp [Self] to the closest NavMesh position", category: "Action", id: "f0f82bf94c38fbb9ff963553ae1e7612")]
public partial class WarpSelfToNavMeshAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        // 워프는 OnUpdate에서 한 번만 실행할 것이므로 Running 반환
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var agent = Self.Value.GetComponent<NavMeshAgent>();
        NavMeshHit hit;
        // 2.0f는 탐색 반경(필요에 따라 조정)
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            // NavMeshAgent를 잠시 비활성화했다가 워프 후 다시 활성화(안정성)
            agent.enabled = false;
            agent.Warp(hit.position);
            agent.enabled = true;
        }
        // 워프가 끝났으니 Success 반환
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}
