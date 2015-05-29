using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{

	public int numberEnemys;
	public int numberColumns;
	public int numberRows;
    public string namePath;

    public int numberTotalEnemys = -1;
	public Vector3 anchorPosition;

	public GameObject enemy;
	private Enemy enemyObj;
	private int countEnemy = 0;

	float frequencia = 0.25f;
	bool endWave = false;
    public List<GameObject> wave = new List<GameObject>();
    public List<Vector3> listPositions = new List<Vector3>();
    public List<GameObject> listEnemys = new List<GameObject>();

    public float frequenciaAttack = 0;
    public float delayFirstAttack = 11;
    public float delayNormaltAttack = 3;
    public float intervalAttack = 0;

    public GameController gameController;
    

	void Start ()
	{
        numberTotalEnemys = numberRows * numberColumns;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        intervalAttack = delayFirstAttack;
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
        //Debug.Log ("list size" + listPositions.Count);
	}
	
	void Update ()
	{
        //Debug.Log(transform.childCount);
        
        if(numberTotalEnemys == 0)
            {
                //gameController.StartNextEvent();

                Destroy(gameObject);
            }
        if (Time.time - frequenciaAttack > intervalAttack)
        {
            frequenciaAttack = Time.time;
            intervalAttack = delayNormaltAttack;
            listEnemys = new List<GameObject>();
            foreach (Transform gameObj in transform)
            {
                listEnemys.Add(gameObj.gameObject);
            }
            if (listEnemys.Count > 0)
            {
                ChoseEnemyAttack();
                //Debug.Log("attack");
            }
            
           
        }

		if (!endWave && Time.time - frequencia > 0.5f) {
			frequencia = Time.time;
			if (countEnemy < listPositions.Count) {
				enemy.GetComponent<Enemy>().lastPosition = listPositions [countEnemy];
                enemy.GetComponent<Enemy>().namePath = namePath;
                enemy.GetComponent<Enemy>().timeFirstAnimation = delayFirstAttack;
                enemy.GetComponent<Enemy>().DefAttackPath("Path_3", 8);
                GameObject gameObj = Instantiate(enemy) as GameObject;
                gameObj.transform.parent = transform;
                //listEnemys.Add(gameObj);
                //Debug.Log(listEnemys.Count);
				countEnemy++;
			} else {
				endWave = true;
			}
		}
        
	}

    public void ChoseEnemyAttack()
    {
        while (true)
        {
            Enemy enemyObj = listEnemys[Random.Range(0, listEnemys.Count - 1)].GetComponent<Enemy>();
            if (!enemyObj.onAttack)
            {
                enemyObj.Attack();
                break;
            }
        }
    }
}
