using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateController_New : MonoBehaviour
{
    public delegate void OnEnterAttackState();
    public delegate void OnExitAttackState();

    public OnEnterAttackState _enterAttackStateHandler;
    public OnExitAttackState _exitAttackStateHandler;

    public bool _IsInAttackState
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        _enterAttackStateHandler = new OnEnterAttackState(EnterAttackState);
        _exitAttackStateHandler = new OnExitAttackState(ExitAttackState);
    }

    #region Helper Methods

    public void OnStartOfAttackState()
    {
        _IsInAttackState = true;
        _enterAttackStateHandler();
    }

    public void OnEndOfAttackState()
    {
        _IsInAttackState = false;
        _exitAttackStateHandler();
    }

    private void EnterAttackState()
    {

    }

    private void ExitAttackState()
    {

    }

    public void OnCheckAttackCollider(int attackIndex)
    {
        GetComponent<IAttackable_New>()?.OnExecuteAttack(attackIndex);
    }

    #endregion Helper Methods
}
