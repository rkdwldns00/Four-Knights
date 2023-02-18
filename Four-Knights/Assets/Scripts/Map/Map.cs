using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    public Vector3 SpawnPos {
        get {
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

    [ContextMenu("Check Blank Gimmick ID")]
    public void CheckBlankGimmickId()
    {
        SaveGimmick[] gimmicks = GetComponentsInChildren<SaveGimmick>();
        List<int> list = new List<int>();
        foreach (SaveGimmick gimmick in gimmicks)
        {
            if (list.Contains(gimmick.Id))
            {
                Debug.LogError("±â¹ÍID Áßº¹ ¸Ê : " + gameObject.name);
            }
            else
            {
                list.Add(gimmick.Id);
            }
        }
        for(int i = 0; i < list.Max(); i++)
        {
            if (!list.Contains(i))
            {
                Debug.Log("ºó ID : " + i);
                return;
            }
        }
        Debug.Log("ºó ID : " + (list.Max() + 1));
    }
}
