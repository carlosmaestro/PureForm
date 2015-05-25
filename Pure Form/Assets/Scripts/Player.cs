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
    private GameController gameController;
    public GameObject cam;

    //sound
    public AudioClip hitSound;
    public AudioClip explosionSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    public GameObject[] listWeapons;


    public float lifePlayer = 100;

    private Animator animator;
	// Use this for initialization
	void Start ()
	{
        Time.timeScale = 1;
       // cam = GameObject.FindGameObjectWithTag("MainCamera");
        source = camera.GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animator = transform.GetComponent<Animator>();
		countShot = limitShot;
		counterTimeFire = 1.0f / fireRate;
		directions = new List<Touch> ();
		limiteNave = transform.GetComponent<Renderer> ().bounds.size;
		playerHeight = transform.GetComponent<Renderer> ().bounds.size.y;
		limit_right = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)).x;
		limit_left = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).x;
		//Debug.Log(limit_left);
        ////Debug.Log (-camera.WorldToScreenPoint (transform.position).x);

	}

    public void ChangeWeapons(string type)
    {
        if(type == "Fire" ||type == "Fire_Air" ||type == "Fire_Water" ||type == "Fire_Earth")
        {
            shot = listWeapons[0];
        }
        else if(type == "Water" ||type == "Air_Water" ||type == "Water_Earth")
        {
            shot = listWeapons[1];
        }
        else if(type == "Air" ||type == "Air_Earth")
        {
            shot = listWeapons[2];
        }
        else if(type == "Earth" )
        {
            shot = listWeapons[3];
        }
        else
        {
            shot = listWeapons[4];
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (waitInterval && Time.time - frequenciaLimiteInterval > intervalShot) {
			frequenciaLimiteInterval = Time.time;
			waitInterval = false;
		}
		if (!waitInterval && Time.time - frequenciaLimite > counterTimeFire) {
            //Debug.Log (counterTimeFire);
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
                animator.SetBool("moveright", true);
                animator.SetBool("moveleft", false);
				if (transform.position.x < limit_right) {
                    
					transform.position = new Vector3 (transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
				} else {
                    
					transform.position = new Vector3 (limit_right, transform.position.y, 0);
					//transform.position = camera.ScreenToWorldPoint(new Vector3(playerWidth / 2, transform.position.y, 0));
				} 
				//transform.position = new Vector3 (transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
            }
            else if (sentido == -1)
            {
                animator.SetBool("moveright", false);
                animator.SetBool("moveleft", true);
                if (transform.position.x > limit_left)
                {
                    transform.position = new Vector3(transform.position.x - velocity * Time.deltaTime, transform.position.y, 0);
                    
                }
                else
                {
                    transform.position = new Vector3(limit_left, transform.position.y, 0);
                    //transform.position = camera.ScreenToWorldPoint(new Vector3(playerWidth / 2, transform.position.y, 0));
                }
            }

        }
        else
        {
            animator.SetBool("moveright", false);
            animator.SetBool("moveleft", false);
        }
	

		
	}

	public void Shoot ()
	{
		Instantiate (shot, spawnCenterShot.transform.position, Quaternion.identity);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ShotEnemy")
        {
            lifePlayer -= other.GetComponent<ShotEnemy>().damage;
            gameController.ResetPointsMultiplier();
            gameController.SetLifePlayer(lifePlayer);

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(hitSound, vol);
            //Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "ShotSergeant")
        {
            lifePlayer -= other.GetComponent<ShotSergeant>().damage;
            gameController.ResetPointsMultiplier();
            gameController.SetLifePlayer(lifePlayer);

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(hitSound, vol);
            //Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ShotBoss")
        {
            lifePlayer -= other.GetComponent<ShotSergeant>().damage;
            gameController.ResetPointsMultiplier();
            gameController.SetLifePlayer(lifePlayer);

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(hitSound, vol);
            //Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (lifePlayer <= 0)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(explosionSound, vol);
            Destroy(gameObject);
            
        }
    }
}
