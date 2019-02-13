using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPointItem : BaseItemManager
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override bool UseItem()
    {
        ++GameManager.HeroPoint;
        return true;
    }

    //アイテムを取得した
    public override void GetItem()
    {
       
    }
}
