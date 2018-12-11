using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BaseEnemyAttack
{
    [SerializeField]
    private List<Status> status;

    void Start()
    {
    }

    void Update()
    {
        foreach (Status it in status)
        {
            SetBullet(it);
        }
    }
}