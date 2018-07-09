using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class ActorData
{

	//These should NOT be serialized
	public Inventory inventoryCollection;
	//They are in for testing 
	//So i can easily see in the inspector
	public List<StatData> modifiers;

	public StatData baseStats;
	public string id;
	public GameObject gameObject;


	public ActorData()
	{
		inventoryCollection = new Inventory();
		modifiers = new List<StatData>();
	}

	public void Equip(Item i)
	{
		i.ownerID = id;
		i.equipped = true;
		modifiers.Add(i.stats);
		inventoryCollection.items.Add(i);
		Save();
	}

	public void UnEquip(Item i)
	{
		i.ownerID = null;
		i.equipped = false;
		modifiers.Remove(i.stats);
		inventoryCollection.items.Remove(i);
		Save();
	}

	public void Save()
	{
		//string json = JsonUtility.ToJson(this, true);
		PlayerPrefs.SetString(id, id);
	}

	public void SpawnItemGameObject()
	{
		inventoryCollection.items.ForEach( i => i.InstantiateGameObject());
	}
}
