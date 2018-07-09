using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Item 
{
	public string name;

	[TextArea()]
	public string desc;

	[SerializeField]
	public Texture2D iconSprite;
	
	public GameObject gameObject;

	public bool equipped;

	public string ownerID;

	public StatData stats;

	public List<string> tags;

	public Item()
	{
		
	}

	protected Item(Item i)
	{
		this.name = i.name;
		this.desc = i.desc;
		this.iconSprite = i.iconSprite;
		this.gameObject = i.gameObject;
		this.equipped = i.equipped;
		this.ownerID = i.ownerID;
		this.stats = i.stats;
		this.tags = i.tags;
		RandomizeStats();
	}

	public Item Clone()
	{	
		string json = JsonUtility.ToJson(this);
		Item i = new Item(JsonUtility.FromJson<Item>(json));
		return i;
	}


	//TODO:
	void RandomizeStats()
	{
		Debug.Log("Randomize");
		this.stats.attack += UnityEngine.Random.Range(1,5);
	}

	public GameObject InstantiateGameObject()
	{
		GameObject g = null;
		if(gameObject != null)
		{
			g = GameObject.Instantiate(gameObject);
		}
		return g;
	}
}
