using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	// Use this for initialization
    public float lifetime = 2f;

    void Start ()
    {
        Destroy (gameObject, lifetime);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
