using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // ---싱글톤으로 선언--- //
    public static DataManager instance;

    // --- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //
    string GameDataFileName = "GameData.json";

    // --- 저장용 클래스 변수 --- //
    Data data = new Data();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    // 불러오기
    void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(FromJsonData);
        }
    }

    public Item[] GetInventory()
    {
        LoadGameData();
        Item[] inventory = new Item[data.inventory.Length];
        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = new Item();
            inventory[i].id = data.inventory[i].id;
            switch (GameManager.ItemTable[data.inventory[i].id].ItemType)
            {
                case ItemType.Weapon:
                    inventory[i].uniqueData = data.weaponUniqueData[data.inventory[i].uniqueIndex];
                    break;
                case ItemType.Accessories:
                    inventory[i].uniqueData = data.accessoriesUniqueData[data.inventory[i].uniqueIndex];
                    break ;
                case ItemType.Etc:
                    inventory[i].uniqueData = data.etcUniqueData[data.inventory[i].uniqueIndex];
                    break;
            }
        }
        return inventory;
    }


    // 저장하기
    void SaveGameData()
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

        // 올바르게 저장됐는지 확인 (자유롭게 변형)
    }

    public void SetInventoryData(Item[] inventory)
    {
        data.inventory = new ItemWithUniqueIndex[inventory.Length];
        int weaponIndex = 0;
        int accessoriesIndex = 0;
        int etcIndex = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            switch (GameManager.ItemTable[inventory[i].id].ItemType)
            {
                case ItemType.Weapon:
                    weaponIndex += 1;
                    break;
                case ItemType.Accessories:
                    accessoriesIndex += 1;
                    break;
                case ItemType.Etc:
                    etcIndex += 1;
                    break;
            }
        }
        data.weaponUniqueData = new WeaponUniqueData[weaponIndex];
        data.accessoriesUniqueData = new AccessoriesUniqueData[accessoriesIndex];
        data.etcUniqueData = new EtcUniqueData[etcIndex];
        weaponIndex = 0;
        accessoriesIndex = 0;
        etcIndex = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            switch (GameManager.ItemTable[inventory[i].id].ItemType)
            {
                case ItemType.Weapon:
                    data.weaponUniqueData[weaponIndex++] = (WeaponUniqueData)inventory[i].uniqueData;
                    break;
                case ItemType.Accessories:
                    data.accessoriesUniqueData[accessoriesIndex++] = (AccessoriesUniqueData)inventory[i].uniqueData;
                    break;
                case ItemType.Etc:
                    data.etcUniqueData[etcIndex++] = (EtcUniqueData)inventory[i].uniqueData;
                    break;
            }
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            data.inventory[i].id = inventory[i].id;
        }
        SaveGameData();
    }
}