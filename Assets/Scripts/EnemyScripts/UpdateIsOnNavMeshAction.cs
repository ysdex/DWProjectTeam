using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateIsOnNavMeshAction", story: "Update [isOnNavMesh] with whether [Self] is on NavMesh", category: "Action", id: "9b94d717d924c5dd8d629cc608bdb289")]
public partial class UpdateIsOnNavMeshAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsOnNavMesh;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        IsOnNavMesh.Value = Self.Value.GetComponent<NavMeshAgent>().isOnNavMesh;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}
