using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }
}
