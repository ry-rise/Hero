using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesDataTable : ScriptableObject
{
    [SerializeField]
    private List<EnemiesSetStatus> status;
    public List<EnemiesSetStatus> Status { get { return status; } set { status = value; } }
}
