using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string showedName;
    GameObject activeSymbol;
    protected GameObject ActiveSymbol
    {
        get { return activeSymbol; }
        set
        {
            activeSymbol = value;
            activeSymbol.SetActive(IsActive);
        }
    }

    bool isActive = true;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        protected set
        {
            isActive = value;
            if (activeSymbol != null)
            {
                activeSymbol.SetActive(value);
            }
        }
    }

    public abstract void Interaction(GameObject eventPlayer);
}
