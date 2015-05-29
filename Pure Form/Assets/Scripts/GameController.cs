using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    //fase
    public GameObject[] eventsOnPhase;
    public int countEvent = 0;

    public float scorePlayer = 0;
    public float lifePlayer = 100;
    public float pointsMultiplier = 1.0f;

    public Scrollbar healthBar;
    //public Image healthBar;
    public Text pontuationText;
    public Text pointsMultiplierText;

    public GameObject gameOverPanel;
	
    private RectTransform rectHealthbar;

    public int gemEquiped1 = 0;
    public int gemEquiped2 = 0;
    public int gemEquiped3 = 0;

    public Item itemEquiped1 ;
    public Item itemEquiped2 ;
    public Item itemEquiped3 ;

    public GameObject btnArma1;
    public GameObject btnArma2;
    public GameObject btnArma3;

    public GameObject btnArmaSelecionada;

    public GameObject panelWeapons;

    public Player player;
    public Connection connection;

    public BlockController blockController;

    void Start()
    {
        blockController = GameObject.FindGameObjectWithTag("BlockController").GetComponent<BlockController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        connection = GameObject.FindGameObjectWithTag("Connection").GetComponent<Connection>();
        Instantiate(eventsOnPhase[countEvent]);
        gemEquiped1 = PlayerPrefs.GetInt("idGemaEquipada1");
        gemEquiped2 = PlayerPrefs.GetInt("idGemaEquipada2");
        gemEquiped3 = PlayerPrefs.GetInt("idGemaEquipada3");

        itemEquiped1 = connection.GetItemById(gemEquiped1);
        btnArma1.GetComponent<Image>().sprite = itemEquiped1.itemIcon;
        btnArmaSelecionada.GetComponent<Image>().sprite = itemEquiped1.itemIcon;
        itemEquiped2 = connection.GetItemById(gemEquiped2);
        btnArma2.GetComponent<Image>().sprite = itemEquiped2.itemIcon;
        itemEquiped3 = connection.GetItemById(gemEquiped3);
        btnArma3.GetComponent<Image>().sprite = itemEquiped3.itemIcon;

       //rectHealthbar = healthBar.GetComponent<RectTransform>();
	}

    public void TooglePanelWeapons()
    {
        if (panelWeapons.activeSelf)
        {
            panelWeapons.SetActive(false);
        }
        else
        {
            panelWeapons.SetActive(true);
        }
    }

    public void ShowPanelWeapons()
    {
        panelWeapons.SetActive(true);
    }

    public void HidePanelWeapons()
    {
        panelWeapons.SetActive(false);
    }

    public void SelectWeapon(int index)
    {
        switch (index)
        {
            case 1:
                btnArmaSelecionada.GetComponent<Image>().sprite = itemEquiped1.itemIcon;
                player.ChangeWeapons(itemEquiped1.itemType);
                break;
            case 2:
                btnArmaSelecionada.GetComponent<Image>().sprite = itemEquiped2.itemIcon;
                player.ChangeWeapons(itemEquiped2.itemType);
                break;
            case 3:
                btnArmaSelecionada.GetComponent<Image>().sprite = itemEquiped3.itemIcon;
                player.ChangeWeapons(itemEquiped3.itemType);
                break;
        }

        TooglePanelWeapons();
    }

    public void StartNextEvent()
    {
        countEvent++;
        if (countEvent <= eventsOnPhase.Length)
        {
            blockController.ShowBlock();
            Invoke("CallInventory", 1);

            
        }
        else
        {
            Instantiate(eventsOnPhase[countEvent]);
        }
    }

    public void CallInventory()
    {
        Application.LoadLevel("Inventario");
    }
	
	void Update () {
        if (lifePlayer <= 0)
        {
            //Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
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

    public void SetLifePlayer(float life)
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
