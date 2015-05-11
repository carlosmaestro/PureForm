using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> itens = new List<Item>();
    
    void Start () {
        itens.Add(new Item(1, 1, "Fire", false));
        itens.Add(new Item(2, 1, "Water", false));
        itens.Add(new Item(3, 1, "Air", false));
        itens.Add(new Item(4, 1, "Earth", false));
        itens.Add(new Item(5, 1, "AirWater", true));
        itens.Add(new Item(6, 1, "AirEarth", true));
        itens.Add(new Item(7, 1, "AirFire", true));
	}
	
}
