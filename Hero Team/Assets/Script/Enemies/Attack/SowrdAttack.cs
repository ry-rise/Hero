using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowrdAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject slash;
    private LastBoss baseObject;
    [SerializeField]
    private Vector2 addPosition;
    [SerializeField]
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        baseObject = transform.root.GetComponent<LastBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationFlagChanger(baseObject.stop != BaseEnemy.StopStatus.None);
    }

    private void AnimationFlagChanger(bool flag)
    {
        if (!flag)
        {
            animator.SetFloat("MoveSpeed", 1.0f);
        }
        else
        {
            animator.SetFloat("MoveSpeed", 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Stay"))
            {
                Instantiate(slash, baseObject.transform.position + (Vector3)addPosition, slash.transform.rotation);
                animator.SetBool("SlashFlag", true);
            }
        }
    }
}
