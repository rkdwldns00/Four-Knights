using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] bool footIKEnable;
    [Header("IK 설정 (footIK를 쓸경우에만 입력)")]
    [SerializeField] GameObject footCollider;

    Animator animator;
    float attackStunTimer;
    float attackAnimationTimer;

    float currentSpeed;
    float targetSpeed;
    float speedChangeRatio = 10;
    bool checkAttackLengthBuffer;

    float ikTimer;
    float ikRayLength;
    [SerializeField] Transform LeftKnee;
    [SerializeField] Transform LeftFoot;
    [SerializeField] Transform RightKnee;
    [SerializeField] Transform RightFoot;
    [SerializeField] float footHeight;
    float ikKneeHeight;
    Vector3 targetFootPos;

    /// <summary>
    /// Idle = 0, Walk = 1, Run = 2
    /// </summary>
    public int SpeedState
    {
        set
        {
            
            if (!IsActing)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Speed") && !animator.GetNextAnimatorStateInfo(0).IsName("Speed") && value != 0)
                {
                    animator.SetTrigger("TryMove");
                }
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
    }

    /// <summary>
    /// 행동중 여부 판단
    /// false일때만 행동가능하게 설정용
    /// </summary>
    public bool IsActing
    {
        get { return attackStunTimer > 0; }
    }

    public bool IsIdle
    {
        get { return attackAnimationTimer <= 0; }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (footIKEnable)
        {
            ikKneeHeight = (LeftKnee.position.y + RightKnee.position.y) / 2 - (LeftFoot.position.y + RightFoot.position.y) / 2;
            ikRayLength = ikKneeHeight * 2;
        }
        if (animator == null)
        {
            Debug.LogError("애니메이터가 존재하지않습니다!");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ikTimer += Time.deltaTime;
        attackStunTimer -= Time.deltaTime;
        attackAnimationTimer -= Time.deltaTime;
        if (currentSpeed != targetSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, speedChangeRatio * Time.deltaTime);
            animator.SetFloat("Speed", (float)Math.Round(currentSpeed, 2));
        }
        if (checkAttackLengthBuffer)
        {
            checkAttackLengthBuffer = false;
            attackAnimationTimer = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        if (footIKEnable)
        {
            footCollider.transform.localPosition = Vector3.Lerp(footCollider.transform.localPosition, targetFootPos, Time.deltaTime * 3);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!footIKEnable)
        {
            return;
        }
        if (animator.GetFloat("Speed") == 0f && IsIdle && attackAnimationTimer <= - 1.92f)
        {
            RaycastHit leftFootHit;
            RaycastHit rightFootHit;
            bool isLeftHit;
            bool isRightHit;
            isLeftHit = Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + new Vector3(0,ikKneeHeight,0), Vector3.down, out leftFootHit, ikRayLength, 1 << LayerMask.NameToLayer("Map"));
            isRightHit = Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.RightFoot) + new Vector3(0, ikKneeHeight, 0), Vector3.down, out rightFootHit, ikRayLength, 1 << LayerMask.NameToLayer("Map"));

            if (!isLeftHit && !isRightHit)
            {
                ikTimer = 0;
                targetFootPos = Vector3.zero;
                footCollider.transform.localPosition = targetFootPos;
            }
            else
            {
                if (ikTimer > 0.15f && ikTimer < 1)
                {
                    ikTimer = 1;
                    targetFootPos = new Vector3(0, ((leftFootHit.distance) + (rightFootHit.distance)) / 2 - ikKneeHeight - footHeight, 0);
                }
            }
            if (ikTimer >= 1)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootHit.point + Vector3.up * footHeight);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootHit.point + Vector3.up * footHeight);
            }

        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            targetFootPos = Vector3.zero;
            footCollider.transform.localPosition = targetFootPos;
            ikTimer = 0;
        }
    }

    public void UseAttack(int index,float stunTime)
    {
        animator.SetInteger("AttackIndex", index);
        animator.SetTrigger("UseAttack");
        attackAnimationTimer = 1;
        attackStunTimer = stunTime;
        SpeedState = 0;
        checkAttackLengthBuffer = true;
    }

    public void UseSkill(float stunTime)
    {
        animator.SetTrigger("UseSkill");
        attackAnimationTimer = 1;
        attackStunTimer = stunTime;
        SpeedState = 0;
        checkAttackLengthBuffer = true;
    }

    public void UseUltimateSkil(float stunTime)
    {
        animator.SetTrigger("UseUltimateSkil");
        attackAnimationTimer = 1;
        attackStunTimer = stunTime;
        SpeedState = 0;
        checkAttackLengthBuffer = true;
    }
}
