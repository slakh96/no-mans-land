using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePhysics : MonoBehaviour
{
    private float _playerInput;
    private float _rotationInput;
    private Vector3 _userRot;
    private bool _userJumped;

    private float JUMP_STRENGTH = 3;

    private Rigidbody _rigidbody;

    private Transform _transform;

    private const float ScaleMovement = 0.7f;

    public GameObject goscreen;

    private const float ScaleJump = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerInput = Input.GetAxis("Vertical");
        _rotationInput = Input.GetAxis("Horizontal");
        _userJumped = Input.GetButton("Jump");
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.collider.tag == "Alien") 
        {
            Destroy(this.gameObject);
            goscreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        _userRot = _transform.rotation.eulerAngles;
        _userRot += new Vector3(0, _rotationInput, 0);
        _transform.rotation = Quaternion.Euler(_userRot);
        _rigidbody.position += _transform.forward * _playerInput * ScaleMovement;

        if (_userJumped)
        {
            _rigidbody.AddForce(Vector3.up * JUMP_STRENGTH, ForceMode.VelocityChange);
            _userJumped = false;
        }
    }
}
