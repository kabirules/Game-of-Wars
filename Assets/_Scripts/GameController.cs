using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public CameraControl m_CameraControl; 
    public EnemyController m_EnemyController;

    //public EnemyManager[] m_Enemies;
    public List<EnemyManager> m_Enemies = new List<EnemyManager>();
    private int enemyNumber = 0; // Assign a number to every enemy. The player will be number 0.

    public bool playerKilled;
    public bool levelCompleted;
    public Text textGameOver;
    public Text textKills;
    public Text textInfo;
    public Button homeButton;
    public Button againButton;
    public Button nextButton;

    private int score;
    public int enemiesToKill = 3;

    void Start()
    {
        this.score = 0;
        this.playerKilled = false;
        this.levelCompleted = false;
        this.textGameOver.text = "";
        this.textInfo.text = "Kill " + this.enemiesToKill + " enemies!";
        this.textKills.text = "Kills: -";
        this.homeButton.gameObject.SetActive(false);
        this.againButton.gameObject.SetActive(false);
        this.nextButton.gameObject.SetActive(false);
        // Add the player in the array to set the camera position/rotation
        AddPlayer();

        StartCoroutine(SpawnWaves());
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.LoadLevel("Home"); 
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
       // while (!playerKilled)
       // {
            this.textInfo.text = "";
            for (int i = 0; i < hazardCount && !this.playerKilled && !this.levelCompleted; i++)
            {
                //Instantiate a new enemy
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 
                                                    spawnValues.y, 
                                                    Random.Range(-spawnValues.z, spawnValues.z));
                Quaternion spawnRotation = Quaternion.identity;
                GameObject newEnemy = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;
                enemyNumber++;
                EnemyManager em = new EnemyManager();
                em.m_Instance = newEnemy;
                em.m_PlayerNumber = enemyNumber;
                m_Enemies.Add(em);
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                enemyController.enemyNumber = enemyNumber;
                SetCameraTargets();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
      //  }
    }

    private void SetCameraTargets()
    {   
        int x = 0;
        for (int i = 0; i < m_Enemies.Count; i++)
        {
            if (m_Enemies[i].m_Instance != null) {
                x++;
            }
        }
        Transform[] targets = new Transform[x];//m_Enemies.Count];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Enemies[i].m_Instance.transform;
        }
        m_CameraControl.m_Targets = targets;
    }

    // Add the player to the list of targers of the camera
    private void AddPlayer()
    {
        GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = playerArray[0];
        EnemyManager em = new EnemyManager();
        em.m_Instance = player;
        em.m_PlayerNumber = enemyNumber;
        m_Enemies.Add(em);
    }

    public void AddScore (int newScoreValue)
    {
        this.score += newScoreValue;
        UpdateScore ();
        if (this.score >= this.enemiesToKill)
        {
            LevelCompleted();
        }
    }

    void UpdateScore ()
    {
        textKills.text = "Kills: " + this.score;
    }    

    // Make sure the camera won't move, just focus on the player
    public void GameOver() 
    {
        this.playerKilled = true;
        this.textGameOver.text = "GAME OVER!";
        this.homeButton.gameObject.SetActive(true);
        this.againButton.gameObject.SetActive(true);
        m_Enemies.Clear();
        AddPlayer();
    }

    public void LevelCompleted()
    {
        this.levelCompleted = true;
        this.textInfo.text = "Level completed!";
        this.nextButton.gameObject.SetActive(true);
        m_Enemies.Clear();
        AddPlayer();        
    }
}
