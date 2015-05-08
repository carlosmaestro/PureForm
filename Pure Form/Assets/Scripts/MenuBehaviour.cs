using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour {

    public GameObject menuContainer;
    public float velocityMenu;
    public GameObject[] menulist;

    public Image backgroundMenu;

    private float sWidth;
    private float sHeight;
    private Vector3 positionMenuLeft;
    private Vector3 positionMenuRight;
    private Vector3 positionMenuCenter;
    public string[] listPositionMenu;
    public Color32[] backgroundMenuColor;
	// Use this for initialization
	void Start () {
        sWidth = Screen.width;
        sHeight = Screen.height;

        positionMenuRight = new Vector3(menulist[0].transform.position.x + sWidth, menulist[0].transform.position.y, 0);
        positionMenuLeft = new Vector3(menulist[0].transform.position.x - sWidth, menulist[0].transform.position.y, 0);
        positionMenuCenter = new Vector3(0 + sWidth / 2, 0 + sHeight / 2, 0);

        velocityMenu = 1;
        //foreach(GameObject menu in menulist){
            
        //}
        //menulist[0].transform.position = new Vector3(sWidth/2, 0, 0);
        for (int i = 1; i <= 4; i++)
        {
            menulist[i].transform.position = GetSideMenu(listPositionMenu[i]);
        }
       
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public Vector3 GetSideMenu(string side) {
        Vector3 sidePosition = positionMenuCenter;
        if (side == "left") {
            sidePosition = positionMenuLeft;
        }
        else if (side == "right")
        {
            sidePosition = positionMenuRight;
        }
        return sidePosition;
    }

    public void ShowMenu(int indexMenu){
        backgroundMenu.color = backgroundMenuColor[indexMenu];
        FadeOut(backgroundMenu.GetComponent<CanvasGroup>());
        FadeIn(backgroundMenu.GetComponent<CanvasGroup>());
        

        FadeOut(menulist[0].GetComponent<CanvasGroup>());
        FadeIn(menulist[indexMenu].GetComponent<CanvasGroup>());
        iTween.MoveTo(menulist[indexMenu], positionMenuCenter, velocityMenu);
    }
    public void HideMenu(int indexMenu)
    {
        backgroundMenu.color = backgroundMenuColor[0];

        FadeOut(backgroundMenu.GetComponent<CanvasGroup>());
        FadeIn(backgroundMenu.GetComponent<CanvasGroup>());

        FadeIn(menulist[0].GetComponent<CanvasGroup>());
        FadeOut(menulist[indexMenu].GetComponent<CanvasGroup>());
        iTween.MoveTo(menulist[indexMenu], GetSideMenu(listPositionMenu[indexMenu]), velocityMenu);
    }

    //metodo para trocar de tela no menu principal
    public void SlideMenu(string directionMenu)
    {
        
        //moveMenu = true;

        if (directionMenu == "left")
        {
            FadeOut(menulist[0].GetComponent<CanvasGroup>());
            FadeIn(menulist[1].GetComponent<CanvasGroup>());
            iTween.MoveTo(menulist[1], new Vector3(0 +sWidth/2, 0 + sHeight/2, 0), velocityMenu);
            
        }
        else if (directionMenu == "right")
        {
            iTween.MoveTo(menuContainer, new Vector3(-275, 0, 0), velocityMenu);
        }
        else if (directionMenu == "top")
        {
            iTween.MoveTo(menuContainer, new Vector3(-0, -172, 0), velocityMenu);
        }
        else if (directionMenu == "botton")
        {
            iTween.MoveTo(menuContainer, new Vector3(-0, 172, 0), velocityMenu);
        }
        else if (directionMenu == "center")
        {
            iTween.MoveTo(menuContainer, new Vector3(-0, 0, 0), velocityMenu);
        }
    }

    private IEnumerator _FadeOut(CanvasGroup canvasGroup)
    {
        //CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime *3;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }

    public void FadeOut(CanvasGroup canvasGroup){
        StartCoroutine(_FadeOut(canvasGroup));    
    }

    private IEnumerator _FadeIn(CanvasGroup canvasGroup)
    {
        //CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha <= 1)
        {
            canvasGroup.alpha += Time.deltaTime * 3;
            yield return null;
        }
        canvasGroup.interactable = true;
        yield return null;
    }

    public void FadeIn(CanvasGroup canvasGroup)
    {
        StartCoroutine(_FadeIn(canvasGroup));
    }
}
