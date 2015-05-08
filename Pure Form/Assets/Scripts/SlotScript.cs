﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerEnterHandler ,IPointerClickHandler
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

	private float maxPickingDistance = 10000;// increase if needed, depending on your scene size

	private Vector3 startPos;

	void Start ()
	{
        
		posX = transform.position.x;
		posY = transform.position.y;
		//dialogPanel = GameObject.FindGameObjectWithTag ("DialogPanel");
        
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
        
        
		itemImage = gameObject.transform.GetChild (1).GetComponent<Image> ();
	}
	
	void Update ()
	{
        //draggingItem = inventory.draggingItem;

		if (item != null) {
			itemImage.sprite = item.itemIcon;
			itemImage.enabled = true;

		} else {
			//itemImage.enabled = false;
		}

        foreach (Touch touch in Input.touches)
        {
            //Create horizontal plane
            Plane horPlane = new Plane(Vector3.up, Vector3.zero);

            //Gets the ray at position where the screen is touched
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, maxPickingDistance))
                {
                    //pickedObject = hit.transform;
                    startPos = touch.position;
                    inventory.draggingItem = true;
                    inventory.positionMouse = startPos;
                }
                else
                {
                    //pickedObject = null;
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //if (pickedObject != null)
                //{
                //    float distance1 = 0f;
                //    if (horPlane.Raycast(ray, out distance1))
                //    {
                //        //pickedObject.transform.position = ray.GetPoint(distance1);
                //        inventory.positionMouse = ray.GetPoint(distance1);
                //    }
                //}
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //pickedObject = null;
                inventory.draggingItem = false;
            }
        }


	}

	public void SetItem (Item objitem)
	{
		item = objitem;
	}

	public void OnPointerClick (PointerEventData data)
	{
        float slotwidth = transform.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        float slotheigth = transform.gameObject.GetComponent<RectTransform>().sizeDelta.y;

		//Debug.Log(transform.name);
		if (item != null ) {
			//dialogPanel.transform.position = new Vector3 (posX + slotwidth, posY - slotheigth);
            //dialogPanel.GetComponent<DialogPanel> ().SetItem (item);
            //dialogPanel.SetActive (true);
            //transform.GetComponent<Image>().color = 
            inventory.ShowDialogItem(gameObject);

		} else {
            inventory.HideDialogItem(gameObject);

		}
        
	}

	public void OnPointerEnter (PointerEventData data)
	{
		 Debug.Log("mouseover on " + transform.name);
	}

  
}