using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBar : BaseItemManager
{
    private Bar bar;
    [SerializeField]
    private int healPoint;

    protected override void Awake()
    {
        base.Awake();
        bar = GameObject.Find("Bar").GetComponent<Bar>();
    }

    protected override bool UseItem()
    {
        return true;
    }

    //アイテムを取得した
    public override void GetItem()
    {
        bar.Heal(healPoint);
    }
}
