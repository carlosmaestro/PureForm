using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

    public float velocidade;
	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, velocidade));
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y >= 8)
        {
            Destroy(gameObject);
        }
	}
}
