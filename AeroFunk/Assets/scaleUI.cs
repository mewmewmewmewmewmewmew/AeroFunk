using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class scaleUI : MonoBehaviour
{
    [SerializeField] RectTransform rt;

    [System.Serializable]
    public struct Scale 
    {
        public float newWidth;
        public float newHeight;
    }
    public struct TranslateInput
    {
        public float newX;
        public float newY;
    }
    public Scale hoverScale;
    public Scale hoverExitScale;
    public TranslateInput pressedTranslate;
    public AnimationCurve scaleCurve;
    public AnimationCurve translateCurve;


    public float duration = 1f;

    [SerializeField] bool onUIHover;
    IEnumerator currentRoutine;

    // Start is called before the first frame update
    void Start()
    {
        //rt = GetComponent<RectTransform>();
        currentRoutine = ScaleGUIToCurve(hoverExitScale);
        StartCoroutine(currentRoutine);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }
    }

    public void ScaleGUI(Scale scaleInput)
    {
        StopAllCoroutines();
        rt.sizeDelta = Vector2.Lerp(new Vector2(rt.rect.width, rt.rect.height), new Vector2(scaleInput.newWidth,scaleInput.newHeight),0.1f);
    }

    /*public void OnMouseEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        currentRoutine = null;
        currentRoutine = ScaleGUItoCurve(hoverScale);
        StartCoroutine(ScaleGUItoCurve(hoverScale));
        if (onUIHover)
        {
            //ScaleGUI(hoverScale);
        }
        // Code to execute when the pointer enters the UI element
        Debug.Log("Mouse entered!");
    }*/

    /*public void OnMouseExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        currentRoutine = null;
        currentRoutine = ScaleGUItoCurve(hoverExitScale);
        StartCoroutine(ScaleGUItoCurve(hoverExitScale)); 
        if (onUIHover)
        {

        }
        // Code to execute when the pointer exits the UI element
        Debug.Log("Mouse exited!");
    }*/

    public void ScaleMouseEnter()
    {
        //StopAllCoroutines();
        currentRoutine = null;
        currentRoutine = ScaleGUIToCurve(hoverScale);
        StartCoroutine(ScaleGUIToCurve(hoverScale));
    }
    public void ScaleMouseExit()
    {
        //StopAllCoroutines();
        currentRoutine = null;
        currentRoutine = ScaleGUIToCurve(hoverExitScale);
        StartCoroutine(ScaleGUIToCurve(hoverExitScale));
    }
    public void TranslateMouseDown()
    {
        currentRoutine = null;
        currentRoutine = TranslateGUIToCurve(pressedTranslate);
        StartCoroutine(ScaleGUIToCurve(hoverExitScale));

    }

    public IEnumerator ScaleGUIToCurve(Scale scaleInput)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float _curve = scaleCurve.Evaluate(elapsedTime/duration);
            Debug.Log(elapsedTime);

            // Apply the curve value to the object's position
            //rt.sizeDelta = new Vector2(rt.rect.width * (_curve * strMod), rt.rect.height * (_curve * strMod));
            rt.sizeDelta = new Vector2((_curve * scaleInput.newWidth),(_curve * scaleInput.newHeight));
            yield return null; // Wait for the next frame
        }
        rt.sizeDelta = new Vector2(scaleCurve.Evaluate(1f)* scaleInput.newWidth, scaleCurve.Evaluate(1f)* scaleInput.newHeight);
    }
    public IEnumerator TranslateGUIToCurve(TranslateInput translateInput)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float _curve = scaleCurve.Evaluate(elapsedTime / duration);
            Debug.Log(elapsedTime);

            // Apply the curve value to the object's position
            //rt.sizeDelta = new Vector2(rt.rect.width * (_curve * strMod), rt.rect.height * (_curve * strMod));
            rt.anchoredPosition = new Vector2 ( _curve * translateInput.newX, _curve * translateInput.newY);
            yield return null; // Wait for the next frame
        }
        rt.sizeDelta = new Vector2(scaleCurve.Evaluate(1f) * translateInput.newX, scaleCurve.Evaluate(1f) * translateInput.newY);
    }
}
