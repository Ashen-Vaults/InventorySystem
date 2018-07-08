using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory
{
	[Header("Inventory Properties")]
	public List<Item> items = new List<Item>();
}
