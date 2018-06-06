using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	private GameController gameController;
	private EnemyManager playerEM;

	// Use this for initialization
	void Start () {
		// Get player to replace it by the dead enemy in the enemies list
        GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = playerArray[0];
        playerEM = new EnemyManager();
        playerEM.m_Instance = player;
	
		gameController = RetrieveGameController();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        //Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver ();
        }
        if (other.tag == "Enemy")
        {
			EnemyController enemyManager = other.gameObject.GetComponent<EnemyController>();
			if (gameController == null) {
				gameController = RetrieveGameController();
			}
			// Replace the killed enemy by the player
			playerEM.m_PlayerNumber = enemyManager.enemyNumber;
			gameController.m_Enemies[enemyManager.enemyNumber]=playerEM;
			// Destroy object, but play dead animation first
			Destroy(other.gameObject);
            //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver ();
        }		
        //Destroy(other.gameObject);
		//DeactivateChildren(other.gameObject, true);
        Destroy(gameObject);
    }

	void DeactivateChildren(GameObject g, bool a) {
		g.SetActive(a);
		
		foreach (Transform child in g.transform) {
			DeactivateChildren(child.gameObject, a);
    	}
	}

	private GameController RetrieveGameController() {
		GameObject[] gameControllerArray = GameObject.FindGameObjectsWithTag("GameController");
        GameObject gameControllerObj = gameControllerArray[0];
		gameController = gameControllerObj.GetComponent<GameController>();
		return gameController;
	}
}
