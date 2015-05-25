using UnityEngine;
using System.Collections;

public class ShotSergeant : MonoBehaviour
{

    public float velocidade;
    public int directionShot = 1;
    private Vector3 postionScreenPoints;
    private GameObject player;
    public float sideMoveValue;
    public Vector3 positionTarget;
    public Vector3 startPosition;
    public float damage;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x)
            {
                positionTarget = new Vector3(player.transform.position.x + sideMoveValue, transform.position.y, 0);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                positionTarget = new Vector3(player.transform.position.x - sideMoveValue, transform.position.y, 0);
            }
            //iTween.MoveTo(gameObject, iTween.Hash("position", positionTarget, "islocal", true, "time", 1));
            //postionScreenPoints = Screen.
            //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, velocidade * directionShot));
            //Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            ////direction.Normalize();
            //gameObject.GetComponent<Rigidbody2D>().velocity = direction * sideMoveValue;

            Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            //GameObject projectile = (GameObject)Instantiate(bullet, myPos, Quaternion.identity);
            gameObject.GetComponent<Rigidbody2D>().velocity = direction * sideMoveValue;
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        //if (positionTarget.x > startPosition.x)
        //{
        //    transform.position = new Vector3(transform.position.x + sideMoveValue * Time.deltaTime, transform.position.y, 0);
        //}
        //else if (positionTarget.x < startPosition.x)
        //{
        //    transform.position = new Vector3(transform.position.x - sideMoveValue * Time.deltaTime, transform.position.y, 0);
        //}
        //if (transform.position.y >= 8)
        //{
        //    Destroy(gameObject);
        //}
        Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
        if ( tmpPos.y < 0)
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
}
