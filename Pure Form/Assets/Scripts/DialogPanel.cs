using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour {

    public Item item;
    public Text nameItem;

	void Start () {
	
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
    }
}
