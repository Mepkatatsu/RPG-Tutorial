using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable_New
{
    AttackBehaviour_New _CurrentAttackBehaviour
    {
        get;
    }

    void OnExecuteAttack(int attackIndex);
}
