using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinLevel : MonoBehaviour {

    public GameObject triggerFin;

	// Use this for initialization
	void Start () {
        triggerFin.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter ()
    {
        triggerFin.SetActive(true);
    }
}
