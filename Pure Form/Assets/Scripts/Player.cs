using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

	public bool move;
	public int sentido = 0;
	public float velocity;
	public float fireRate = 4;
	private float counterTimeFire;
	public float frequenciaLimite = 0;
	public float frequenciaLimiteInterval = 0;
	public float intervalShot;
	public bool waitInterval = false;
	public int countShot = 0;
	public int limitShot = 0;

	private float limit_left;
	private float limit_right;
	private float playerWidth;
	private float playerHeight;
	public Camera camera;
	private Vector2 limiteNave;
	private List<Touch> directions;
	public GameObject spawnCenterShot;
	public GameObject shot;
	private bool shoot;
	// Use this for initialization
	void Start ()
	{
		countShot = limitShot;
		counterTimeFire = 1.0f / fireRate;
		directions = new List<Touch> ();
		limiteNave = transform.GetComponent<Renderer> ().bounds.size;
		playerHeight = transform.GetComponent<Renderer> ().bounds.size.y;
		limit_right = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x;
		limit_left = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).x;
		//Debug.Log(limit_left);
		Debug.Log (-camera.WorldToScreenPoint (transform.position).x);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (waitInterval && Time.time - frequenciaLimiteInterval > intervalShot) {
			frequenciaLimiteInterval = Time.time;
			waitInterval = false;
		}
		if (!waitInterval && Time.time - frequenciaLimite > counterTimeFire) {
			Debug.Log (counterTimeFire);
			frequenciaLimite = Time.time;
			Shoot ();
			if (countShot <= 0) {
				waitInterval = true;
				countShot = limitShot;
			} else {
				countShot--;
			}
		}

		//directions = new List<Touch>();
		foreach (Touch touch in Input.touches) {
           
			if (touch.phase == TouchPhase.Began) {
				directions.Add (touch);
			} else if (touch.phase == TouchPhase.Moved) {
				int a = 3;
			} else if (touch.phase == TouchPhase.Ended) {
				for (int i = 0; i < directions.Count; i++) {
					if (touch.fingerId != null) {
						if (directions [i].fingerId != null && touch.fingerId == directions [i].fingerId) {
							directions.RemoveAt (i);
						}
                            
					}
				}

               
			}
		}

		if (directions.Count > 0) {
			Debug.Log (directions.Count);
			//Debug.Log (camera.ScreenToWorldPoint( Screen.width));
			if (directions [directions.Count - 1].position.x < Screen.width / 2) {
				sentido = -1;
			} else {
				sentido = +1;
			}
			move = true;
		} else {
			move = false;
		}
		if (move) {
			if (sentido == 1) {
				if (transform.position.x < limit_right) {
					transform.position = new Vector3 (transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
				} else {
					transform.position = new Vector3 (limit_right, transform.position.y, 0);
					//transform.position = camera.ScreenToWorldPoint(new Vector3(playerWidth / 2, transform.position.y, 0));
				} 
				//transform.position = new Vector3 (transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
			} else if (sentido == -1) {
				if (transform.position.x > limit_left) {
					transform.position = new Vector3 (transform.position.x - velocity * Time.deltaTime, transform.position.y, 0);
				} else {
					transform.position = new Vector3 (limit_left, transform.position.y, 0);
					//transform.position = camera.ScreenToWorldPoint(new Vector3(playerWidth / 2, transform.position.y, 0));
				} 
			}
		}
	

		
	}

	public void Shoot ()
	{
		Instantiate (shot, spawnCenterShot.transform.position, Quaternion.identity);
	}
}
