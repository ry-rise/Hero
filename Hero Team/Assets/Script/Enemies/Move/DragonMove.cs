using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMove : BaseEnemyMove
{
    [SerializeField]
    private bool isFirstDefensed;
    [SerializeField]
    private Animator tail;
    [SerializeField]
    private float revivalTime = 0.0f;
    private float revivalCountTime;
    private void Awake()
    {
        revivalCountTime = 0;
        if (isFirstDefensed)
        {
            tail.SetBool("InFlag", true);
            tail.SetBool("IsMoved", true);
        }
    }

    private void Update()
    {
        TailIn();
    }

    public void AnimationFlagChanger(bool flag)
    {
        if (flag)
        {
            tail.SetFloat("MoveSpeed", 1.0f);
        }
        else
        {
            tail.SetFloat("MoveSpeed", 0.0f);
        }
    }

    private void TailIn()
    {
        AnimatorStateInfo stateInfo = tail.GetCurrentAnimatorStateInfo(0);
        if (!tail.GetBool("InFlag") && stateInfo.IsName("TailStay"))
        {
            if (revivalTime <= revivalCountTime)
            {
                tail.SetBool("InFlag", true);
                tail.SetBool("IsMoved", true);
                revivalCountTime = 0;
            }
            else
            {
                revivalCountTime += Time.deltaTime;
            }
        }
    }

    public void TailOut()
    {
        AnimatorStateInfo stateInfo = tail.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("TailStay"))
        {
            tail.SetBool("InFlag", false);
            tail.SetBool("IsMoved", true);
        }
    }
}