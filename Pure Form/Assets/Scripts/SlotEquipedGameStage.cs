using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotEquipedGameStage : MonoBehaviour, IPointerEnterHandler ,IPointerClickHandler
{

	GameObject dialogPanel;
 
	public Item item;
	Image itemImage;
	Inventory inventory;
	public int slotNumber;
	float posX;
	float posY;
	public bool draggingItem = false;
	public Image selectedSlotImage;
	public Image blockSloctImage;
	public Text levelSlotText;

	private float maxPickingDistance = 10000;// increase if needed, depending on your scene size

	private Vector3 startPos;

	void Start ()
	{
        
		posX = transform.position.x;
		posY = transform.position.y;
		//dialogPanel = GameObject.FindGameObjectWithTag ("DialogPanel");
        
        //inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
        
        
		itemImage = gameObject.transform.GetChild (1).GetComponent<Image> ();
	}
	
	void Update ()
	{
		

	}

	public void SetItem (Item objitem)
	{
		item = objitem;
	}

	public void OnPointerClick (PointerEventData data)
	{
		float slotwidth = transform.gameObject.GetComponent<RectTransform> ().sizeDelta.x;
		float slotheigth = transform.gameObject.GetComponent<RectTransform> ().sizeDelta.y;

	
		inventory.ProcessSlotClicked (gameObject);
	}

	public void OnPointerEnter (PointerEventData data)
	{
		//Debug.Log ("mouseover on " + transform.name);
	}

  
}
