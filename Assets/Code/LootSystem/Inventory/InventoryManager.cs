using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	Inventory _inventoryCollection;


	//In for testing
	public static InventoryManager instance;

	void Awake()
	{
		instance = this;		
	}

	public void AddItem(Item i)
	{
		_inventoryCollection.items.Add(i);
		EquipItem(_actor, i);
	}

	public void UnEquipItem(Actor a, Item i)
	{
		a.UnEquip(i);
		Save();
	}

	public void EquipItem(Actor a, Item i)
	{
		if(i.equipped && i.owner != null)
		{
			i.owner.UnEquip(i);
		}

		a.Equip(i);
		Save();
	}

	[ContextMenu("Save")]
	void Save()
	{
		_json = JsonUtility.ToJson(_inventoryCollection, true);
	}
	
	[ContextMenu("Load")]
	void Load()
	{
		_inventoryCollection = JsonUtility.FromJson<Inventory>(_json);

		_inventoryCollection.items.ForEach( i => EquipItem(i.owner, i));
		
	}

	[Space(25)]
	[Header("Test Properties")]
	[SerializeField]
	Actor _actor;
	[SerializeField]
	int _itemId;

	[SerializeField][TextArea(5,50)]
	string _json;

	[ContextMenu("Equip")]
	void TestEquip()
	{
		EquipItem(_actor, _inventoryCollection.items[_itemId]);
		Save();
	}

	[ContextMenu("UnEquip")]
	void TestUnEquip()
	{
		UnEquipItem(_actor, _inventoryCollection.items[_itemId]);
		Save();
	}

	
}
