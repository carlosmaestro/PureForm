using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public float scorePlayer = 0;
    public int lifePlayer = 100;
    public float pointsMultiplier = 1.0f;

    public Scrollbar healthBar;
    //public Image healthBar;
    public Text pontuationText;
    public Text pointsMultiplierText;
	
    private RectTransform rectHealthbar;
    
    void Start () {
       //rectHealthbar = healthBar.GetComponent<RectTransform>();
	}
	
	void Update () {
        //rectHealthbar.right = new Vector3(lifePlayer, rectHealthbar.sizeDelta.y,0);
        //healthBar.size = lifePlayer / 100f;
        //pointsMultiplierText.text = pointsMultiplier.ToString("0.0") + "X";
        //pontuationText.text = scorePlayer.ToString("0000000");
	}

    public void AddScore(int points)
    {
        AddPointsMultiplier();
        scorePlayer += points * pointsMultiplier;
        pontuationText.text = scorePlayer.ToString("0000000");
    }

    public void SetLifePlayer(int life)
    {
        lifePlayer = life;
        healthBar.size = lifePlayer / 100f;
    }

    public void AddPointsMultiplier()
    {
        pointsMultiplier += 0.1f;
        pointsMultiplierText.text = pointsMultiplier.ToString("0.0") + "X";
    }

    public void ResetPointsMultiplier()
    {
        pointsMultiplier = 1;
        pointsMultiplierText.text = pointsMultiplier.ToString("0.0") + "X";
    }
}
