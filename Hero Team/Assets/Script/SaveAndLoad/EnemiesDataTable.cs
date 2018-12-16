using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesDataTable : ScriptableObject
{
    public List<EnemiesSetStatus> Status { get; set; }

    public List<EnemiesShowStatus> ShowStatus;   //見た目だけなので、ここを変えても意味がない
}
