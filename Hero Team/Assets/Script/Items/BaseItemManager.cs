using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemManager : MonoBehaviour
{
    //アイテムのマネージャーに張り付けて、この中でアイテムの処理をさせる
    [SerializeField]
    private GameObject itemObject;
    public string ItemName { get { return itemObject.name; } }

    protected bool isStarted = false;

    protected virtual void Awake()
    {
        GetComponent<ItemManager>().ItemManagers.Add(this);
    }

    protected void Update()
    {
        UseItem();
        if(isStarted)Debug.Log(this);
    }

    protected abstract void UseItem();

    //アイテムを取得した
    public abstract void GetItem();
}