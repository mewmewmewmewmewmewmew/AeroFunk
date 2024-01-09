using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    private Vector3 _rotation;
    public float RotationSpeed = 0.3f;

    void Start()
    {
        _rotation = new Vector3(_rotation.x, _rotation.y + RotationSpeed, _rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
}
