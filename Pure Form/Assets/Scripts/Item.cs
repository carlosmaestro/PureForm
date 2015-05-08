using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item {

    public int itemID;
    public int itemLevel;
    public Sprite itemIcon;
    public GameObject itemModel;
    public string itemType;

    public  Dictionary<string, string> dictTypes = new Dictionary<string, string>(); 

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

    public Item(int id, int level, string type)
    {
        itemID = id;
        itemLevel = level;
        itemIcon = Resources.Load<Sprite>(type);
        itemType = type;
    }

    public Item()
    {
    }
	
}
