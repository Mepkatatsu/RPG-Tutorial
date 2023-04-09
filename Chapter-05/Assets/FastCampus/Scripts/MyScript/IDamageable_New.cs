using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable_New
{
    bool _IsAlive
    {
        get;
    }

    void TakeDamage(int damage, GameObject hitEffectPrefabs);
}
