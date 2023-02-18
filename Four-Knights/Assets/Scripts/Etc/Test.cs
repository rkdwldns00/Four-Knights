using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.instance.StartDungeon(prefab);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.instance.ChangeMap(0);
        }
    }
}
