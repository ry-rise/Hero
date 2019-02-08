using System.Collections.Generic;
using UnityEngine;

public class ScorpionMove : BaseEnemyMove {

    [SerializeField] private Animator ScopionAnimation;
    private List<ObjectEnemy> objectEnemies;
    public void AnimationFlagChanger(bool flag)
    {
        if (flag)
        {
            ScopionAnimation.SetFloat("MoveSpeed", 1.0f);
        }
        else
        {
            ScopionAnimation.SetFloat("MoveSpeed", 0.0f);
        }
    }
}
