using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Loot 
{
	public ItemPrototype itemPrototype;

	[Range(0,1)]
	public float dropRate;
}
