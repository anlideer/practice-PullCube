using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamp : MonoBehaviour {

    public bool lightStar = false;
    public Material defaultMater;
    public Material selfMater;
    public GameObject selfLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == gameObject.tag)
        {
            selfLight.GetComponent<MeshRenderer>().material = selfMater;
            lightStar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == gameObject.tag)
        {
            selfLight.GetComponent<MeshRenderer>().material = defaultMater;
            lightStar = false;
        }
    }
}
