using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBar : BaseItemManager
{
    private Bar bar;
    [SerializeField]
    private float time;
    private float timeCount;
    private int pastLevel;

    protected override void Awake()
    {
        base.Awake();
        bar = GameObject.Find("Bar").GetComponent<Bar>();
    }

    protected override bool UseItem()
    {
        if (time <= timeCount)
        {
            bar.ScaleLevel = pastLevel;
            return true;
        }
        timeCount += Time.deltaTime;
        return false;
    }

    //アイテムを取得した
    public override void GetItem()
    {
        timeCount = 0;
        pastLevel = bar.ScaleLevel;
        bar.ScaleLevel = bar.MaxScaleLevel;
    }
}
