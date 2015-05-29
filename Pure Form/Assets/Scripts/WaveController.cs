using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

    public int childCount;
    GameController gameController;
	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update () {
        childCount = transform.childCount;
        if (childCount <= 0)
        {
            gameController.StartNextEvent();
        }
	}
}
