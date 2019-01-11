using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemManager : MonoBehaviour
{
    //アイテムのマネージャーに張り付けて、この中でアイテムの処理をさせる
    [SerializeField]
    private GameObject itemObject;
    public string ItemName { get { return itemObject.name; } }
    public bool IsStoped { get; set; }

    protected virtual void Awake()
    {
        GetComponent<ItemManager>().ItemManagers.Add(this);
        IsStoped = false;
        enabled = false;
    }

    protected void Update()
    {
        if (!IsStoped)
        {
            if (UseItem())
            {
                enabled = false;
            }
        }
    }

    protected abstract bool UseItem();

    //アイテムを取得した
    public abstract void GetItem();
}