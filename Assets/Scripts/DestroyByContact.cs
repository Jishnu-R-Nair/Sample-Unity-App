using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;

	public int scoreValue;
	public GameController gameController;
	string lifeDown = "Collided , life gone !";
	public int livesCount = 3; //
	string livesCountKey = "LivesCount"; //


	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameControllerObject == null) {
			Debug.Log ("Can not find 'GameController' Script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") && other.tag != "Player" ) 
		{
			return;
		}
		if (explosion != null ) 
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}
		if (other.CompareTag("Player")) {
			gameController.AddScore (-1 * scoreValue);
			livesCount = PlayerPrefs.GetInt (livesCountKey, 3);
			livesCount--;
			PlayerPrefs.SetInt(livesCountKey, livesCount);
			PlayerPrefs.Save();
			gameController.NotificationChange (lifeDown);
			gameController.LivesCount (livesCount);
			if (livesCount == 0) 
			{
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				gameController.GameOver ();
			}
		}
		gameController.AddScore (scoreValue);
		if (livesCount != 0) {
			if (other.tag != "Player") {
				Destroy (other.gameObject);
			}
		} 

		else 
		{
			Destroy (other.gameObject);
			livesCount = 3;
		}

		Destroy (gameObject);
	}
		

}
