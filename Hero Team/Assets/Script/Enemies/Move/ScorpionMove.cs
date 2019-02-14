using System.Collections.Generic;
using UnityEngine;

public class ScorpionMove : BaseEnemyMove {

    [SerializeField] private Animator scopionAnimation;
    [SerializeField] private GameObject cactus;
    private float timeCount;
    [SerializeField] private float time;
    [SerializeField] private Scorpion scorpion;

    public void Awake()
    {
        scopionAnimation.enabled = false;
    }

    public void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= time)
        {
            GameObject CactusInstance = Instantiate(cactus,
                                        transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, -2.0f), 0),
                                        Quaternion.identity);
            scorpion.CactusObjects.Add(CactusInstance.GetComponent<ObjectEnemy>());
            timeCount = 0;
        }
    }

    public void AnimationFlagChanger(bool flag)
    {
        if (flag)
        {
            scopionAnimation.SetFloat("MoveSpeed", 1.0f);
        }
        else
        {
            scopionAnimation.SetFloat("MoveSpeed", 0.0f);
        }
    }

    public void AnimationStart()
    {
        scopionAnimation.enabled = true;
        scopionAnimation.SetBool("StartFlag", true);
    }
}
