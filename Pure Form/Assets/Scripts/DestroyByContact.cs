using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{		
		public GameObject explosion;
		public GameObject playerExplosion;
		public int scoreValue;
		private GameController gameController;
		private PlayerController playerController;
	

		void Start ()
		{
				GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
				if (gameControllerObject != null) {
						gameController = gameControllerObject.GetComponent <GameController> ();
				}
				if (gameController == null) {
						Debug.Log ("Cannot find 'GameController' script");
				}
				
				GameObject playerControllerObject = GameObject.FindWithTag ("Player");
				if (playerControllerObject != null) {
						playerController = playerControllerObject.GetComponent <PlayerController> ();
				}
				if (playerController == null) {
						Debug.Log ("Cannot find 'PlayerController' script");
				}
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.tag == "Boundary") {
						return;
				}
				Instantiate (explosion, transform.position, transform.rotation);
				if (other.tag == "Player") {
						if (!playerController.UpdateLife (25)) {
								Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
								gameController.GameOver ();
								Destroy (other.gameObject);
				
						}
						gameController.AddScore (scoreValue);
			
				}
				if (other.tag == "ShotPlayer") {
						gameController.AddScore (scoreValue);
						Destroy (other.gameObject);
				}
				Destroy (gameObject);
		}
}