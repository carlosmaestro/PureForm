using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> itens = new List<Item>();
    
    void Start () {
        itens.Add(null);
        itens.Add(new Item(2, 1, "Water", false));
        itens.Add(new Item(3, 1, "Air", false));
        itens.Add(new Item(4, 1, "Earth", false));
        itens.Add(new Item(5, 1, "AirWater", true));
        itens.Add(new Item(6, 1, "AirEarth", true));
        itens.Add(new Item(7, 1, "AirFire", true));
        itens.Add(new Item(8, 1, "Fire", false));
        itens.Add(new Item(9, 1, "Water", false));
        itens.Add(new Item(10, 1, "Air", false));
        itens.Add(new Item(11, 1, "Earth", false));
        itens.Add(new Item(12, 1, "AirWater", true));
        itens.Add(new Item(13, 1, "AirEarth", true));
        itens.Add(new Item(14, 1, "AirFire", true));
        itens.Add(new Item(15, 1, "Fire", false));
        itens.Add(new Item(16, 1, "Water", false));
        itens.Add(new Item(17, 1, "Air", false));
        itens.Add(new Item(18, 1, "Earth", false));
        itens.Add(new Item(19, 1, "AirWater", true));
        itens.Add(new Item(20, 1, "AirEarth", true));
        itens.Add(new Item(21, 1, "AirFire", true));
        itens.Add(new Item(22, 1, "Fire", false));
        itens.Add(new Item(23, 1, "Water", false));
        itens.Add(new Item(24, 1, "Air", false));
        itens.Add(new Item(25, 1, "Earth", false));
        itens.Add(new Item(26, 1, "AirWater", true));
        itens.Add(new Item(27, 1, "AirEarth", true));
        itens.Add(new Item(28, 1, "AirFire", true));     

	}
	
}
