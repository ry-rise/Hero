using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesDataTable : ScriptableObject
{
    [SerializeField]
    private List<EnemiesSetStatus> status;
    public IEnumerable<EnemiesSetStatus> Status { get { return status; } }

    public void Clear()
    {
        status.Clear();
    }

    public void Add(string name, Vector2 position, GameObject dropItem)
    {
        status.Add(new EnemiesSetStatus(name, position, dropItem));
    }

}
