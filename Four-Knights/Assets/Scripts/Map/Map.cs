using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] int mapId;
    public int MapId { get { return mapId; } }

    private void OnEnable()
    {
        foreach (SaveGimmick gimmik in GetComponentsInChildren<SaveGimmick>())
        {
            gimmik.SpawnCheck();
        }
    }

    void Start()
    {
        SaveGimmick[] gimmicks = GetComponentsInChildren<SaveGimmick>();
        List<int> list = new List<int>();
        foreach (SaveGimmick gimmick in gimmicks)
        {
            if (list.Contains(gimmick.Id))
            {
                Debug.LogError("±â¹ÍID Áßº¹ ¸Ê : "+gameObject.name);
            }
            else
            {
                list.Add(gimmick.Id);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
