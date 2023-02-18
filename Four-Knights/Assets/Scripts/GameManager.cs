using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    static ItemTable itemTable;
    [SerializeField] ItemTable UsedItemTable;
    [SerializeField] List<GameObject> mapPrefabList;
    [SerializeField] GameObject playerPrefab;

    GameObject currentMapPrefab;

    public static ItemStaticData[] ItemTable
    {
        get
        {
            return itemTable.Table;
        }
    }

    [SerializeField] GameObject dropedItemPrefab;
    public static GameObject DropedItemPrefab { get; private set; }

    private void Awake()
    {
        itemTable = UsedItemTable;
        DropedItemPrefab = dropedItemPrefab;
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("GameManager가 여러개입니다!");
            Destroy(this);
        }

        ChangeMap(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject FindMapWithId(int id)
    {
        foreach(GameObject map in mapPrefabList)
        {
            if(map.GetComponent<Map>().MapId == id)
            {
                return map;
            }
        }
        return null;
    }

    public void ChangeMap(int id)
    {
        if(currentMapPrefab != null)
        {
            Destroy(currentMapPrefab);
        }
        if(FindObjectOfType<Dungeon>()!= null)
        {
            Destroy(FindObjectOfType<Dungeon>().gameObject);
        }

        currentMapPrefab = Instantiate(FindMapWithId(id),Vector3.zero,Quaternion.identity);
        GameObject player = Instantiate(playerPrefab, currentMapPrefab.transform);
        player.transform.localPosition = currentMapPrefab.GetComponent<Map>().SpawnPos;
    }

    public void StartDungeon(GameObject prefab)
    {
        if (FindObjectOfType<Dungeon>() != null)
        {
            return;
        }

        GameObject map = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        map.GetComponent<Dungeon>().originMap = currentMapPrefab;

        currentMapPrefab.SetActive(false);

        GameObject player = Instantiate(playerPrefab, map.transform);
        player.transform.position = map.GetComponent<Dungeon>().SpawnPos;
    }
}
