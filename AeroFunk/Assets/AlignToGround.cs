using UnityEngine;

public class AlignToGround : MonoBehaviour
{
    public float raycastDistance = 5f;

    void Update()
    {
        // Cast a ray downward to detect the ground
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f))
        {
            // Get the normal vector of the surface hit
            Vector3 groundNormal = hit.normal;

            // Align the object to the ground normal
            transform.up = groundNormal+ new Vector3(0,0,0);
        }
    }
}