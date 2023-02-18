using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveGimmick : Interactable
{
    [SerializeField] int id;
    public int Id { get { return id; } }

    int mapId;


    protected virtual string GimmickId { get { return "Gimmicks/"+"M" + mapId + "G" + Id; } }

    public abstract void SpawnCheck();

    protected virtual void Start()
    {
        Map map = GetComponentInParent<Map>();
        if(map == null)
        {
            Debug.LogError("기믹이 맵밖에 배치되었습니다.");
            gameObject.SetActive(false);
        }
        else
        {
            mapId = map.MapId;
        }
    }
}
