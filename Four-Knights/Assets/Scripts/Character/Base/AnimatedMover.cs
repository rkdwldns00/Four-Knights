using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMover : MonoBehaviour, Mover
{
    AnimatorManager animator;
    Quaternion targetDirection;
    Quaternion currentDirection;

    bool walkBuffer;
    bool runBuffer;

    void Start()
    {
        animator = GetComponent<AnimatorManager>();
        if(animator == null)
        {
            Debug.LogWarning("�ִϸ����͸� ã�����߽��ϴ�! : �����ϼ�����");
        }
        currentDirection = transform.rotation;
        targetDirection = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDirection != targetDirection)
        {
            currentDirection = Quaternion.Lerp(currentDirection, targetDirection, Time.deltaTime * 10);
            transform.rotation = currentDirection;
        }
    }

    private void LateUpdate()
    {
        if (animator == null) {
            return;
        }

        if (runBuffer)
        {
            runBuffer = false;
            animator.SpeedState = 2;
        }
        else if (walkBuffer)
        {
            walkBuffer = false;
            animator.SpeedState = 1;
        }
        else
        {
            animator.SpeedState = 0;
        }
    }

    /// <summary>
    /// direction�� ���⺤�� �Է�
    /// </summary>
    /// <param name="direction"></param>
    public void Look(Vector2 direction)
    {
        targetDirection = Quaternion.Euler(0, (Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg) + 90f, 0);
    }

    /// <summary>
    /// direction�� x���ӵ��� y���ӵ� �Է�
    /// </summary>
    /// <param name="direction"></param>
    public void Walk(Vector2 direction)
    {
        walkBuffer = true;
        runBuffer = false;
        Look(direction);
    }

    /// <summary>
    /// direction�� x���ӵ��� y���ӵ� �Է�
    /// </summary>
    /// <param name="direction"></param>
    public void Run(Vector2 direction)
    {
        walkBuffer = false;
        runBuffer = true;
        Look(direction);
    }
}
