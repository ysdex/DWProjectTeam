using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckTargetDetect", story: "Compare values of [CurrentDistance] and [ChaseDistance]", category: "Conditions", id: "07bd959027a57ebd60335c99f2330fda")]
public partial class CheckTargetDetectCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> CurrentDistance;
    [SerializeReference] public BlackboardVariable<float> ChaseDistance;

    public override bool IsTrue()
    {
        if ( CurrentDistance.Value <= ChaseDistance.Value )
        {
            return true;
        }

        return false;
    }
}
