using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach(RespawnGimmick gimmik in GetComponentsInChildren<RespawnGimmick>())
        {
            gimmik.RespawnCheck();
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
