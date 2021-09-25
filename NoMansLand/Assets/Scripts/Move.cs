using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float _userHorizontalInput;

    private const float ScaleMovement = 0.25f;

    private Transform playerTransform;

    private float _userRotInput;

    private Vector3 _userRot;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _userHorizontalInput = Input.GetAxis("Vertical");

        _userRotInput = Input.GetAxis("Horizontal");
        _userRot = playerTransform.rotation.eulerAngles;
        _userRot += new Vector3(0, _userRotInput, 0);

        playerTransform.rotation = Quaternion.Euler(_userRot);
        playerTransform.position += transform.forward * _userHorizontalInput * ScaleMovement;
    }
}
