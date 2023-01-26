using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpMonitoring : MonoBehaviour
{
    Text ui;

    void Start()
    {
        ui = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Victim[] a = FindObjectsOfType<Victim>();
        GameObject[] units = new GameObject[a.Length];
        for(int i = 0; i < a.Length; i++)
        {
            units[i] = a[i].gameObject;
        }
        string[] texts = new string[a.Length];
        for(int j = 0; j < a.Length; j++)
        {
            texts[j] = "Ä³¸¯ÅÍ : " + units[j].name + " - H : " + units[j].GetComponent<Victim>().Hp+"/"+ units[j].GetComponent<Victim>().MaxHp;
        }
        string text="";
        foreach(string b in texts)
        {
            text = text + b+"\n";
        }
        ui.text = text;
    }
}
