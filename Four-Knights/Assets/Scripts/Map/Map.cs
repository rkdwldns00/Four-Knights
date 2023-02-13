using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach(SaveGimmick gimmik in GetComponentsInChildren<SaveGimmick>())
        {
            gimmik.SpawnCheck();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
