using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RespawnGimmick : SaveGimmick
{
    [SerializeField] long respawnSecond;
    protected long SaveData
    {
        get
        {
            if (!DataManager.CheckGameData(GimmickId))
            {
                return NowTick;
            }
            return DataManager.LoadGameData<RespawnSaveData>(GimmickId).tick;
        }
        set {
            RespawnSaveData data = new();
            data.tick = value;
            DataManager.SaveGameData(GimmickId, data);
        }
    }

    //protected override string GimmickId { get { return "Gimmicks/RespawnGimmick" + GetInstanceID(); } }
    long NowTick { get { return DateTime.Now.Ticks / 10000000; } }

    protected override void Start()
    {
        base.Start();
        //SpawnCheck();
    }

    public override void SpawnCheck()
    {
        if((DataManager.CheckGameData(GimmickId) && SaveData + respawnSecond > NowTick))
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
        SaveData = NowTick;
        SpawnCheck();
    }
}
