using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsForKinematics : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision) 
	{
		ContactPoint contact = collision.contacts[0];
		Vector3 pos = contact.point;
		GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = playerArray[0];
		player.transform.position = pos;
	}
	
	void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Bullet")
		{
			Destroy(other.gameObject);
		}
		if (other.tag == "Player")
		{
			float newX;
			float newZ;
			if (other.gameObject.transform.position.x > 0)
			{
				newX = other.gameObject.transform.position.x-0.5f;
			}
			else
			{
				newX = other.gameObject.transform.position.x+0.5f;
			}
			if (other.gameObject.transform.position.z > 0)
			{
				newZ = other.gameObject.transform.position.z-0.5f;
			}
			else
			{
				newZ = other.gameObject.transform.position.z+0.5f;
			}			
			Vector3 getBack = new Vector3(newX, other.gameObject.transform.position.y, newZ);
			other.gameObject.transform.position = getBack;
		}
	}	
}
