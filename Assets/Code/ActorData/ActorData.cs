using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class ActorData
{

	public string name;
	public Texture2D iconSprite;

	//These should NOT be serialized
	private Inventory _inventoryCollection;
	//They are in for testing 
	//So i can easily see in the inspector
	private List<StatData> _modifiers;

	public StatData baseStats;
	public string id;
	public GameObject gameObject;

	public ActorData()
	{
		_inventoryCollection = new Inventory();
		_modifiers = new List<StatData>();
	}

	public void Equip(Item i)
	{
		i.ownerID = id;
		i.equipped = true;
		_modifiers.Add(i.stats);
		_inventoryCollection.items.Add(i);
		Save();
	}

	public void UnEquip(Item i)
	{
		i.ownerID = null;
		i.equipped = false;
		_modifiers.Remove(i.stats);
		_inventoryCollection.items.Remove(i);
		Save();
	}

	public void Save()
	{
		//string json = JsonUtility.ToJson(this, true);
		PlayerPrefs.SetString(id, id);
	}

	public void SpawnItemGameObject()
	{
		_inventoryCollection.items.ForEach( i => 
		{
			if(!i.isActor)
				i.InstantiateGameObject();
		});
	}
}
