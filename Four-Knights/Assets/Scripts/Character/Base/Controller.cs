using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Mover mover;
    AnimatorManager animator;

    void Start()
    {
        mover = GetComponent<Mover>();
        animator = GetComponent<AnimatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
