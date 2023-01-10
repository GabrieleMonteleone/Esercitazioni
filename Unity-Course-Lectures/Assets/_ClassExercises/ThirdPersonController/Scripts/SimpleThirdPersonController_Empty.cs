using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleThirdPersonController_Empty : MonoBehaviour
{
    //Which public variables do you need?
    // A Camera
    // A Rotation Speed
    // A Movement Speed
    public Camera Camera;
    public float RotSpeed=3;
    public float Speed=2;

    private Vector3 _inputvector;
    private Vector3 _targetDirection;
    private float _inputSpeed;
    void Update()
    {
        //Get the Input using Input.GetAxis() & assign the values to a new direction Vector3
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _inputvector = new Vector3(h,0,v);
        _inputSpeed = Mathf.Clamp(_inputvector.magnitude, 0, 1);

        //Compute direction According to Camera Orientation (use function TransformDirection)
        //Reference: https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
       _targetDirection = Camera.transform.TransformDirection(_inputvector).normalized;
        _targetDirection.y = 0;

        //Calculate rotation vector and rotate the object, you can use Quaternion.LookRotation() funcation.
        //Reference: https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
        if (_inputSpeed < 0)
            return;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, RotSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Translate along forward
        transform.Translate(transform.forward * _inputSpeed * Speed * Time.deltaTime, Space.World);
    }
}
