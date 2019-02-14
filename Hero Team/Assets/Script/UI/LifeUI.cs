using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject lifePrefab;
    [SerializeField]
    private Vector2 startPosition;
    [SerializeField]
    private Vector2 addPosition;
    private List<GameObject> lifes = new List<GameObject>();
    private GameObject canvas;

    // Use this for initialization
    void Start()
    {
        canvas = gameObject;
        SettingLife(true);
    }

    // Update is called once per frame
    void Update()
    {
        SettingLife();
    }

    private void SettingLife(bool isFirst = false)
    {
        if (isFirst)
        {
            for(int n = 0; n < GameManager.PlayerLife; ++n)
            {
                lifes.Add(Instantiate(lifePrefab));
                lifes[n].transform.SetParent(canvas.transform, false);
                lifes[n].GetComponent<RectTransform>().localPosition = startPosition + addPosition * n;
            }
        }
        else
        {
            if (GameManager.PlayerLife < lifes.Count)
            {
                if (lifes.Count == 0) return;
                Destroy(lifes[lifes.Count - 1]);
                lifes.RemoveAt(lifes.Count - 1);
            }
            else if (GameManager.PlayerLife > lifes.Count)
            {
                lifes.Add(Instantiate(lifePrefab));
                lifes[lifes.Count - 1].transform.SetParent(canvas.transform, false);
                lifes[lifes.Count - 1].GetComponent<RectTransform>().localPosition = startPosition + addPosition * (lifes.Count - 1);
            }
        }
    }
}