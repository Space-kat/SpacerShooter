using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
 
	public GameObject explosion;
	public int scoreValue;

	
	private ShipPlayerController playerController;
	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) 
		{
			playerController = playerObject.GetComponent <ShipPlayerController>();
		}
		if (playerController == null) 
		{
			Debug.Log ("Cannot find 'ShipPlayerController' script");
		}
	}


	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary") {
			return;
		}  
		if (other.tag == "Player") {
			playerController.Explode();
			gameController.GameOver();
		}


		// Don't add score if asteroids collide or anything else makes them explode beside shots fired
		if ( other.tag == "Bolt") {
			gameController.AddScore (scoreValue);
		}

		Destroy(other.gameObject);
		Explode ();
	}

	public void Explode()
	{
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

}