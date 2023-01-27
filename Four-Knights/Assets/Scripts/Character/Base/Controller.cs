using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Mover mover;
    AnimatorManager animator;
    Attacker attacker;

    protected virtual void Start()
    {
        mover = GetComponent<Mover>();
        animator = GetComponent<AnimatorManager>();
        attacker = GetComponent<Attacker>();
        if(GetComponent<Controller>() != this)
        {
            Debug.LogError("�� ���ӿ�����Ʈ�� �ΰ��� ��Ʈ�ѷ��� �����մϴ�.");
            Destroy(this);
        }
    }

    protected virtual void Update()
    {

    }

    protected void Walk(Vector2 vector)
    {
        /*if (animator != null && animator.IsActing)
        {
            return;
        }*/
        if(mover == null)
        {
            Debug.LogError("Mover�� ���������ʽ��ϴ�!");
            return;
        }
        mover.Walk(vector);
    }

    protected void Run(Vector2 vector)
    {
        /*if (animator != null && animator.IsActing)
        {
            return;
        }*/
        if (mover == null)
        {
            Debug.LogError("Mover�� ���������ʽ��ϴ�!");
            return;
        }
        mover.Run(vector);
    }

    protected void UseAttack()
    {
        if(attacker == null)
        {
            Debug.LogError("Attacker�� ���������ʽ��ϴ�!");
            return;
        }
        attacker.UseAttack();
    }

    protected void UseSkill()
    {
        if (attacker == null)
        {
            Debug.LogError("Attacker�� ���������ʽ��ϴ�!");
            return;
        }
        attacker.UseSkill();
    }

    protected void UseUltimateSkill()
    {
        if (attacker == null)
        {
            Debug.LogError("Attacker�� ���������ʽ��ϴ�!");
            return;
        }
        attacker.UseUltimateSkill();
    }
}
