﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //アイテムと各アイテムのマネージャーを管理する
    private List<ItemMover> items = new List<ItemMover>();
    public List<ItemMover> Items { get { return items; } set {items = value; } }
    private List<BaseItemManager> itemManagers = new List<BaseItemManager>();
    public List<BaseItemManager> ItemManagers { get { return itemManagers; } set { itemManagers = value; } }
    private WallHitter wallHItter;

    // Use this for initialization
    void Start()
    {
        wallHItter = GameObject.Find("GameManager").GetComponent<WallHitter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Items.Count == 0) return;
        foreach (ItemMover it in Items)
        {
            if (it == null || it.gameObject == null) continue;
            if (wallHItter.IsHit(it.gameObject, HitPointFlag.Bottom))
            {
                Destroy(it.gameObject);
            }
            else if (wallHItter.IsHit(it.gameObject, HitPointFlag.Left | HitPointFlag.Right))
            {
                it.SpeedReturn();
            }
        }

        Items.RemoveAll(a => a == null);
    }

    public void GetItem(ItemMover myItem)
    {
        foreach (BaseItemManager it in ItemManagers)
        {
            if (it.ItemName != myItem.name) continue;
            it.GetItem();
            Items.Remove(myItem);
            Destroy(myItem.gameObject);
        }
    }
}