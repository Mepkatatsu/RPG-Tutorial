using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float _viewRadius = 5f;
    [Range(0, 360)]
    public float _viewAngle = 90f;

    public LayerMask _targetMask;
    public LayerMask _obstacleMask;

    private List<Transform> _visibleTargets = new List<Transform>();
    
    private Transform _nearestTarget;
    private float _distanceToTarget = 0.0f;

    public float delay = 0.2f;

    public List<Transform> _VisibleTargets => _visibleTargets;
    public Transform _NearestTarget => _nearestTarget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindTargetsWithDelay(delay));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        _distanceToTarget = 0.0f;
        _nearestTarget = null;
        _visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleMask))
                {
                    _visibleTargets.Add(target);
                    if (_nearestTarget == null || (_distanceToTarget < distanceToTarget))
                    {
                        _nearestTarget = target;
                        _distanceToTarget = distanceToTarget;
                    }
                }
            }
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
