using UnityEngine;

public class EnemyAttack : BaseEnemyAttack
{
    [SerializeField]
    Status status;

    void Start()
    {
        
    }

    void Update()
    {
        SetBullet(status);
    }
}