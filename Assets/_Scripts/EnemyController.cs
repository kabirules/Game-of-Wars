using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float movementSpeed;

	// Use this for initialization
	void Start () {
        GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject target = playerArray[0];
        Transform targetTransform = target.GetComponent(typeof(Transform)) as Transform;
        transform.LookAt(targetTransform.position, transform.up);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}
}
