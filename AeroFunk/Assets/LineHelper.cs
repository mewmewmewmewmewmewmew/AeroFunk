using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHelper : MonoBehaviour
{
    [SerializeField] GameObject _obj;
    [SerializeField] int _lineCount = 0;
    List<GameObject> _objL= new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _lineCount; i++)
        {
            GameObject _go = Instantiate(_obj,transform);

            _go.transform.position = new Vector3(i* transform.localScale.x *10, transform.position.y, transform.position.z);
            _go.transform.rotation = transform.rotation;
            _objL.Add(_go);
        }
    }


}
