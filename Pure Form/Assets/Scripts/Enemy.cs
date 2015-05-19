using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public AudioClip hitSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    public GameObject cam;
    public Vector3 lastPosition;
    public string namePath;

    
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        source = cam.GetComponent<AudioSource>();
        Vector3[] path = new Vector3[iTweenPath.GetPath(namePath).Length + 1];
        path = iTweenPath.GetPath(namePath);
        path[path.Length -1] = lastPosition;
        iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 10, "easetype", iTween.EaseType.easeOutSine));
	}

    void Awake()
    {

        

    }

	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(hitSound, vol);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
