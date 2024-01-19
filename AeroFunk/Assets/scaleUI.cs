using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scaleUI : MonoBehaviour
{
    [SerializeField] RectTransform rt;
    [SerializeField] GameObject textObj;
    public List<RectTransform> menuOptions = new List<RectTransform>();

    Outline tOutline;

    [System.Serializable]
    public struct Scale 
    {
        public float newWidth;
        public float newHeight;
    }

    [System.Serializable]
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


    public float scaleDuration = 0.2f;
    public float translateDuration = 1f;

    IEnumerator currentRoutine;


    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        //rt = GetComponent<RectTransform>();
        currentRoutine = ScaleGUIToCurve(hoverExitScale);
        StartCoroutine(currentRoutine);
        tOutline = textObj.GetComponent<Outline>();
        startPosition = rt.position;
    }

    private void Update()
    {
        /*if (Input.GetButtonDown("Fire1"))
        {
            TranslateMouseDown();
        }*/
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
        StopAllCoroutines();
        currentRoutine = null;
        currentRoutine = TranslateGUIToCurve(pressedTranslate);
        StartCoroutine(TranslateGUIToCurve(pressedTranslate));
    }

    public IEnumerator ScaleGUIToCurve(Scale scaleInput)
    {
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {

            elapsedTime += Time.deltaTime;
            float _curve = scaleCurve.Evaluate(elapsedTime/scaleDuration);
            Debug.Log(elapsedTime);


            rt.sizeDelta = new Vector2((_curve * scaleInput.newWidth),(_curve * scaleInput.newHeight));

            yield return null; // Wait for the next frame
        }
        rt.sizeDelta = new Vector2(scaleCurve.Evaluate(1f)* scaleInput.newWidth, scaleCurve.Evaluate(1f)* scaleInput.newHeight);
    }
    public IEnumerator TranslateGUIToCurve(TranslateInput translateInput)
    {
        float elapsedTime = 0f;
        //Vector2 startPosition = rt.position;
        while (elapsedTime < translateDuration)
        {
            elapsedTime += Time.deltaTime;
            float _curve = translateCurve.Evaluate(elapsedTime / translateDuration);
            Debug.Log(elapsedTime);


            rt.position = new Vector3 (startPosition.x + (_curve * translateInput.newX), rt.position.y /*startPosition.y + (_curve * translateInput.newY)*/,rt.position.z);

            yield return null; // Wait for the next frame
        }
        rt.position = new Vector3(startPosition.x + (translateCurve.Evaluate(1f) * translateInput.newX), rt.position.y/*startPosition.y + (translateCurve.Evaluate(1f) * translateInput.newY)*/, rt.position.z);
    }
}
