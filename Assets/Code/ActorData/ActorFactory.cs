﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorFactory : MonoBehaviour
{

	[SerializeField]
	ActorData _data;

	[SerializeField]
	List<Actor> _instantiatedActors;

	public Actor CreateActor(ActorData data)
	{
		Actor a = GetActor(data);
		if(a == null)
		{
			GameObject g = new GameObject(data.id);
			a = g.AddComponent<Actor>();
			_instantiatedActors.Add(a);
		}
		a.Init(data);
		//data.SpawnItemGameObject();
		return a;
	}

	public Actor GetActor(ActorData data)
	{
		return _instantiatedActors.FirstOrDefault( a => a.data.id == data.id);
	}


	[SerializeField]
	[TextArea(1,15)]
	string _json;

	[ContextMenu("Create Actor")]
	void TestCreateActor()
	{
		_data = Load();
		CreateActor(_data);
	}

	ActorData Load()
	{
		return JsonUtility.FromJson<ActorData>(_json);
	}

}
