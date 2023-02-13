using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RespawnGimmick : Interactable
{
    [SerializeField] long respawnSecond;
    protected long SaveData
    {
        get
        {
            if (!DataManager.CheckGameData(GimmikId))
            {
                return NowTick;
            }
            return DataManager.LoadGameData<RespawnSaveData>(GimmikId).tick;
        }
        set {
            RespawnSaveData data = new RespawnSaveData();
            data.tick = value;
            DataManager.SaveGameData(GimmikId, data);
        }
    }

    protected string GimmikId { get { return "Gimmicks/RespawnGimmick" + GetInstanceID(); } }
    long NowTick { get { return DateTime.Now.Ticks / 10000000; } }

    protected virtual void Start()
    {
        RespawnCheck();
    }

    public void RespawnCheck()
    {
        if((DataManager.CheckGameData(GimmikId) && SaveData + respawnSecond > NowTick))
        {
            IsActive = false;
        }
        else
        {
            IsActive = true;
        }
    }

    public override void Interaction(GameObject eventPlayer)
    {
        Debug.Log("ddd");
        SaveData = NowTick;
        RespawnCheck();
    }
}
