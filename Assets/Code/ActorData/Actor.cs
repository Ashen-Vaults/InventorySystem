using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Actor : MonoBehaviour
{

	[SerializeField]
	[TextArea(1,15)]
	string _json;

	[SerializeField]
	public ActorData data;

	public void Init(ActorData data)
	{
		this.data = data;
	}

	public StatData GetModifiedStats()
	{
	//		_modifiers.ForEach( s => s.a)
		return null;
	}

	[ContextMenu("Save")]
	public void Save()
	{
		_json = JsonUtility.ToJson(data, true);
		PlayerPrefs.SetString(data.id, _json);
	}
}
