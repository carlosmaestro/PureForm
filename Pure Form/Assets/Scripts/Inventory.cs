using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

	private List<GameObject> slots = new List<GameObject> ();
	public GameObject slotModel;
	public List<Item> itens = new List<Item> ();
	public bool draggingItem = false;
	public Vector3 positionMouse;
    public GameObject gradeItens;
	ItemDatabase database;
	float Swidth;
	float Sheight;

    //cores dos slots
    public Color32 selectedSlotColor;
    public Color32 selectableSlotColor;
    public Color32 notSelectableSlotColor;    

    private GameObject lastSelectSlot;
    
    //paineis de interação dieta com o player
    public GameObject dialogPanel;
    public GameObject confirmPanel;

    //operação referante ao menu de dialogo
    public enum OperationType { 
        MoveItem,
        CombineItem,
        EquipItem,
        Null
    }
    public OperationType currentOperation;
    public Item currentOperationItem;
    //--->

	void Start ()
	{
        currentOperation = OperationType.Null;
        foreach (Transform gameObj in transform)
        {
            if (gameObj.tag == "Slot")
                slots.Add(gameObj.gameObject);
        }

		Swidth = Screen.width / 100;
		Sheight = Screen.height / 100;
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();

		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
	}

	void Update ()
	{
		
	}

	public void AddItem (int index)
	{
		foreach (Item item in database.itens) {
			if (item.itemID == index) {
				AddItemToEmptySlot (item);
			}
		}
	}

	public void AddItemToEmptySlot (Item item)
	{
		foreach (GameObject slot in slots) {
			SlotScript objSlotScript = slot.GetComponent<SlotScript> ();
			if (objSlotScript.item == null) {
				objSlotScript.SetItem (item);
				break;
			}
		}
	}

    public void ShowDialogItem(GameObject slot)
    {
        if (lastSelectSlot != null)
        {
            DesSelectSlot();
        }
        SlotScript slotScriptObj = slot.GetComponent<SlotScript>();
        SelectSlot(slot);

        dialogPanel.GetComponent<DialogPanel>().SetItem(slotScriptObj.item);
        dialogPanel.SetActive(true);
    }

    public void HideDialogItem(GameObject slot)
    {
        DesSelectSlot();
        //SlotScript slotScriptObj = slot.GetComponent<SlotScript>();
        //slot.GetComponent<Image>().color = selectedSlotColor;
        //dialogPanel.GetComponent<DialogPanel>().SetItem(slotScriptObj.item);
        dialogPanel.SetActive(false);
    }

    public void SelectSlot(GameObject slot)
    {
        lastSelectSlot = slot;
        SlotScript slotScriptObj = slot.GetComponent<SlotScript>();
        //slot.GetComponent<Image>().color = selectedSlotColor;
        slotScriptObj.selectedSlotImage.gameObject.SetActive(true);
        slotScriptObj.selectedSlotImage.color = selectedSlotColor;
    }

    public void DesSelectSlot()
    {
        lastSelectSlot.GetComponent<SlotScript>().selectedSlotImage.gameObject.SetActive(false);
        
    }

    public void MarkMovebleSlots()
    {
        foreach (GameObject slot in slots)
        {
            Debug.Log(" slot ----" + slot.name);
            if (slot.name != lastSelectSlot.name)
            {
                SlotScript objSlotScript = slot.GetComponent<SlotScript>();
                objSlotScript.selectedSlotImage.gameObject.SetActive(true);
                objSlotScript.selectedSlotImage.color = selectableSlotColor;
            }
            else
            {
                SlotScript objSlotScript = slot.GetComponent<SlotScript>();
                objSlotScript.selectedSlotImage.gameObject.SetActive(true);
                objSlotScript.selectedSlotImage.color = notSelectableSlotColor;
            }
        }

        //Debug.Log(" slot ----" + lastSelectSlot.name);
    }

    public void SetCurrentOperation(OperationType operation, Item item) 
    {
        currentOperationItem = item;
        currentOperation = operation;
    }

    public void ProcessSlotClicked(GameObject slot)
    {
        SlotScript objSlotScript = slot.GetComponent<SlotScript>();
        if (currentOperation == OperationType.Null)
        {
            if (objSlotScript.item != null)
            {

                ShowDialogItem(slot);

            }
            else
            {
                HideDialogItem(slot);

            }
        }
        else
        {	
			if(lastSelectSlot != slot){
	            if (currentOperation == OperationType.MoveItem)
	            {
	                objSlotScript.item = currentOperationItem;
	                ResetCurrentOperation();
	                lastSelectSlot.GetComponent<SlotScript>().item = null;
	                DesSelectSlot();
	            }
			}
        }
    }

    public void ResetCurrentOperation()
    {
        currentOperation = OperationType.Null;
        currentOperationItem = new Item();
    }
    
}
