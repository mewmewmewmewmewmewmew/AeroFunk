using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinTransform : MonoBehaviour
{
    private float yMin = -0.5f, yMax = 0.5f;
    private float timeValue = 0.0f;
    public float distanceModifier;
    public float timeModifier;

    void Update()
    {
        // Compute the sin position.
        float yValue = Mathf.Sin(timeValue * timeModifier);

        // Now compute the Clamp value.
        float yPos = Mathf.Clamp(yValue, yMin, yMax);

        // Update the position of the cube.
        transform.position = new Vector3(0.0f, yValue*distanceModifier, 0.0f);

        // Increase animation time.
        timeValue = timeValue + Time.deltaTime;

        // Reset the animation time if it is greater than the planned time.
        if (yValue > Mathf.PI * 2.0f)
        {
            timeValue = 0.0f;
        }
    }
}