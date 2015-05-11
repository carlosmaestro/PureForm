using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour {

    public Item item;
    public Text nameItem;
    Inventory inventory;

	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetItem(Item pitem)
    {
        item = pitem;
        nameItem.text = item.itemType;
    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
        inventory.DesSelectSlot();
    }

    public void SetMoveOperation()
    {
        inventory.SetCurrentOperation(Inventory.OperationType.MoveItem, item);
    }


}
