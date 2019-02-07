using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGard : MonoBehaviour {
    [SerializeField]
    GameObject DragonWings;
    bool GardActive = false;
	// Use this for initialization
	void Start () {
       GardActive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (GardActive)
        {
            DragonWings.SetActive(true);
        }
	}
}
