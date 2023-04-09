using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour_New : MonoBehaviour
{
    #region Variables

#if UNITY_EDITOR
    [Multiline]
    public string _developmentDescription = "";
#endif // UNITY_EDITOR

    public int _animationIndex;

    public int _priority;

    public int _damage = 10;
    public float _range = 3f;

    [SerializeField]
    protected float _coolTime;
    protected float _calcCoolTime = 0.0f;

    public GameObject _effectPrefab;

    [HideInInspector]
    public LayerMask _targetMask;

    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        _calcCoolTime = _coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_calcCoolTime < _coolTime)
        {
            _calcCoolTime += Time.deltaTime;
        }
    }

    public abstract void ExecuteAttack(GameObject target = null, Transform startPoint = null);
}
