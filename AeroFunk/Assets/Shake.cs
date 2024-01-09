using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is a nifty way to control shaking objects.
//I will give it a more decoupled name, as it is not solely to be used for screen shake.
//This script can control intensity of shaking over time with a curve, and it can control the duration.
public class Shake : MonoBehaviour
{
    public float duration = 1f;
    public AnimationCurve curve;
    public bool start = false;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());

        }
    }
    public void CallShake()
    {
        start = true;
    }
    IEnumerator Shaking() 
    {
        Vector3 startingPosition = transform.position;
            float elapsedTime = 0f;
            
            while (elapsedTime < duration)
            {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startingPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startingPosition;
    }
}
