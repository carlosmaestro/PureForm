using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> itens = new List<Item>();
    
    void Start () {
        itens.Add(new Item(1, 1, "Fire"));
        itens.Add(new Item(2, 1, "Water"));
        itens.Add(new Item(3, 1, "Air"));
        itens.Add(new Item(4, 1, "Earth"));
	}
	
}
