using UnityEngine;
using System.Collections;

public class Sergeant : MonoBehaviour
{
    public float life;
    //public float _velocity;
    public float shield;

    
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
    Vector3[] path;

    public GameObject player;
    void Start()
    {
        

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SetIntervalShot();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        source = cam.GetComponent<AudioSource>();
        
        path = iTweenPath.GetPath(namePath);
        iTween.MoveTo(gameObject, iTween.Hash("position", path[0], "islocal", true, "time", 3,"easetype", iTween.EaseType.easeOutSine, "oncomplete", "StartLoopAnimation"));
        //postionScreenPoints = Screen.
    }

    public void StartLoopAnimation()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", timeFirstAnimation, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.loop));

    }

    void Awake()
    {



    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
            if (tmpPos.y < Screen.height && Time.time - frequenciaAttack > intervalAttack)
            {
                frequenciaAttack = Time.time;
                Instantiate(shot, spawnShot.transform.position, Quaternion.identity);
                SetIntervalShot();

            }
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ShotPlayer")
        {
            gameController.AddScore(valueScore);
            

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(hitSound, vol);
            ProccessDamage(other.gameObject.GetComponent<Shot>().damage);
            Destroy(other.gameObject);
        }
    }

    public void Attack()
    {

        iTween.MoveTo(gameObject, iTween.Hash("path", pathFinal, "time", timeAttack, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "CompleteAttack"));
        onAttack = true;
    }

    public void DefAttackPath(string path, float time)
    {
        timeAttack = time;
        Vector3[] path1 = iTweenPath.GetPath(path);
        pathFinal = new Vector3[path1.Length + 1];
        float diference = 0;
        float p1 = path1[0].x;
        float p2 = lastPosition.x;

        if (p1 < 0 && p2 < 0)
        {
            diference = (p1 * -1) - (p2 * -1);
            if (diference < 0)
                diference *= -1;
        }
        else if (p1 < 0 && p2 > 0 || p1 > 0 && p2 < 0)
        {
            if (p1 < 0)
                p1 *= -1;

            if (p2 < 0)
                p2 *= -1;

            diference = p1 + p2;
        }
        else
        {
            diference = p1 - p2;
            if (diference < 0)
                diference *= -1;
        }

        if (path1[0].x > lastPosition.x)
        {
            diference *= -1;
        }

        for (int i = 0; i < path1.Length; i++)
        {
            path1[i].x += diference;
            pathFinal[i] = path1[i];
        }


        //path2 = iTweenPath.GetPath(namePath);
        pathFinal[pathFinal.Length - 1] = lastPosition;
    }

    private void CompleteAttack()
    {
        onAttack = false;
        Debug.Log("ataque completo");
    }

    public void SetIntervalShot()
    {
        intervalAttack = Random.Range(minItervalAttack, maxItervalAttack);
    }

    public void ProccessDamage(float damage)
    {
        life -= damage - shield;
        if (life <= 0)
        {
            Destroy(gameObject);
            gameController.StartNextEvent();
        }

    }
}
