using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private List<GameObject> isHitObject = new List<GameObject>();
    public List<GameObject> IsHitObject { get { return isHitObject; } private set { isHitObject = value; } }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool Search(GameObject myObject)
    {
        if(IsHitObject.Contains(myObject)) return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsHitObject.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsHitObject.Remove(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsHitObject.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsHitObject.Remove(collision.gameObject);
    }
}