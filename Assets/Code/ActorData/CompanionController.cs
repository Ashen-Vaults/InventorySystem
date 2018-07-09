using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : Actor 
{
    public override void Init(ActorData data)
    {
        base.Init(data);

        Debug.Log("YO__ " + this);
        //TODO: TEMP?
        Item i = InventoryManager.instance.RequestItemByOwner(data.id);
        data.name = i.name;
        data.iconSprite = i.iconSprite;
    }
}
