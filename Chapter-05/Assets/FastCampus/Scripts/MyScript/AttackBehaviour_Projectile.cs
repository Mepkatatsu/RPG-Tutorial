using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour_Projectile : AttackBehaviour_New
{
    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null)
    {
        if (target == null)
        {
            return;
        }

        Vector2 projectilePosition = startPoint?.position ?? transform.position;
        if (_effectPrefab)
        {
            GameObject projectileGO = Instantiate(_effectPrefab, projectilePosition, Quaternion.identity);
            projectileGO.transform.forward = transform.forward;

            Projectile_New projectile = projectileGO.GetComponent<Projectile_New>();
            if (projectile)
            {
                projectile._owner = gameObject;
                projectile._target = target;
                projectile._attackBehaviour = this;
            }
        }

        _calcCoolTime = 0.0f;
    }
}
