using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSe : MonoBehaviour
{

    [SerializeField]
    private SeManager se;

    // Use this for initialization
    void Start()
    {
        se.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (se.End())
        {
            Destroy(gameObject);
        }
    }
}