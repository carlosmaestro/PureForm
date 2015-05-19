using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{

	public int numberEnemys;
	public int numberColumns;
	public int numberRows;
    public string namePath;
	public Vector3 anchorPosition;

	public GameObject enemy;
	private Enemy enemyObj;
	private int countEnemy = 0;

	float frequencia = 0.25f;
	bool endWave = false;
	private List<GameObject> wave = new List<GameObject> ();
	private List<Vector3> listPositions = new List<Vector3> ();


	void Start ()
	{
        anchorPosition = transform.position;
        Vector3 position;
        position = anchorPosition;
		enemyObj = enemy.GetComponent<Enemy> ();
		for (int i = 0; i < numberRows; i++) {
            position.y--;
            position.x = anchorPosition.x;
			for (int j = 0; j < numberColumns; j++) {
                position.x++;
				//enemy.GetComponent<Enemy>().lastPosition = anchorPosition;
				//wave.Add(enemy);
                listPositions.Add(new Vector3(position.x, position.y, 0));
			}
		}
		Debug.Log ("list size" + listPositions.Count);
	}
	
	void Update ()
	{
		if (!endWave && Time.time - frequencia > 0.5f) {
			frequencia = Time.time;
			if (countEnemy < listPositions.Count) {
				enemy.GetComponent<Enemy>().lastPosition = listPositions [countEnemy];
                enemy.GetComponent<Enemy>().namePath = namePath;
				Instantiate (enemy);
				countEnemy++;
			} else {
				endWave = true;
			}
		}
	}
}
