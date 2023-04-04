using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCharacter : MonoBehaviour
{
    #region Variables
    public float _speed = 5f;
    public float _jumpHeight = 2f;
    public float _dashDistance = 5f;

    private Rigidbody _rigidbody;

    private Vector3 _inputDirection = Vector3.zero;

    private bool _isGrounded = false;
    public LayerMask _groundLayerMask;
    public float _groundCheckDistance = 0.3f;

    #endregion Variables
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundStatus();

        // Process user inputs
        _inputDirection = Vector3.zero;
        _inputDirection.x = Input.GetAxis("Horizontal");
        _inputDirection.z = Input.GetAxis("Vertical");
        if (_inputDirection != Vector3.zero)
        {
            transform.forward = _inputDirection;
        }

        // Process jump input
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        // Process dash input
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, _dashDistance
                * new Vector3(Mathf.Log(1f / (Time.deltaTime * _rigidbody.drag + 1)) / -Time.deltaTime, 0, Mathf.Log(1f / (Time.deltaTime * _rigidbody.drag + 1)) / -Time.deltaTime));
            _rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if(_inputDirection != Vector3.zero)
        {
            _rigidbody.MovePosition(transform.position + _inputDirection * _speed * Time.fixedDeltaTime);
        }
    }

    #region Helper Methods
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * _groundCheckDistance));
#endif

        if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, _groundCheckDistance, _groundLayerMask))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }    
    #endregion Helper Methods
}
