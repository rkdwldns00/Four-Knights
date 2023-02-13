using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceGimmick : SaveGimmick
{
    protected bool SaveData
    {
        get
        {
            if (!DataManager.CheckGameData(GimmickId))
            {
                return false;
            }
            return DataManager.LoadGameData<OnceGimmickSaveData>(GimmickId).isUsed;
        }
        set
        {
            OnceGimmickSaveData data = new();
            data.isUsed = value;
            DataManager.SaveGameData(GimmickId, data);
        }
    }

    protected override string GimmickId
    {
        get { return "Gimmicks/OnceGimmick" + GetInstanceID(); }
    }

    public override void Interaction(GameObject eventPlayer)
    {
        SaveData = true;
        SpawnCheck();
    }

    public override void SpawnCheck()
    {
        IsActive = !SaveData;
    }
}
