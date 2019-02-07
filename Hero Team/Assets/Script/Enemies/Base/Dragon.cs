using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BaseEnemy
{
    [SerializeField]
    private bool isFirstDefensed;
    [SerializeField]
    private Animator tail;
    [SerializeField]
    private float revivalTime = 0.0f;
    private float revivalCountTime;
    protected override void Awake()
    {
        base.Awake();
        revivalCountTime = 0;
        if (isFirstDefensed)
        {
            tail.SetBool("InFlag", true);
            tail.SetBool("IsMoved", true);
        }
    }

    protected override void Update()
    {
        base.Update();
        TailIn();
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