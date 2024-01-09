using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float cameraSpeed = 0.01f;
    Vector3 _pos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _pos = Vector3.Lerp(_pos, Target.position, cameraSpeed*Time.deltaTime);
        transform.position = new Vector3 (_pos.x, transform.position.y , _pos.z);
    }
}
