using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EnemySettingSaver
{
    [MenuItem("Tools/Save Enemies")]
    public static void SaveEnemies()
    {
        CreateManager cm = GameObject.Find("CreateManager").GetComponent<CreateManager>();
        cm.DataSave();
    }
}