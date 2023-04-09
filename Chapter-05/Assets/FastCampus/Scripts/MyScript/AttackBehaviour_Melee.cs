using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour_Melee : AttackBehaviour_New
{
    public ManualCollision_New _attackCollision;
    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null)
    {
        Collider[] colliders = _attackCollision?.CheckOverlapBox(_targetMask);

        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<IDamageable_New>()?.TakeDamage(_damage, _effectPrefab);
            Debug.Log(collider.name + " Hit");
        }
    }
}
