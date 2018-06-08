using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour {

    public float movementSpeed;

    public GameObject enemyBullet;
    public Transform enemyBulletSpawn;
    public float fireRate = 1f;
    private float nextFire;

    public int enemyNumber;

    public Boolean isKilled;

    // Use this for initialization
    void Start () {
        StartCoroutine(FacePlayer());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isKilled)
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
            if (Time.time > nextFire)
            {
                Shoot();
            }
        } else {
            // Enemy is dead, make it smaller until is almost invisble and destroy it.
            GetComponent<Animator>().enabled = false;
            Vector3 scale = transform.localScale;
            Vector3 newScale = new Vector3(scale.x*0.95f, scale.y*0.95f, scale.z*0.95f);
            transform.localScale = newScale;
            if (newScale.x < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator FacePlayer()
    {
        yield return new WaitForSeconds(0f);
        while (true)
        {
            GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
            GameObject target = playerArray[0];
            Transform targetTransform = target.GetComponent(typeof(Transform)) as Transform;
            transform.LookAt(targetTransform.position, transform.up);
            yield return new WaitForSeconds(5f);
        }
    }

    void Shoot()
    {
        nextFire = Time.time + fireRate;
        if (enemyBullet != null)
        {
            Instantiate(enemyBullet, enemyBulletSpawn.position, enemyBulletSpawn.rotation);
        }
    }
}
