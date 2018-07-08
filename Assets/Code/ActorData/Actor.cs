using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Actor : MonoBehaviour
{

	[SerializeField]
	Inventory _inventoryCollection;

	[SerializeField]
	StatData _baseStats;

	[SerializeField]
	List<StatData> _modifiers;

	public void Equip(Item i)
	{
		i.owner = this;
		i.equipped = true;
		_modifiers.Add(i.stats);
		_inventoryCollection.items.Add(i);
	}

	public void UnEquip(Item i)
	{
		i.owner = null;
		i.equipped = false;
		_modifiers.Remove(i.stats);
		_inventoryCollection.items.Remove(i);
	}

	public StatData GetModifiedStats()
	{
	//		_modifiers.ForEach( s => s.a)
		return null;
	}
}
