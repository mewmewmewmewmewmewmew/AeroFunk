using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject g;

    [SerializeField] int _grid = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i < _grid; i++)
        {
            for (int j = 0; j < _grid; j++)
            {
                GameObject _g;
                _g = Instantiate(g);
                _g.transform.position = new Vector3(i*5, 0, j*5);
            }
        }
    }

}
