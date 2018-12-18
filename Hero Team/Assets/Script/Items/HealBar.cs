using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBar : BaseItemManager
{

    protected override void UseItem()
    {
        isStarted = false;
    }

    //アイテムを取得した
    public override void GetItem()
    {
        isStarted = true;
    }
}
