using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Generic")]
public class ItemPrototype : ScriptableObject
{
	public Item item;

	public Item Clone()
	{
		return item.Clone();
	}
}
