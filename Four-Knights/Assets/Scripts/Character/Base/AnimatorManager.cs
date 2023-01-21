using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    float actTimer;

    float currentSpeed;
    float targetSpeed;
    float speedChangeRatio = 10;
    bool checkAttackLengthBuffer;

    /// <summary>
    /// Idle = 0, Walk = 1, Run = 2
    /// </summary>
    public int SpeedState
    {
        set
        {
            switch (value)
            {
                case 0:
                    targetSpeed = 0;
                    speedChangeRatio = 5;
                    break;
                case 1:
                    targetSpeed = 0.5f;
                    speedChangeRatio = 10;
                    break;
                case 2:
                    targetSpeed = 0.7f;
                    speedChangeRatio = 10;
                    break;
                default:
                    Debug.LogError("SpeedState에 범위밖의 값을 대입했습니다!");
                    break;
            }
        }
    }

    public bool IsActing
    {
        get { return actTimer > 0; }
    }


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if(animator != null)
        {
            Debug.LogError("애니메이터가 존재하지않습니다!");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        actTimer -= Time.deltaTime;
        if (currentSpeed != targetSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, speedChangeRatio * Time.deltaTime);
            animator.SetFloat("Speed", (float)Math.Round(currentSpeed, 2));
        }
        if (checkAttackLengthBuffer)
        {
            checkAttackLengthBuffer = false;
            actTimer = animator.GetNextAnimatorStateInfo(0).length;
        }
    }

    public void UseAttack(int index)
    {
        animator.SetInteger("AttackIndex", index);
        animator.SetTrigger("UseAttack");
        actTimer = 1;
        checkAttackLengthBuffer = true;
    }

    public void UseSkill()
    {
        animator.SetTrigger("UseSkill");
        actTimer = 1;
        checkAttackLengthBuffer = true;
    }

    public void UseUltimateSkil()
    {
        animator.SetTrigger("UseUltimateSkil");
        actTimer = 1;
        checkAttackLengthBuffer = true;
    }
}
