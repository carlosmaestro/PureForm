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

	//ultimo Slot selecionado
	private GameObject lastSelectSlot;
	//index do item do ultimo Slot selecionado
	private int lastSelectSlotIndexItem;
	public Item lastSelectedItem;
    
	//paineis de interação dieta com o player
	public GameObject dialogPanel;
	public GameObject confirmPanel;

    
	//painel cancel operation
	public GameObject closeOperationPanel;
	//texto de ajuda
	public Text helpText;

	//indice da pagina atual no inventario
	public int indexPageInventory = 1;

	//operação referante ao menu de dialogo
	public enum OperationType
	{ 
		MoveItem,
		CombineItem,
		EquipItem,
		Null
	}
	public OperationType currentOperation;
	public Item currentOperationItem;
	//--->

	//dicionarios de slots da grade que tem itens equipados e slots equipados
	public List<GameObject> listSlotsGridEquiped;
	public List<GameObject> listSlotsEquiped;

	public Text pageNumberText;

	//

	//list colors item equiped
	public List<Color32> listColorsItemEquiped;

	void Start ()
	{
		listSlotsGridEquiped = GetSlotsGridEquiped ();

		currentOperation = OperationType.Null;
		foreach (Transform gameObj in gradeItens.transform) {
			if (gameObj.tag == "Slot")
				slots.Add (gameObj.gameObject);
		}

		Swidth = Screen.width / 100;
		Sheight = Screen.height / 100;
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();

		SetPageInventory (indexPageInventory);
		
	}

	public List<GameObject> GetSlotsGridEquiped ()
	{
		List<GameObject> listReturn = new List<GameObject> ();
		foreach (Transform gameObj in gradeItensEquiped.transform) {
			if (gameObj.tag == "EquipedSlot") 
				listReturn.Add (gameObj.gameObject);               
		}
		return listReturn;
	}

	public int CheckItemEquiped (Item item)
	{
		int returnCheck = -1;
		for (int i =0; i < 3; i++) {
			SlotScript slotScriptObj = listSlotsGridEquiped [i].GetComponent<SlotScript> ();
			if (slotScriptObj.item == item) {
				returnCheck = i;
			}
		}
		return returnCheck;
	}

	public GameObject GetSlotGridByItem (Item item)
	{
		GameObject gameObjReturn = null;

		foreach (GameObject slot in slots) {
			if (slot.GetComponent<SlotScript> ().item == item) {
				gameObjReturn = slot;
				break;
			}
		}

		return gameObjReturn;
	}

	public void SetPageInventory (int indexPage)
	{
		indexPageInventory = indexPage;
		pageNumberText.text = indexPageInventory + "";
		//if(currentOperationItem != null && currentOperation == OperationType.CombineItem)
		//MarkCombinableSlots (currentOperationItem);
	

		for (int i = 0; i < 12; i++) {
			//if (i < indexPage * 12 && i+1 > indexPage * 12 - 12) {
			//AddItemToEmptySlot (database.itens [i]);
			int newIndex = i + (indexPage * 12 - 12);
			SlotScript objSlotScript = slots [i].GetComponent<SlotScript> ();
			if (newIndex < database.itens.Count) {
				objSlotScript.SetItem (database.itens [newIndex]);
			} else {
				objSlotScript.SetItem (null);
			}
			//}
		}
	}

	//public void AddItemToEmptySlot (Item item)
	//{
	//    SlotScript objSlotScript = slots[].GetComponent<SlotScript>();
	//    objSlotScript.SetItem(item);
	//    //foreach (GameObject slot in slots) {
			
	//    //    if (objSlotScript.item == null) {
	//    //        objSlotScript.SetItem (item);
	//    //        break;
	//    //    }
	//    //}
	//}

	public void ShowDialogItem (GameObject slot)
	{
		if (lastSelectSlot != null) {
			DesSelectSlot ();
		}
		SlotScript slotScriptObj = slot.GetComponent<SlotScript> ();
		SelectSlot (slot);

		dialogPanel.GetComponent<DialogPanel> ().SetItem (slotScriptObj.item);
		dialogPanel.SetActive (true);
	}

	public void HideDialogItem (GameObject slot)
	{
		DesSelectSlot ();
		//SlotScript slotScriptObj = slot.GetComponent<SlotScript>();
		//slot.GetComponent<Image>().color = selectedSlotColor;
		//dialogPanel.GetComponent<DialogPanel>().SetItem(slotScriptObj.item);
		dialogPanel.SetActive (false);
	}

	public void SelectSlot (GameObject slot)
	{
		lastSelectSlot = slot;
		SlotScript slotScriptObj = slot.GetComponent<SlotScript> ();
		lastSelectedItem = slotScriptObj.item;
		lastSelectSlotIndexItem = database.itens.IndexOf (slotScriptObj.item);
		
		//slot.GetComponent<Image>().color = selectedSlotColor;
		slotScriptObj.selectedSlotImage.gameObject.SetActive (true);
		slotScriptObj.selectedSlotImage.color = selectedSlotColor;
	}

	public void DesSelectSlot ()
	{
		if (lastSelectSlot != null) {
			lastSelectSlot.GetComponent<SlotScript> ().selectedSlotImage.gameObject.SetActive (false);
		}
	}

	public void MarkMovebleSlots ()
	{
		//foreach (GameObject slot in slots) {
		//    Debug.Log (" slot ----" + slot.name);
		//    if (slot.name != lastSelectSlot.name) {
		//        SlotScript objSlotScript = slot.GetComponent<SlotScript> ();
		//        objSlotScript.selectedSlotImage.gameObject.SetActive (true);
		//        objSlotScript.selectedSlotImage.color = selectableSlotColor;
		//    } else {
		//        SlotScript objSlotScript = slot.GetComponent<SlotScript> ();
		//        objSlotScript.selectedSlotImage.gameObject.SetActive (true);
		//        objSlotScript.selectedSlotImage.color = notSelectableSlotColor;
		//    }
		//}

		//Debug.Log(" slot ----" + lastSelectSlot.name);
	}

	public void SetCurrentOperation (OperationType operation, Item item)
	{
		//if (operation == OperationType.MoveItem)
		//{
		//    painelBloqueio2.SetActive(true);
		//    painelBloqueio.SetActive(false);
		//}else if(op)
		switch (operation) {
		case OperationType.MoveItem:
			painelBloqueio2.SetActive (true);
			painelBloqueio.SetActive (false);
			helpText.text = "Selecione o slot para mover a gema.";
			helpText.gameObject.SetActive (true);

			break;
		case OperationType.EquipItem:
			painelBloqueio2.SetActive (false);
			painelBloqueio.SetActive (true);
			helpText.text = "Selecione o slot para equipar a gema.";
			helpText.gameObject.SetActive (true);
      
			break;
		case OperationType.CombineItem:
			painelBloqueio2.SetActive (true);
			MarkCombinableSlots (item);
			helpText.text = "Selecione o slot para combinar a gema.";
			helpText.gameObject.SetActive (true);
			break;
        

		}
		closeOperationPanel.SetActive (true);
		dialogPanel.SetActive (false);
		currentOperationItem = item;
		currentOperation = operation;
	}

	public void ProcessSlotClicked (GameObject slot)
	{
		SlotScript objSlotScript = slot.GetComponent<SlotScript> ();
		SlotScript lastobjSlotScript = new SlotScript ();
		if (lastSelectSlot != null) {
			lastobjSlotScript = lastSelectSlot.GetComponent<SlotScript> ();
		}
		if (currentOperation == OperationType.Null) {

			if (objSlotScript.item != null) {
				if (slot.tag == "Slot") {

					ShowDialogItem (slot);
				}

			} else {
				HideDialogItem (slot);

			}
		} else {	
			if (lastSelectSlot != slot) {
				switch (currentOperation) {
				case OperationType.MoveItem:
					if (objSlotScript.item != null) {
						int firsIndex = lastSelectSlotIndexItem;
						int secondIndex = database.itens.IndexOf (objSlotScript.item);
						Item fItem = lastSelectedItem;
						Item sItem = objSlotScript.item;
						database.itens [firsIndex] = sItem;
						database.itens [secondIndex] = fItem;
						//lastobjSlotScript.item = sItem;
						//objSlotScript.item = fItem;
						SetPageInventory (indexPageInventory);
						

					} else {
						int firsIndex = lastSelectSlotIndexItem;
						int secondIndex = slots.IndexOf (slot) + indexPageInventory * 12 - 12;
						Item fItem = lastSelectedItem;
						Item sItem = null;
						database.itens [firsIndex] = sItem;
						database.itens [secondIndex] = fItem;
						//lastobjSlotScript.item = null;
						//objSlotScript.item = fItem;
						SetPageInventory (indexPageInventory);
					}

					ResetCurrentOperation ();
					break;
				case OperationType.EquipItem:
					foreach (Transform gameObj in gradeItensEquiped.transform) {
						if (gameObj.tag == "EquipedSlot") {
							SlotScript slotObj = gameObj.GetComponent<SlotScript> ();
							if (currentOperationItem == slotObj.item) {
								slotObj.item = null;

							}
						}
					}
					GameObject oldSlot = new GameObject ();
					if (objSlotScript.item != null) {
						oldSlot = GetSlotGridByItem (objSlotScript.item);
						if (oldSlot != null) {
							oldSlot.GetComponent<SlotScript> ().selectedSlotImage.gameObject.SetActive (false);
							oldSlot.GetComponent<SlotScript> ().item.orderEquip = -1;
						}

					}
					objSlotScript.item = currentOperationItem;
					objSlotScript.item.orderEquip = CheckItemEquiped (currentOperationItem);
                        //lastobjSlotScript.selectedSlotImage.color = objSlotScript.selectedSlotImage.color;
                        //lastSelectSlot = null; //objSlotScript.selectedSlotImage.gameObject.SetActive(true);
					ResetCurrentOperation ();

					break;
				case OperationType.CombineItem:
					SlotScript atual_slotScript = slot.GetComponent<SlotScript> ();
					SlotScript ultimo_slotSript = lastSelectSlot.GetComponent<SlotScript> ();
					if (atual_slotScript.blockSloctImage.gameObject.activeSelf == false) {
						List<Item> listItensCombine = new List<Item> ();
						listItensCombine.Add (atual_slotScript.item);
						listItensCombine.Add (lastSelectedItem);

						int secondIndex = database.itens.IndexOf (atual_slotScript.item);

                        ultimo_slotSript.item = null;
                        atual_slotScript.item = CombineItens(listItensCombine);

						int firsIndex = lastSelectSlotIndexItem;
						//Item fItem = lastSelectedItem;
						//Item sItem = objSlotScript.item;
                        database.itens[secondIndex] = atual_slotScript.item;
                        database.itens[firsIndex] = null;


						UpdateEquipedSlots ();
						ResetCurrentOperation ();
					}
					break;
				}
				//DesSelectSlot();
                    

			}
		}
	}

	public void ResetCurrentOperation ()
	{

		currentOperation = OperationType.Null;
		currentOperationItem = new Item ();
		closeOperationPanel.SetActive (false);
		helpText.gameObject.SetActive (false);
		painelBloqueio.SetActive (false);
		painelBloqueio2.SetActive (false);
		DesSelectSlot ();

		foreach (Transform gameObj in gradeItens.transform) {
			SlotScript slotScript = gameObj.GetComponent<SlotScript> ();

			if (gameObj.tag == "Slot")
			if (slotScript.item != null)
				slotScript.blockSloctImage.gameObject.SetActive (false);
           
		}
	}

	public Item CombineItens (List<Item> itens)
	{
		Item itemRetur = new Item ();
		int level = itens [0].itemLevel + itens [1].itemLevel;
		if (itens [0].itemType == itens [1].itemType) {
			
			itemRetur = new Item (8, level, itens [0].itemType, true);
		
		} else {

			var novalista = itens.OrderBy (c => c.itemType);
			string newName = "";
			foreach (Item item in novalista) {
				newName += item.itemType;
			}

			Debug.Log (newName);
            
			itemRetur = new Item (8, level, newName, true);
		}

		return itemRetur;
	}

	public void MarkCombinableSlots (Item item)
	{
		//foreach (Transform gameObj in gradeItens.transform) {
		//    SlotScript slotScript = gameObj.GetComponent<SlotScript> ();
		//    if (gameObj.tag == "Slot")
		//    {
		//        if (slotScript.item != null)
		//        {

		//            if (!item.isCombination)
		//            {
		//                if (slotScript.item.isCombination)
		//                {
		//                    slotScript.blockSloctImage.gameObject.SetActive(true);
		//                }
		//                else
		//                {
		//                    slotScript.blockSloctImage.gameObject.SetActive(false);
		//                }

		//            }
		//            else
		//            {
		//                if (slotScript.item.itemType != item.itemType)
		//                {
		//                    slotScript.blockSloctImage.gameObject.SetActive(true);
		//                }else
		//                {
		//                    slotScript.blockSloctImage.gameObject.SetActive(false);
		//                }

		//            }

		//        }
		//        else
		//        {
		//            slotScript.blockSloctImage.gameObject.SetActive(true);
		//        }
		//    }

            
		//}
	}

	public void UpdateEquipedSlots ()
	{
		foreach (GameObject gameObj in listSlotsGridEquiped) {
			Debug.Log (listSlotsGridEquiped.Count);
			SlotScript slotScripObj = gameObj.GetComponent<SlotScript> ();
			if (slotScripObj.item != null) {
				GameObject gObj = GetSlotGridByItem (slotScripObj.item);
				if (gObj == null) {
					slotScripObj.item = null;
				}
			}
		}
	}

	public void ChangePageInventory (string sentido)
	{
		if (sentido == "next") {
			int numberPages = 1;
			if (database.itens.Count % 12 == 0) {
				numberPages = database.itens.Count / 12;
			} else {
				numberPages = (int)(database.itens.Count / 12 + 1);
			}
			if (indexPageInventory < numberPages)
				indexPageInventory += 1;
		} else {
			if (indexPageInventory >= 2) {
				indexPageInventory -= 1;
			}
		}
		//UpdateEquipedSlots ();
		SetPageInventory (indexPageInventory);
	}

	public void UpdateGridItensToEquipedSlots ()
	{

	}


}
