using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    AnimatedMover mover;
    AnimatorManager animator;

    void Start()
    {
        mover = GetComponent<AnimatedMover>();
        animator = GetComponent<AnimatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SetSpeedState(int state)
    {
        
    }
}
