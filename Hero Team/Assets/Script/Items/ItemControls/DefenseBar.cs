using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBar : BaseItemManager
{
    private Bar bar;
    [SerializeField]
    private float time;
    private float timeCount;

    protected override void Awake()
    {
        base.Awake();
        timeCount = 0;
        bar = GameObject.Find("Bar").GetComponent<Bar>();
    }

    protected override bool UseItem()
    {
        if (!bar.IsCounterAttacked) return true;    //正直全く意味ない
        if (time <= timeCount)
        {
            bar.IsCounterAttacked = false;
            return true;
        }
        timeCount += Time.deltaTime;
        return false;
    }

    //アイテムを取得した
    public override void GetItem()
    {
        timeCount = 0;
        bar.IsCounterAttacked = true;
    }
}
