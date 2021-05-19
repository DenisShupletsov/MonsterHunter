using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherecterControler : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;
    private const float _originalPlayerSpeed = .1f;
    private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;

    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerTransform = GetComponent<Transform>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveCherecter(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void MoveCherecter(float horizontalInput, float verticalInput) 
    {
        float _playerSpeed = _originalPlayerSpeed + (0.05f * Input.GetAxis("Runing"));

        Vector3 _cameraPosition = new Vector3(_cameraTransform.position.x, _playerTransform.position.y, _cameraTransform.position.z);

        Vector3 diractionForward = (_playerTransform.position - _cameraPosition) * verticalInput;
        Vector3 diractionRight = _cameraTransform.right * horizontalInput;
        Vector3 diraction = diractionRight + diractionForward;

       _playerRigidbody.MovePosition(_playerTransform.position + diraction.normalized * _playerSpeed);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            _playerTransform.forward = diraction.normalized;

            _playerAnimator.SetBool("IsWalking", true);

            if (Input.GetAxis("Runing") > 0)
                _playerAnimator.SetBool("IsRuning", true);
            else
                _playerAnimator.SetBool("IsRuning", false);
        }
        else 
            _playerAnimator.SetBool("IsWalking", false);
    }
}
