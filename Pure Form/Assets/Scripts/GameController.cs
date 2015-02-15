using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
		public Vector3 spawnValues;
		public GameObject hazard;
		public int hazardCount;
		public float spawnWait;
		public float startWait;
		public float wavetWait;
		
//		public GUIText scoreText;
//		public Canvas scoreText;
		public Text scoreText;
		public Text gameOverText;
		public Text restartText;
		private int score;
		private bool gameOver;
		private bool restart;
	
		void Start ()
		{		
				gameOver = false;
				restart = false;
				score = 0;
				restartText.text = "";
				gameOverText.text = "";
				StartCoroutine (SpawnWaves ());
				UpdateScore ();
		}

		void Update ()
		{
				if (restart) {
						if (Input.GetKeyDown (KeyCode.R)) {
								Application.LoadLevel (Application.loadedLevel);
						}
				}
		}

		IEnumerator SpawnWaves ()
		{		
				yield return new WaitForSeconds (startWait);
				while (true) {
						for (int i = 0; i < hazardCount; i++) {
								Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
								Quaternion spawnRotation = Quaternion.identity;
								Instantiate (hazard, spawnPosition, spawnRotation);
								yield return new WaitForSeconds (spawnWait);
						}
						yield return new WaitForSeconds (wavetWait);

						if (gameOver) {
								restartText.text = "Press 'R' for Restart";
								restart = true;
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
				scoreText.text = "Score: " + score;
		}

		public  void GameOver ()
		{
				gameOverText.text = "Game Over!";
				gameOver = true;
		}
}
