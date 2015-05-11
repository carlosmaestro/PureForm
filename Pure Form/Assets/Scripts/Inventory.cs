using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{

	private List<GameObject> slots = new List<GameObject> ();
	public GameObject slotModel;
	public List<Item> itens = new List<Item> ();
	public bool draggingItem = false;
	public Vector3 positionMouse;

    //grade itens
    public GameObject gradeItens;

    //grade itens
    public GameObject gradeItensEquiped;

	ItemDatabase database;
	float Swidth;
	float Sheight;

    //paineis de bloqueio 
    public GameObject painelBloqueio;
    public GameObject painelBloqueio2;

    //cores dos slots
    public Color32 selectedSlotColor;
    public Color32 selectableSlotColor;
    public Color32 notSelectableSlotColor;    

    private GameObject lastSelectSlot;
    
    //paineis de interação dieta com o player
    public GameObject dialogPanel;
    public GameObject confirmPanel;

    
    //painel cancel operation
    public GameObject closeOperationPanel;
    //texto de ajuda
    public Text helpText;

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
        foreach (Transform gameObj in gradeItens.transform)
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
        AddItem(5);
        AddItem(6);
        AddItem(7);
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
        if (lastSelectSlot != null)
        {
            lastSelectSlot.GetComponent<SlotScript>().selectedSlotImage.gameObject.SetActive(false);
        }
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
        //if (operation == OperationType.MoveItem)
        //{
        //    painelBloqueio2.SetActive(true);
        //    painelBloqueio.SetActive(false);
        //}else if(op)
        switch (operation) 
        {
            case OperationType.MoveItem:
                painelBloqueio2.SetActive(true);
                painelBloqueio.SetActive(false);
                helpText.text = "Selecione o slot para mover a gema.";
                helpText.gameObject.SetActive(true);

                break;
            case OperationType.EquipItem:
                painelBloqueio2.SetActive(false);
                painelBloqueio.SetActive(true);
                helpText.text = "Selecione o slot para equipar a gema.";
                helpText.gameObject.SetActive(true);
      
                break;
            case OperationType.CombineItem:
                painelBloqueio2.SetActive(true);
                MarkCombinableSlots(item);
                helpText.text = "Selecione o slot para combinar a gema.";
                helpText.gameObject.SetActive(true);
                break;
        

        }
        closeOperationPanel.SetActive(true);
        dialogPanel.SetActive(false);
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
                if (slot.tag == "Slot")
                {

                    ShowDialogItem(slot);
                }

            }
            else
            {
                HideDialogItem(slot);

            }
        }
        else
        {	
			if(lastSelectSlot != slot){
                switch (currentOperation)
                {
                    case OperationType.MoveItem:
                        if (objSlotScript.item != null)
                        {
                            lastSelectSlot.GetComponent<SlotScript>().item = objSlotScript.item;
                        }
                        else
                        {
                            lastSelectSlot.GetComponent<SlotScript>().item = null;
                        }
                        objSlotScript.item = currentOperationItem;
                        ResetCurrentOperation();
                        break;
                    case OperationType.EquipItem:
                        foreach (Transform gameObj in gradeItensEquiped.transform)
                        {
                            if (gameObj.tag == "EquipedSlot")
                            {
                                SlotScript slotObj = gameObj.GetComponent<SlotScript>();
                                if (currentOperationItem == slotObj.item)
                                {
                                    slotObj.item = null;

                                }
                            }
                        }
                        objSlotScript.item = currentOperationItem;
                        ResetCurrentOperation();

                        break;
                    case OperationType.CombineItem:
                        SlotScript atual_slotScript = slot.GetComponent<SlotScript>();
                        SlotScript ultimo_slotSript = lastSelectSlot.GetComponent<SlotScript>();
                        if (atual_slotScript.blockSloctImage.gameObject.activeSelf == false)
                        {
                            List<Item> listItensCombine = new List<Item>();
                            listItensCombine.Add(atual_slotScript.item);
                            listItensCombine.Add(ultimo_slotSript.item);

                            ultimo_slotSript.item = CombineItens(listItensCombine);
                            atual_slotScript.item = null;
                            ResetCurrentOperation();
                        }
                        break;
                }
                    //DesSelectSlot();
                    

			}
        }
    }

    public void ResetCurrentOperation()
    {

        currentOperation = OperationType.Null;
        currentOperationItem = new Item();
         closeOperationPanel.SetActive(false);
        helpText.gameObject.SetActive(false);
        painelBloqueio.SetActive(false);
        painelBloqueio2.SetActive(false);
        DesSelectSlot();

        foreach (Transform gameObj in gradeItens.transform)
        {
            SlotScript slotScript = gameObj.GetComponent<SlotScript>();
           
            slotScript.blockSloctImage.gameObject.SetActive(false);
           
        }
    }

    public Item CombineItens(List<Item> itens)
    {
        Item itemRetur = new Item();
        int level = itens[0].itemLevel + itens[1].itemLevel;
        if (itens[0].itemType == itens[1].itemType)
        {
            itens[0].itemLevel = level;
            itemRetur = itens[0];
        }
        else
        {

            var novalista = itens.OrderBy(c => c.itemType);
            string newName = "";
            foreach (Item item in novalista)
            {
                newName += item.itemType;
            }

            Debug.Log(newName);
            
            itemRetur = new Item(8, level, newName, true);
        }

        return itemRetur;
    }

    public void MarkCombinableSlots(Item item)
    {
        foreach (Transform gameObj in gradeItens.transform)
        {
            SlotScript slotScript = gameObj.GetComponent<SlotScript>();
            if (slotScript.item != null)
            {
                if (gameObj.tag == "Slot")
                {
                    if (!item.isCombination)
                    {
                        if (slotScript.item.isCombination)
                        {
                            slotScript.blockSloctImage.gameObject.SetActive(true);
                        }

                    }
                    else
                    {
                        if (slotScript.item.itemType != item.itemType)
                        {
                            slotScript.blockSloctImage.gameObject.SetActive(true);
                        }

                    }
                }
            }
            else
            {
                slotScript.blockSloctImage.gameObject.SetActive(true);
            }

            
        }
    }


}
