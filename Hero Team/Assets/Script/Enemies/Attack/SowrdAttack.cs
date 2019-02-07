using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowrdAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject slash;
    [SerializeField]
    private GameObject baseObject;
    [SerializeField]
    private Vector2 addPosition;
    [SerializeField]
    private Animator animator;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
