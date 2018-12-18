using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBar : BaseItemManager
{

    protected override bool UseItem()
    {
        return true;
    }

    //アイテムを取得した
    public override void GetItem()
    {
    }
}
