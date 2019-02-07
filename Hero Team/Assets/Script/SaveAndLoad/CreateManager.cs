using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class CreateManager : MonoBehaviour
{
    [SerializeField]
    private EnemiesDataTable dataTable;
    public EnemiesDataTable DataTable { get { return dataTable; } private set { dataTable = value; } }

    public void DataSave()
    {
        if (dataTable == null)
        {
            Debug.Log("記録先が存在しません");
            return;
        }
        AssetDatabase.StartAssetEditing();
        dataTable.Clear();   //中身を空にする
        EnemiesIndex index = Resources.Load("Enemies/EnemiesIndex") as EnemiesIndex;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject it in enemies)
        {
            //検索
            int topMatchSize = -1;
            string topName = "";
            foreach (GameObject indexIt in index.Enemies)
            {
                int matchSize = -1;
                if (it.name.IndexOf(indexIt.name) != -1)
                {
                    matchSize = indexIt.name.Length;
                }
                if (topMatchSize < matchSize)
                {
                    topMatchSize = matchSize;
                    topName = indexIt.name;
                }
            }
            Debug.Log(topMatchSize);
            if (topMatchSize == -1)
            {
                Debug.Log("検索一覧に存在しない敵を発見");
                Debug.Log(it.name);
                AssetDatabase.StopAssetEditing();
                return;
            }
            else
            {
                DataTable.Add(topName, it.transform.position, it.GetComponent<BaseEnemy>().DropItem);
            }
        }
        Debug.Log("記録成功");
        AssetDatabase.StopAssetEditing();
        EditorUtility.SetDirty(DataTable);
        AssetDatabase.SaveAssets();
    }
}
#elif UNITY_ANDROID
#endif