using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item {

    public int itemID;
    public int itemLevel;
    public Sprite itemIcon;
    public GameObject itemModel;
    public string itemType;
    public bool isCombination;
    public int orderEquip = -1;

    //public enum ItemType
    //{ 
    //    Fire,
    //    Air,
    //    Water,
    //    Earth,
    //    Fire_Air,
    //    Fire_Water,
    //    Fire_Earth,
    //    Air_Water,
    //    Air_Earth,
    //    Water_Earth
    //}

    public Item(int id, int level, string type, bool iscombination, int order)
    {
        itemID = id;
        itemLevel = level;
        itemIcon = Resources.Load<Sprite>(type);
        itemType = type;
        isCombination = iscombination;
        orderEquip = order;
    }

    public Item()
    {
    }
	
}
