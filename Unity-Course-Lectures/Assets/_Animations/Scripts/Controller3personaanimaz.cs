using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller3personaanimaz : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 3f;

   
    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;

    private Animator _animator;
    private bool walk;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        walk = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _inputVector = new Vector3(h, 0, v);
        _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);

        //Compute direction According to Camera Orientation
        _targetDirection = _camera.transform.TransformDirection(_inputVector).normalized;
        _targetDirection.y = 0f;

        if (_inputSpeed <= 0f)
            return;

        UpdateAnimations();
       
        //Calculate rotation vector and rotate
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Translate along forward
        transform.Translate(transform.forward * _inputSpeed * _speed * Time.deltaTime, Space.World);

        Debug.DrawRay(transform.position + transform.up * 3f, _targetDirection * 5f, Color.red);
        Debug.DrawRay(transform.position + transform.up * 3f, newDir * 5f, Color.blue);
    }
    private void UpdateAnimations()
    {

        _animator.SetFloat("speed", _inputSpeed);
        _animator.SetBool("walk", walk);
    }
    }

