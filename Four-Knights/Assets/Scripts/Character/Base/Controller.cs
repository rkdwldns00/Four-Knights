using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Mover mover;
    AnimatorManager animator;

    protected virtual void Start()
    {
        mover = GetComponent<Mover>();
        animator = GetComponent<AnimatorManager>();
        if(GetComponent<Controller>() != this)
        {
            Debug.LogError("한 게임오브젝트에 두개의 컨트롤러가 존재합니다.");
            Destroy(this);
        }
    }

    protected virtual void Update()
    {

    }

    protected void Walk(Vector2 vector)
    {
        if (animator != null && animator.IsActing)
        {
            return;
        }
        if(mover == null)
        {
            Debug.LogError("Mover가 존재하지않습니다!");
            return;
        }
        mover.Walk(vector);
    }

    protected void Run(Vector2 vector)
    {
        if (animator != null && animator.IsActing)
        {
            return;
        }
        if (mover == null)
        {
            Debug.LogError("Mover가 존재하지않습니다!");
            return;
        }
        mover.Run(vector);
    }
}
