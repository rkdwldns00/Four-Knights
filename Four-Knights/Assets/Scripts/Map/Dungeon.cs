using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [HideInInspector]public GameObject originMap;

    [SerializeField] Transform spawnPoint;
    public Vector3 SpawnPos
    {
        get
        {
            if (spawnPoint == null)
            {
                return Vector3.zero;
            }
            else
            {
                return spawnPoint.position;
            }
        }
    }


    public void FinishDungeon()
    {
        originMap.SetActive(true);
        Destroy(gameObject);
    }
}
