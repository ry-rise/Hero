using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [SerializeField]
    private EnemiesDataTable dataTable;
    public EnemiesDataTable DataTable { get { return dataTable; } private set { dataTable = value; } }

    public void DataSave()
    {
        dataTable.Status.Clear();   //中身を空にする
        dataTable.ShowStatus.Clear();   //中身を空にする
        if (dataTable == null)
        {
            Debug.Log("記録先が存在しません");
            return;
        }
        EnemiesIndex index = Resources.Load("EnemiesIndex") as EnemiesIndex;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject it in enemies)
        {
            //検索
            int topMatchSize = -1;
            string topName = "";
            foreach (GameObject indexIt in index.Enemies)
            {
                int matchSize = it.name.IndexOf(indexIt.name);
                if (topMatchSize < matchSize)
                {
                    topMatchSize = matchSize;
                    topName = indexIt.name;
                }
            }
            if (topMatchSize == -1)
            {
                Debug.Log("検索一覧に存在しない敵を発見");
                Debug.Log(it.name);
                return;
            }
            else
            {
                DataTable.Status.Add(new EnemiesSetStatus(topName, it.transform.position));
                DataTable.ShowStatus.Add(new EnemiesShowStatus(topName, it.transform.position));
            }
        }
        Debug.Log("記録成功");
    }
}