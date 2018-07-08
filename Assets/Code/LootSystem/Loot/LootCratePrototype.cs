using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Crate")]
public class LootCratePrototype : ScriptableObject
{
	public List<Loot> loots;
}
