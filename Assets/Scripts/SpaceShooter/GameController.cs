using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public GameObject player;
	public GameObject hazard;
	public Vector3 spawnValues;
	public float hazardCount;
	public float maxSpawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText levelText;



	private ShipPlayerController playerController;
	private bool gameOver;
	private bool restart;
	private int score;
	private int level;
	private float hazardsForLevel;
	private float minSpawnWait;

	void Start () 
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		level = 1;

		StartCoroutine(SpawnWaves ());

		if (player != null) {
			playerController = player.GetComponent <ShipPlayerController> ();
		}
		if (playerController == null) 
		{
			Debug.Log ("Cannot find 'ShipPlayerController' script");
		}
	}

	void Update ()
	{
		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		if(GameObject.FindGameObjectsWithTag ("Hazard").Length == 0 && !gameOver){
			levelText.enabled = true;
		}else{
			levelText.enabled = false;
		}

	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		levelText.text = "";

		//each wave
		while (true)
		{
			// increase number of hazards each level
			hazardsForLevel = hazardCount * (1 + ((float) level) / 10);

			//each asteroid
			for (int i = 0; i < hazardsForLevel; i++)
			{
				if(!gameOver){
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);

					// increase chase of fast spawns each level
					minSpawnWait = Random.Range(maxSpawnWait/level, maxSpawnWait);
					yield return new WaitForSeconds (minSpawnWait);
				}
			}

			levelText.text = string.Format("Wave {0}", ++level);
			yield return new WaitForSeconds (waveWait);
			levelText.text = "";


		}
	}


	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	
	}

	void UpdateScore ()
	{
		if (score >= 0) {
			scoreText.text = "Score: " + score;
		} else {
			if(!gameOver){
				
				GameOver();
				GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
				foreach(var hazard in hazards){
					DestroyByContact hazardScript =  hazard.GetComponent <DestroyByContact>();
					hazardScript.Explode();
				}

			}
		}

	}

	public void GameOver ()
	{
		gameOverText.text = "GAME OVER!";
		gameOver = true;
		
		restartText.text = "Press 'R' for Restart";
		restart = true;
	}
	

}


public class Score {

	public string name;
	public int score;

	public Score(string n, int s){
		this.name = n;
		this.score = s;
	}

}


