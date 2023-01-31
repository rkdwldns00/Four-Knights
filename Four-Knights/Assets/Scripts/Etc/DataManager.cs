using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // ---�̱������� ����--- //
    public static DataManager instance;

    // --- ���� ������ �����̸� ���� ("���ϴ� �̸�(����).json") --- //
    string GameDataFileName = "GameData.json";

    // --- ����� Ŭ���� ���� --- //
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

    // �ҷ�����
    void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
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


    // �����ϱ�
    void SaveGameData()
    {
        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);

        // �ùٸ��� ����ƴ��� Ȯ�� (�����Ӱ� ����)
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