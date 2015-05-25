using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

    public float velocidade;
    public int directionShot = 1;
    private Vector3 postionScreenPoints;
    public float damage;
	// Use this for initialization
    
	void Start () {
        //postionScreenPoints = Screen.
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, velocidade * directionShot));
	}
	
	// Update is called once per frame
	void Update () {

        //if (transform.position.y >= 8)
        //{
        //    Destroy(gameObject);
        //}
        Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
        if (tmpPos.y > Screen.height || tmpPos.y < 0)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
	}
}
