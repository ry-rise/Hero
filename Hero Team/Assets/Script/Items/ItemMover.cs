using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;
    private ItemManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        manager.Items.Add(this);
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime);
    }

    public void SpeedReturn()
    {
        speed = new Vector2(-speed.x, speed.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bar")
        {
            manager.GetItem(this);
        }
    }
}