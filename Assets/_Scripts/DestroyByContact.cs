using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject blood;
	private GameController gameController;
	private EnemyManager playerEM;

	// Use this for initialization
	void Start () {
		// Get player to replace it by the dead enemy in the enemies list
		this.playerEM = GetPlayerEM();
	
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
			EnemyController EnemyController = other.gameObject.GetComponent<EnemyController>();
			if (gameController == null) {
				gameController = RetrieveGameController();
			}
			// Replace the killed enemy by the player
			if (this.playerEM==null) {
				this.playerEM = GetPlayerEM();
			}
			this.playerEM.m_PlayerNumber = EnemyController.enemyNumber;
			gameController.m_Enemies[EnemyController.enemyNumber]=this.playerEM;
			// Destroy object, but play dead animation first. Will be destroyed in EnemyController
			EnemyController.isKilled = true;
			//Destroy(other.gameObject);
            Instantiate(blood, other.transform.position, other.transform.rotation);
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

	private EnemyManager GetPlayerEM()
	{
		GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = playerArray[0];
        playerEM = new EnemyManager();
        playerEM.m_Instance = player;
		return playerEM;
	}
}
