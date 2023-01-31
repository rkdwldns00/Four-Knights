using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    List<Interactable> list = new List<Interactable>();
    public List<Interactable> List
    {
        get
        {
            List<Interactable> l = new List<Interactable>();
            foreach (Interactable interactable in list)
            {
                if (interactable.IsActive)
                {
                    l.Add(interactable);
                }
            }
            return l;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dddd");
        if (other.GetComponent<Interactable>() != null)
        {
            list.Add(other.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            list.Remove(other.GetComponent<Interactable>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach(Interactable interactable in list)
            {
                if (interactable.IsActive)
                {
                    interactable.Interaction(gameObject);
                    break;
                }
            }
        }
    }
}
