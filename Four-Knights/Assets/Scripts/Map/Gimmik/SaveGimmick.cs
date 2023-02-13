using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveGimmick : Interactable
{
    protected virtual string GimmickId { get { return ""; } }

    public abstract void SpawnCheck();
}
