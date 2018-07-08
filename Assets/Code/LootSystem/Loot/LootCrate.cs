using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LootCrate : MonoBehaviour
{
	public LootCratePrototype prototype;

	public RangedInt itemDropRange;

	public void OnValidate()
	{
		if(prototype != null && prototype.loots != null)
		{
			if(itemDropRange.end > prototype.loots.Count)
			{
				itemDropRange.end = prototype.loots.Count;
			}
		}
	}

	public Item GiveLoot(IProbable probability)
	{
		List<float> weights = new List<float>();

		prototype.loots.ForEach( l => weights.Add(l.dropRate));

		List<int> ids = probability.SelectIds( weights,  itemDropRange);

		Item i = prototype.loots[ids[0]].itemPrototype.Clone();
		return i;
	}

	[ContextMenu("Give Loot")]
	void TestGiveLoot()
	{
		Item i = GiveLoot(new RouletteProbable());

		InventoryManager.instance.AddItem(i);

		Instantiate(i.gameObject);
	}
}
