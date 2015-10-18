using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GUIText countText;
	public GUIText winText;

	private int count;

	void Start() {
		count = 0;
		UpdateCountText ();
		winText.text = "";
	}

	void UpdateCountText(){
		countText.text = "Count: " + count.ToString();
		if (count >= 12) {
			winText.text = "YOU WIN!!!";
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical); 

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision col){
		Debug.Log(col);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log("collided");
		if (other.gameObject.tag == "PickUp") {
			Debug.Log("tagged");
			other.gameObject.SetActive(false);
			count++;			
			UpdateCountText ();
		}
	}
}
