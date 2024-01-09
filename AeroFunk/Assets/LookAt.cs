using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Vector3 _pos;
    void Start()
    {

    }

    void Update()
    {
        _pos = target.transform.position;
        transform.LookAt(_pos);
    }
}