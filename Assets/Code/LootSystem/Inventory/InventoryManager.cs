using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// Each actors has there own instance of an inventory
/// which for our specifications, need to be be pointers
/// to one single shared inventory. They maintain their
/// own invenetories incase this requirement changes
/// </summary>
public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	Inventory _inventoryCollection;

	public ActorFactory actorFactory;

	List<ActorData> _actorDatas;

	[SerializeField]
	InventoryConfig _configs;


	//In for testing
	public static InventoryManager instance;

	void Awake()
	{
		instance = this;	
		_actorDatas = new List<ActorData>();	

		if(_actor != null)
		{
			_actorDatas.Add(_actor.data);
		}

	}

	public void AddItem(Item i)
	{
		_inventoryCollection.items.Add(i);
		EquipItem(_actor.data, i);
	}

	public void UnEquipItem(ActorData a, Item i)
	{
		if(a != null && i != null)
		{
			a.UnEquip(i);
			Save();
		}
	}

	public void UnEquipItem(string actorID, Item i)
	{
		_actorDatas.FirstOrDefault(a => a.id == actorID).UnEquip(i);
	}

	public void EquipItem(ActorData a, Item i)
	{
		if(i.equipped && !string.IsNullOrEmpty(a.id))
		{
			//If was equipped by another actor, unequip it from them
			if(!String.Equals(a.id, i.ownerID, StringComparison.CurrentCultureIgnoreCase))
			{
				UnEquipItem(i.ownerID, i);
			}
		}

		a.Equip(i);
		Save();
	}

	public List<Item> GetFilteredInventory(string filter)
    {
        return _inventoryCollection.items.Where(i => i.tags.Any(t => String.Equals(t, filter, StringComparison.CurrentCultureIgnoreCase))).ToList();
    }

    public List<Item> GetFilteredInventory(List<string> filters, int count)
    {
        List<Item> filteredItems = new List<Item>();
        filters.ForEach(f => filteredItems.AddRange(GetFilteredInventory(filters, count)));
        return filteredItems;
    }

	List<Item> GetActorsItems(ActorData a)
	{
		return _inventoryCollection.items.Where(i => i.ownerID == a.id).ToList();
	}


	[ContextMenu("Save")]
	void Save()
	{
		_json = JsonUtility.ToJson(_inventoryCollection, true);
		//_actorDatas.ForEach(a => a.Save() );
	}
	
	[ContextMenu("Load")]
	void Load()
	{
		// Loads the entire inventory from json
		_inventoryCollection = JsonUtility.FromJson<Inventory>(_json);

		//For each unique ownerID on each item, store those ids
		List<string> actorIDS = _inventoryCollection.items
									.Select( i => i.ownerID)
									.Distinct()
									.ToList();

		//Look up the actorData associated with all the ids and cache them
		actorIDS.ForEach( id => _actorDatas.Add(LoadFromPlayerPrefs<ActorData>(id)));

		//For each actorData equip all of there items
		//And the actor in the scene
		_actorDatas.ForEach(a =>
		{
			GetActorsItems(a)
				.ForEach(i =>
				{
					EquipItem(a,i);
					if(i.isActor) //If the item is an actor (companion) dont spawn here
						actorFactory.CreateActor<Player>(a);
					else
						actorFactory.CreateActor<CompanionController>(a);
				});
		});	

	}	

	public Item RequestItemByOwner(string id)
	{
		return _inventoryCollection.items.FirstOrDefault( i => i.ownerID == id);
	}

	

	[ContextMenu("Clear PP")]
	void ClearPP()
	{
		PlayerPrefs.DeleteAll();
	}

	T LoadFromPlayerPrefs<T>(string id)
	{
		string json = PlayerPrefs.GetString(id);
		return JsonUtility.FromJson<T>(json);
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
		EquipItem(_actor.data, _inventoryCollection.items[_itemId]);
		Save();
	}

	[ContextMenu("UnEquip")]
	void TestUnEquip()
	{
		UnEquipItem(_actor.data, _inventoryCollection.items[_itemId]);
		Save();
	}

	
}
