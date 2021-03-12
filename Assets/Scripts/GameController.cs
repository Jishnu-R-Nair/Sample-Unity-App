using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject[] hazardDoubleBlue;
	public GameObject[] hazardDoubleRed;
	public GameObject[] hazardDoubleBlack;
	public GameObject[] hazards50;
	public GameObject[] hazards200;
	public GameObject[] hazards500;
	public GameObject[] hazards1000;
	public GameObject[] hazards2000;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	//public Text restartText;
	public Text gameOverText;
	public Text highScoreText;
	public Text gameOverScoreText;
	public Text livesText;
	public GameObject restartButton;
	public GameObject menuButton;
	public GameObject scoreBGButton;
	public GameObject livesBGButton;
	public GameObject notificationBGButton;

	private bool gameOver;
	public int score;
	public int highScore = 0;
	public int livesCount = 3; //
	string highScoreKey = "HighScore";
	string livesCountKey = "LivesCount"; //
	private static int gameCount = 0;
	private static int more50 = 0, more200 = 0, more500 = 0, more1000 = 0, more2000 = 0;
	string lifeUp = "Checkscore arrived, one life up!";

	void Start()
	{
		livesText.text = "";
		gameCount++;
		gameOver = false;
		restartButton.SetActive(false);
		menuButton.SetActive (false);
		scoreBGButton.SetActive (false);
		livesBGButton.SetActive (false);
		notificationBGButton.SetActive (false);


		//restartText.text = "";
		gameOverScoreText.text = "";
		gameOverText.text = "";
		highScoreText.text = "";
		scoreText.text = "";
		score = 0;
		highScore = PlayerPrefs.GetInt(highScoreKey,0);
		PlayerPrefs.SetInt(livesCountKey, livesCount);
		PlayerPrefs.Save();
		livesCount = PlayerPrefs.GetInt (livesCountKey, 3); //
		if (gameCount != 1) {
			livesText.text = "Lives : " + livesCount; //
			UpdateScore ();
			StartCoroutine (SpawnWaves ());
		} 

		else 
		{
			gameOverText.GetComponent<RectTransform> ().position = new Vector3 (360, 680, 0.0f);

			highScoreText.GetComponent<RectTransform> ().position = new Vector3 (360, 640, 0.0f);
			gameOverScoreText.GetComponent<RectTransform> ().position = new Vector3 (360, 660, 0.0f);
			restartButton.GetComponent<RectTransform> ().position = new Vector3 (360, 590, 0.0f);
			gameOverText.text = "New Game";
			highScoreText.text = "High Score : " + highScore;
			restartButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (120, 30);
			restartButton.GetComponentInChildren<Text> ().text = "Start";
			menuButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (200, 120);
			menuButton.SetActive (true);
			restartButton.SetActive (true);
		}
	}
		

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				if (score >= 0 && score < 50) {
					GameObject hazard = hazards50 [Random.Range (0, hazards50.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					if (score >= 25) {
						string note = "Reach score 50 to get one life up !";
						NotificationChange (note);
					}
				}

				else if (score >= 50 && score < 200) {
					GameObject hazard = hazards200 [Random.Range (0, hazards200.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					if (score >= 100) {
						string note = "Reach score 200 to get one life up !";
						NotificationChange (note);
					}
				} else if (score >= 200 && score < 400) {
					GameObject hazard = hazards500 [Random.Range (0, hazards500.Length)]; 
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);

				}

				else if (score >= 400 && score < 500) {
					i++;
					GameObject hazard = hazardDoubleBlue [Random.Range (0, hazardDoubleBlue.Length)]; 
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					string note = "Reach score 500 to get one life up !";
					NotificationChange (note);
					
				}
				
				else if (score >= 500 && score < 800) {
					GameObject hazard = hazards1000 [Random.Range (0, hazards1000.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
				}

				else if (score >= 800 && score < 1000) {
					i++;
					GameObject hazard = hazardDoubleRed [Random.Range (0, hazardDoubleRed.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					string note = "Reach score 1000 to get one life up !";
					NotificationChange (note);

				}
				
				else if (score >= 1000 && score < 1700) {
					GameObject hazard = hazards2000 [Random.Range (0, hazards2000.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
				}

				else if (score >= 1700) {
					i++;
					GameObject hazard = hazardDoubleBlack [Random.Range (0, hazardDoubleBlack.Length)];
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					string note = "Reach score 2000 to get one life up !";
					NotificationChange (note);

				}

				/*if ((score >= 400 && score < 500) || (score >= 800 && score < 1000) || (score >= 1700 && score < 2000)) {
					yield return new WaitForSeconds (spawnWait);
				}*/
				yield return new WaitForSeconds (spawnWait);

			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartButton.SetActive(true);
				//restartText.text = "Press 'R' for Restart";
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	void UpdateScore ()
	{
		if (!gameOver) {
			scoreBGButton.SetActive (true);
			livesBGButton.SetActive (true);
			scoreText.text = "Score : " + score;
			if (score >= 50 && more50 == 0) {
				more50 = 1;
				livesCount = PlayerPrefs.GetInt (livesCountKey, 3);
				livesCount++;
				PlayerPrefs.SetInt(livesCountKey, livesCount);
				PlayerPrefs.Save();
				NotificationChange (lifeUp);
				LivesCount (livesCount);
			}
			else if (score >= 200 && more200 == 0) {
				more200 = 1;
				livesCount = PlayerPrefs.GetInt (livesCountKey, 3);
				livesCount++;
				PlayerPrefs.SetInt(livesCountKey, livesCount);
				PlayerPrefs.Save();
				NotificationChange (lifeUp);
				LivesCount (livesCount);
			}
			else if (score >= 500 && more500 == 0) {
				more500 = 1;
				livesCount = PlayerPrefs.GetInt (livesCountKey, 3);
				livesCount++;
				PlayerPrefs.SetInt(livesCountKey, livesCount);
				PlayerPrefs.Save();
				NotificationChange (lifeUp);
				LivesCount (livesCount);
			}
			else if (score >= 1000 && more1000 == 0) {
				more1000 = 1;
				livesCount = PlayerPrefs.GetInt (livesCountKey, 3);
				livesCount++;
				PlayerPrefs.SetInt(livesCountKey, livesCount);
				PlayerPrefs.Save();
				NotificationChange (lifeUp);
				LivesCount (livesCount);
			}
			else if (score >= 2000 && more2000 == 0) {
				more2000 = 1;
				livesCount = PlayerPrefs.GetInt (livesCountKey, 3);
				livesCount++;
				PlayerPrefs.SetInt(livesCountKey, livesCount);
				PlayerPrefs.Save();
				NotificationChange (lifeUp);
				LivesCount (livesCount);
			}
		} 

		else 
		{
			
			
			scoreText.text = "";
			gameOverScoreText.text = "Your Score : " + score;
			if(score>highScore){
				PlayerPrefs.SetInt(highScoreKey, score);
				PlayerPrefs.Save();
			}

			highScore = PlayerPrefs.GetInt(highScoreKey,0);
			scoreText.text = "";
			highScoreText.text = "High Score : " + highScore;
			menuButton.SetActive (true);
		}
	}
	public void LivesCount(int count)
	{
		livesText.text = "Lives : " + count;
	}

	public void GameOver ()
	{
		livesCount = 3;
		more50 = 0;
		more200 = 0;
		more500 = 0;
		more1000 = 0;
		more2000 = 0;
		PlayerPrefs.SetInt(livesCountKey, livesCount);
		PlayerPrefs.Save();
		scoreBGButton.SetActive (false);
		livesBGButton.SetActive (false);
		notificationBGButton.SetActive (false);
		gameOverText.GetComponent<RectTransform> ().position = new Vector3 (360, 715, 0.0f);

		highScoreText.GetComponent<RectTransform> ().position = new Vector3 (360, 630, 0.0f);
		gameOverScoreText.GetComponent<RectTransform> ().position = new Vector3 (360, 670, 0.0f);
		restartButton.GetComponent<RectTransform> ().position = new Vector3 (360, 570, 0.0f);


		gameOverText.text = "Game Over";

		gameOver = true;

		//If our scoree is greter than highscore, set new higscore and save.
		if(score>highScore){
			PlayerPrefs.SetInt(highScoreKey, score);
			PlayerPrefs.Save();
		}
		highScore = PlayerPrefs.GetInt(highScoreKey,0);
		scoreText.text = "";
		highScoreText.text = "High Score : " + highScore;
		livesText.text = "";


	}
	public void RestartGame ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void NotificationChange(string nofification)
	{
			notificationBGButton.SetActive (true);
			notificationBGButton.GetComponentInChildren<Text> ().text = nofification;
	}

}
