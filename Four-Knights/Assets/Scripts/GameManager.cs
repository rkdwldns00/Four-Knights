using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static ItemTable itemTable;
    [SerializeField] ItemTable UsedItemTable;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("GameManager�� �������Դϴ�!");
            Destroy(this);
        }
        itemTable = UsedItemTable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}