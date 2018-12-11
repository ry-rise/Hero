using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAttack : MonoBehaviour
{
    protected void SetBullet(Status status)
    {
        status.Timer += Time.deltaTime;

        if (status.Timer > status.NextTime)
        {
            Instantiate(status.EnemyBullet, transform.position, Quaternion.identity);
            status.Timer = 0;
        }
    }

    [System.Serializable]
    protected class Status
    {
        [SerializeField]
        private GameObject enemyBullet;
        public GameObject EnemyBullet { get { return enemyBullet; } }
        [SerializeField]
        private float nextTime;
        public float NextTime { get { return nextTime; } }
        private float timer = 0.0f;
        public float Timer { get { return timer; } set { timer = value; } }
    }
}