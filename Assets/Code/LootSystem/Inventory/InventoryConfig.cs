using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Config")]
public class InventoryConfig : ScriptableObject
{
	public List<string> dontSpawnTags;
}
