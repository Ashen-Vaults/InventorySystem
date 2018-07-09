using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
	public static ActorManager instance;

	public List<ActorData> actorDatas;

	void Awake()
	{
		instance = this;
	}


}
