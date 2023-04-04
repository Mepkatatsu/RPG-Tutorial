using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AgentControllerCharacter : MonoBehaviour
{
    #region Variables
    private CharacterController _chatacterController;
    private NavMeshAgent _navMeshAgent;
    private Camera _camera;

    private bool _isGrounded = false;

    public LayerMask _groundLayerMask;
    public float _groundCheckDistance = 0.3f;

    private Vector3 _calcVelocity;

    #endregion Variables
    // Start is called before the first frame update
    void Start()
    {
        _chatacterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = true;

        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Process mouse left button input
        if (Input.GetMouseButtonDown(0))
        {
            // Make ray from screen to world
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            // Check hit from ray
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, _groundLayerMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move our character to what we hit
                _navMeshAgent.SetDestination(hit.point);
            }

            if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            {
                _chatacterController.Move(_navMeshAgent.velocity * Time.deltaTime);
            }
            else
            {
                _chatacterController.Move(Vector3.zero);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = _navMeshAgent.nextPosition;
    }
}
