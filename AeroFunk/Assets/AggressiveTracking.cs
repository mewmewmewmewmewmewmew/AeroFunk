using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AggressiveTracking : MonoBehaviour
{
    public GameObject Target;
    public float speed = 1.5f;

    public void Start()
    {
        Target = GameObject.Find("Obj_SkullCapsule");
    }
    public void Update()
    {
        if (!Target)
            return;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
        transform.up = Target.transform.position - transform.position;

/* use transform.up instead of vector3.up!!! 
 * This is because vector3.up changes worldspace direction, while transform.up changes the local direction. 
 * The local direction is the direction the character faces. 
 * The worldspace direction is the direction of global axis. */
}
}
