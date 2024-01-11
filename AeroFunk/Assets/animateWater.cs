using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateWater : MonoBehaviour
{

    Material mat;
    float x;
    float y;

    [SerializeField] float _spd=.01f;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (mat != null)
        {

            x += _spd*Time.deltaTime;
            y += _spd*Time.deltaTime;

            mat.SetTextureOffset("_MainTex", new Vector2(x,y));
        }
        else
        {
            Debug.Log("no mat");
        }
    }
}
