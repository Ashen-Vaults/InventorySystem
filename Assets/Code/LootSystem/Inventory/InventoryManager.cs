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


	//In for testing
	public static InventoryManager instance;

	void Awake()
	{
		instance = this;	
		_actorDatas = new List<ActorData>();	
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

	void SpawnOwner(Actor a)
	{
		Instantiate(a.gameObject);
	}

	public List<Item> GetFilteredInventory(string filter)
    {
        return _inventoryCollection.items.Where(i => i.tags.Any(t => String.Equals(t, filter, StringComparison.CurrentCultureIgnoreCase))).ToList();
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
		_inventoryCollection = JsonUtility.FromJson<Inventory>(_json);

	

		//Create _actorDatas based off the inventory owners ids
		//for each unique ownerID in all inventory, create
		//new actorDatas and add the 

		List<string> actorIDS = _inventoryCollection.items.Select( i => i.ownerID).Distinct().ToList();

		actorIDS.ForEach( a => _actorDatas.Add( new ActorData() { id = a}));

		_actorDatas.ForEach(a =>
		{
			GetActorsItems(a).ForEach(i => EquipItem(a,i));
			actorFactory.CreateActor(a);
		});	
		}


		//Get all items for an actor
		///_inventoryCollection.items.Where(i => _actorDatas.Any(a => String.Equals(a.id, i.ownerID, StringComparison.CurrentCultureIgnoreCase))).ToList();

		//Equip all items to each actor
		//_inventoryCollection.items.ForEach( i => EquipItem(null, i));    

		//Equip Items
		// _actorDatas.ForEach(a => 
			// EquipItem()

		// Need to only create actor if doesn't exist
		//Use cached actor data to populate scene with actors


		//Equips items for each actor
		//_inventoryCollection.items.ForEach(i => EquipItem(data, i));

		//Spawns any owners who aren't already in the scene
		//_inventoryCollection.items.ForEach( i => Instantiate(i.gameObject));
		
		//Get Companion Items
		//_inventoryCollection.items.Where()

		//Equip Items to companions
		//GetFilteredInventory("companion").ForEach( c => c.gameObject.GetComponent<CompanionController>().Equip() );

	

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
