using UnityEngine;
using System.Collections;

[System.Serializable]

public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class ShipPlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	
	public GameObject playerExplosion;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;


	private float nextFire;

	
	void Update ()
	{
	
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
		
			nextFire = Time.time + fireRate;
		//	GameObject clone = 
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); 
		//as GameObject
			GetComponent<AudioSource>().Play ();
		
		
		}
	}

	public ShipPlayerController()
	{
		boundary = new Boundary ();
	}


	void FixedUpdate ()
	{

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		Rigidbody rigidbody = GetComponent<Rigidbody> ();

		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax) 
			);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);

	}


	public void Explode(){
		Transform transform = GetComponent<Transform> ();
		Instantiate (playerExplosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}





}
