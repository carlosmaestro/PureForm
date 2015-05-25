using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public int valueScore;
	public AudioClip hitSound;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;
	public GameObject cam;
	public Vector3 lastPosition;
	public string namePath;
	public float timeFirstAnimation;
	public bool onAttack = false;

	public float frequenciaAttack = 0;
	public float intervalAttack;
	public float minItervalAttack = 1;
	public float maxItervalAttack = 6;

	public GameObject shot;
	public GameObject spawnShot;
	private GameController gameController;
	public Vector3[] pathFinal;
	public float timeAttack = 0;
    public bool activateAttack = false;
    
	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		SetIntervalShot ();
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		source = cam.GetComponent<AudioSource> ();
		Vector3[] path = new Vector3[iTweenPath.GetPath (namePath).Length + 1];
		path = iTweenPath.GetPath (namePath);
		path [path.Length - 1] = lastPosition;
		iTween.MoveTo (gameObject, iTween.Hash ("path", path, "time", timeFirstAnimation / 2, "easetype", iTween.EaseType.easeOutSine));
	}

	void Awake ()
	{

        

	}

	void Update ()
	{
        Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);

        if (!activateAttack == tmpPos.y < Screen.height)
        {
            frequenciaAttack = Time.time;
            SetIntervalShot();
            activateAttack = true;
        }

        if (activateAttack && Time.time - frequenciaAttack > intervalAttack)
        {
			frequenciaAttack = Time.time;
			Instantiate (shot, spawnShot.transform.position, Quaternion.identity);
			SetIntervalShot ();

		}

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "ShotPlayer") {
			gameController.AddScore (valueScore);
            
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (hitSound, vol);
			Destroy (gameObject);
			Destroy (other.gameObject);
            transform.parent.gameObject.GetComponent<Wave>().numberTotalEnemys -= 1; 
		}
	}

	public void Attack ()
	{
        
		iTween.MoveTo (gameObject, iTween.Hash ("path", pathFinal, "time", timeAttack, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "CompleteAttack"));
		onAttack = true;
	}

	public void DefAttackPath (string path, float time)
	{
		timeAttack = time;
		Vector3[] path1 = iTweenPath.GetPath (path);
		pathFinal = new Vector3[path1.Length + 1];
		float diference = 0;
		float p1 = path1 [0].x;
		float p2 = lastPosition.x;

		if (p1 < 0 && p2 < 0) {
			diference = (p1 * -1) - (p2 * -1);
			if (diference < 0)
				diference *= -1;
		} else if (p1 < 0 && p2 > 0 || p1 > 0 && p2 < 0) {
			if (p1 < 0)
				p1 *= -1;

			if (p2 < 0)
				p2 *= -1;

			diference = p1 + p2;
		} else {
			diference = p1 - p2;
			if (diference < 0)
				diference *= -1;
		}

		if (path1 [0].x > lastPosition.x) {
			diference *= -1;
		}

		for (int i = 0; i < path1.Length; i++) {
			path1 [i].x += diference;
			pathFinal [i] = path1 [i];
		}


		//path2 = iTweenPath.GetPath(namePath);
		pathFinal [pathFinal.Length - 1] = lastPosition;
	}

	private void CompleteAttack ()
	{
		onAttack = false;
		Debug.Log ("ataque completo");
	}

	public void SetIntervalShot ()
	{
		intervalAttack = Random.Range (minItervalAttack, maxItervalAttack);
	}
}
