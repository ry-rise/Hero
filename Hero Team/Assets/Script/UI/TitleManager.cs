using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject levelSelect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackTitle()
    {
        title.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void NextLevelSelect()
    {
        title.SetActive(false);
        levelSelect.SetActive(true);
    }
}