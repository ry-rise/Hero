using System.Collections.Generic;
using UnityEngine;

public class ScorpionMove : BaseEnemyMove {

    [SerializeField] private Animator scopionAnimation;
    [SerializeField] private GameObject cactus;
    private float time;
    [SerializeField]
    private Scorpion scorpion;

    public void Update()
    {
        time += Time.deltaTime;
        if (time >= 3.0f)
        {
            GameObject CactusInstance = Instantiate(cactus,
                                        new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, -2.0f), 0),
                                        Quaternion.identity);
            scorpion.CactusObjects.Add(CactusInstance.GetComponent<ObjectEnemy>());
            time = 0;
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
}
