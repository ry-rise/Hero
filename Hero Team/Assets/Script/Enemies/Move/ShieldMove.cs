using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMove : BaseEnemyMove
{
    public float speed = 1f;
    public float width = 2f;
    public float height = 2f;
    public GameObject target;
    Vector3 pos;
    public float pointx;
    public float pointy;
    public Directionofrotation Direction;
   public enum Directionofrotation
    {
        上,下
    }
    private int startDirection;
        private void Start()
    {
        pos = target.transform.position;
        pos.x += pointx ;//中心
        pos.y += pointy;
        startDirection = (int)Direction;

    }
    void Update()
    {


        if (startDirection == (int)Directionofrotation.上)
        {
            pos.x += Mathf.Sin(Time.time * speed) * width;
            pos.y += Mathf.Cos(Time.time * speed) * height;
            this.transform.position = pos;
        }
        if (startDirection == (int)Directionofrotation.下)
        {
            pos.x += Mathf.Sin(Time.time * speed) * width;
            pos.y -= Mathf.Cos(Time.time * speed) * height;
            this.transform.position = pos;
        }

    }
}

