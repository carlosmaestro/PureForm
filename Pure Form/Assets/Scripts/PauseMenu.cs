using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    

    public BlockController blockController;
    void Start()
    {
        blockController = GameObject.FindGameObjectWithTag("BlockController").GetComponent<BlockController>();
	
	}
	
	void Update () {
	
	}

    public void PauseGame()
    {
        gameObject.SetActive(true);
        //Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        
        blockController.ShowBlock();
        Invoke("CallMainMenu", 1);

    }

    public void CallMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1; 
        blockController.ShowBlock();
        Invoke("CallGameStage", 1);
        
    }

    public void CallGameStage()
    {
        Application.LoadLevel(0);
    }


}
