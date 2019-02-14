using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private SeManager tap;
    [SerializeField]
    private TitleManager manager;
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select(int level)
    {
        GameManager.SelectLevel = level;
        tap.Play();
        SceneManager.LoadScene("Stage1");
    }

    public void Back()
    {
        tap.Play();
        manager.BackTitle();
    }
}
