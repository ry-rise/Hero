using System.Collections.Generic;
using UnityEngine;

public class Scorpion : BaseEnemy
{
    [SerializeField] private GameObject Cactus;
    private float time;
    public List<ObjectEnemy> CactusObjects;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if (time >= 3.0f)
        {
            GameObject CactusInstance = Instantiate(Cactus, 
                                        new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, -2.0f), 0), 
                                        Quaternion.identity);
            CactusObjects.Add(CactusInstance.GetComponent<ObjectEnemy>());
        }
        time = 0;
    }

    protected override void MoveSwitch()
    {
        if (move == null) return;
        if ((stop & StopStatus.MoveStoped) != StopStatus.MoveStoped)
        {
            if (!move.enabled)
            {
                ((ScorpionMove)move).AnimationFlagChanger(true);
                move.enabled = true;
            }
        }
        else
        {
            if (move.enabled)
            {
                ((ScorpionMove)move).AnimationFlagChanger(false);
                move.enabled = false;
            }
        }
    }

    protected override void Die()
    {
        foreach (ObjectEnemy it in CactusObjects)
        {
            if (it != null)
            {
                it.IsDead();
            }
        }
        base.Die();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}

