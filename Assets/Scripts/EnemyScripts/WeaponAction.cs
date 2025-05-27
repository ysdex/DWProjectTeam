using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Weapon", story: "try attack with [CurrentWeapon]", category: "Action", id: "53f09a225e15e8116138ff9a0699069a")]
public partial class WeaponAction : Action
{
    [SerializeReference] public BlackboardVariable<WeaponBase> CurrentWeapon;

    protected override Status OnUpdate()
    {
        // EnemyFlip은 WeaponBase의 부모 계층에 있다고 가정
        var enemyFlip = CurrentWeapon.Value.GetComponentInParent<EnemyFlip>();
        if (enemyFlip != null)
            enemyFlip.IsAttacking = true;

        CurrentWeapon.Value.TryAttack();

        // 공격이 한 프레임에 끝난다면 바로 false로
        if (enemyFlip != null)
            enemyFlip.IsAttacking = false;

        return Status.Success;
    }
}
