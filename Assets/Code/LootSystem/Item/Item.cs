using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
	public string name;

	[TextArea()]
	public string desc;

	public Texture2D iconSprite;
	
	public GameObject gameObject;

	public bool equipped;

	public Actor owner;

	public StatData stats;

	public List<string> tags;

	public Item()
	{
		
	}

	public Item(Item i)
	{
		this.name = i.name;
		this.desc = i.desc;
		this.iconSprite = i.iconSprite;
		this.gameObject = i.gameObject;
		this.equipped = i.equipped;
		this.owner = i.owner;
		this.stats = i.stats;
	}
}
