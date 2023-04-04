using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerCharacter : MonoBehaviour
{
    #region Variables
    public float _speed = 5f;
    public float _jumpHeight = 2f;
    public float _dashDistance = 5f;

    public float _gravity = -9.81f;
    public Vector3 _drags;

    private CharacterController _chatacterController;

    private bool _isGrounded = false;
    public LayerMask _groundLayerMask;
    public float _groundCheckDistance = 0.3f;

    private Vector3 _calcVelocity;
    #endregion Variables
    // Start is called before the first frame update
    void Start()
    {
        _chatacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _chatacterController.isGrounded;

        if (_isGrounded && _calcVelocity.y < 0)
        {
            _calcVelocity.y = 0;
        }

        // Process move inputs
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _chatacterController.Move(move * Time.deltaTime * _speed);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // Process jump input
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _calcVelocity.y += Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }

        // Process dash input
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, _dashDistance
                * new Vector3(Mathf.Log(1f / (Time.deltaTime * _drags.x + 1)) / -Time.deltaTime, 0, Mathf.Log(1f / (Time.deltaTime * _drags.z + 1)) / -Time.deltaTime));
            _calcVelocity += dashVelocity;
        }

        // Progress gravity
        _calcVelocity.y += _gravity * Time.deltaTime;

        // Process dash ground drags
        _calcVelocity.x /= 1 + _drags.x * Time.deltaTime;
        _calcVelocity.y /= 1 + _drags.y * Time.deltaTime;
        _calcVelocity.z /= 1 + _drags.z * Time.deltaTime;

        _chatacterController.Move(_calcVelocity * Time.deltaTime);
    }

    #region Helper Methods
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * _groundCheckDistance));
#endif

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, _groundCheckDistance, _groundLayerMask))
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
