using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtFollow : MonoBehaviour
{
    Vector3 Position;
    public GameObject Target;
    [SerializeField] bool xzOnly;


    // Update is called once per frame
    void Update()
    {
        Position = Target.transform.position;
        if(xzOnly)
        {
            transform.position  = new Vector3(Position.x, transform.position.y, Position.z); 
        }
        //else
        //transform.position = Position;
    }
}
